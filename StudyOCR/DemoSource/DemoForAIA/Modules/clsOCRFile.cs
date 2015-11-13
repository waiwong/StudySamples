using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using BWHITD.Sys.Common;
using OCR.TesseractWrapper;

namespace DemoForAIA
{
    public class OCRFile
    {
        private Rectangle rcChequeNo = new Rectangle(380, 100, 150, 20);
        private Rectangle rcTxtNo = new Rectangle(80, 100, 250, 20);

        private string TESSDATA = Path.Combine(SysUtil.GetAssemblyDirectory(), "tessdata/");
        private string emptyRCKey = "EmptyRect";
        private int oem = 3;
        private string pageSegMode = "7"; // or alternatively, "PSM_AUTO"; // 3 - Fully automatic page segmentation, but no OSD (default)
        public string ErrMsg { get; set; }
        public Dictionary<string, Rectangle> DicOrcRect { get; set; }
        public string ChequeNoDicKey { get; set; }
        public string TxNoDicKey { get; set; }

        public OCRFile()
        {
            this.ChequeNoDicKey = "ChequeNo";
            this.TxNoDicKey = "TxtNo";
            Dictionary<string, Rectangle> dicOrcRect = new Dictionary<string, Rectangle>();
            dicOrcRect.Add(this.ChequeNoDicKey, rcChequeNo);
            dicOrcRect.Add(this.TxNoDicKey, rcTxtNo);
            this.DicOrcRect = dicOrcRect;
        }

        public string UseTesseract(string imgFile)
        {
            this.ErrMsg = string.Empty;
            string defLang = "eng";

            string strResult = string.Empty;
            try
            {
                using (TesseractProcessor processor = new TesseractProcessor())
                {
                    processor.Init(this.TESSDATA, defLang, this.oem);
                    processor.SetPageSegMode((ePageSegMode)Enum.Parse(typeof(ePageSegMode), this.pageSegMode));

#if DEBUG
                    System.Diagnostics.Debug.WriteLine("processor:");
                    System.Diagnostics.Debug.WriteLine(processor.GetTesseractEngineVersion());
#endif

                    string strIndicate = Path.GetFileNameWithoutExtension(imgFile);

                    strResult = processor.Recognize(imgFile);
                    if (!string.IsNullOrEmpty(strResult))
                    {
                        // correct common errors caused by OCR
                        strResult = this.CorrectOCRErrors(strResult);
                        // correct letter cases
                        strResult = this.CorrectLetterCases(strResult);
                    }
                }

                strResult = strResult.Replace("\n", Environment.NewLine);
            }
            catch (Exception ex)
            {
                this.ErrMsg = ex.ToString();
                strResult = string.Empty;
            }

            return strResult;
        }

        public string UseTesseractExe(string imgFile)
        {
            this.ErrMsg = string.Empty;
            string strFileExt = ".txt";
            string defLang = "eng";
            string tempTessOutputFile = Path.GetTempFileName();
            if (File.Exists(tempTessOutputFile))
                File.Delete(tempTessOutputFile);
            tempTessOutputFile = Path.ChangeExtension(tempTessOutputFile, strFileExt);
            string outputFileName = tempTessOutputFile.Substring(0, tempTessOutputFile.Length - strFileExt.Length); // chop the .txt extension

            string strResult = string.Empty;

            try
            {
                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    // Redirect the output stream of the child process.
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.FileName = "tesseract.exe";

                    p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" -l {2}  -psm {3}  digits",
                        imgFile, outputFileName, defLang, this.pageSegMode);
                    p.Start();

                    // Read the output stream first and then wait.
                    string output = p.StandardOutput.ReadToEnd(); // ignore standard output
                    string error = p.StandardError.ReadToEnd();

                    p.WaitForExit();

                    if (p.ExitCode == 0)
                    {
                        string strIndicate = Path.GetFileNameWithoutExtension(imgFile);
                        using (StreamReader sr = new StreamReader(tempTessOutputFile, Encoding.UTF8, true))
                        {
                            strResult = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        if (File.Exists(tempTessOutputFile))
                            File.Delete(tempTessOutputFile);
                        if (error.Trim().Length == 0)
                        {
                            error = "Errors occurred.";
                        }

                        this.ErrMsg = error;
                    }

                    File.Delete(tempTessOutputFile);
                }
            }
            catch (Exception ex)
            {
                this.ErrMsg = ex.ToString();
                strResult = string.Empty;
            }

            return strResult;
        }

        public string UseMODI(string imgFile)
        {
            this.ErrMsg = string.Empty;
            string strResult = string.Empty;
            //TODO: Can not use MODI in windows7
            //MODI.Document md = new MODI.Document();
            //try
            //{
            //    md.Create(imgFile);
            //    md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);

            //    MODI.Image image = (MODI.Image)md.Images[0];
            //    strResult = image.Layout.Text;
            //}
            //catch (Exception ex)
            //{
            //    this.ErrMsg = ex.ToString();
            //    strResult = string.Empty;
            //}
            //finally
            //{
            //    md.Close(false);
            //}

            return strResult;
        }

        public string UseTessnet2(string imgFile)
        {
            Dictionary<string, string> dicResult = this.UseTessnet2(imgFile, null);
            return dicResult[this.emptyRCKey];
        }

        public string UseTessnet2(string imgFile, string strOCRKey, Rectangle rcOCR)
        {
            Dictionary<string, Rectangle> pDicOcrRect = new Dictionary<string, Rectangle>();
            pDicOcrRect.Add(strOCRKey, rcOCR);
            Dictionary<string, string> dicResult = this.UseTessnet2(imgFile, pDicOcrRect);
            return dicResult[strOCRKey];
        }

        public Dictionary<string, string> UseTessnet2(string imgFile, Dictionary<string, Rectangle> pDicOcrRect)
        {
            this.ErrMsg = string.Empty;

            string defLang = "eus";
            Dictionary<string, string> dicResult = new Dictionary<string, string>();

            if (pDicOcrRect == null)
            {
                pDicOcrRect = new Dictionary<string, Rectangle>();
                pDicOcrRect.Add(this.emptyRCKey, Rectangle.Empty);
            }

            tessnet2.Tesseract ocr = new tessnet2.Tesseract();
            ocr.Init(this.TESSDATA, defLang, false);

            using (Bitmap m_image = new Bitmap(imgFile))
            {
                foreach (var recItem in pDicOcrRect)
                {
                    Rectangle rcTemp = recItem.Value;
                    if (rcTemp != Rectangle.Empty)
                    {
                        int dx = Convert.ToInt16(m_image.HorizontalResolution / 100);
                        int dy = Convert.ToInt16(m_image.VerticalResolution / 100);

                        rcTemp = new Rectangle(recItem.Value.X * dx, recItem.Value.Y * dy,
                            recItem.Value.Width * dx, recItem.Value.Height * dy);
                    }

                    List<tessnet2.Word> m_words = ocr.DoOCR(m_image, rcTemp);
                    StringBuilder sbWords = new StringBuilder();
                    foreach (var item in m_words)
                    {
                        sbWords.AppendLine(item.Text);
                    }

                    string text = sbWords.ToString();
                    if (!string.IsNullOrEmpty(text))
                    {
                        //// correct common errors caused by OCR
                        //text = TextUtilities.CorrectOCRErrors(text);
                        //// correct letter cases
                        //text = TextUtilities.CorrectLetterCases(text);
                        text = text.Replace("\n", Environment.NewLine);
                    }
                    else
                    {
                        text = string.Empty;
                    }

                    dicResult.Add(recItem.Key, text);
                }
            }

            return dicResult;
        }

        public void PreparePathAndSplitTiff(string checkPath, string dirSplitPath)
        {
            if (!Directory.Exists(dirSplitPath))
            {
                Directory.CreateDirectory(dirSplitPath);
            }
            else
            {
                IEnumerator delFiles = Directory.GetFiles(dirSplitPath, "*", SearchOption.AllDirectories).GetEnumerator();
                while (delFiles.MoveNext())
                {
                    File.Delete(delFiles.Current.ToString());
                }
            }

            IEnumerator tifFiles = Directory.GetFiles(checkPath, "*.tif").GetEnumerator();
            while (tifFiles.MoveNext())
            {
                string currFile = tifFiles.Current.ToString();
                using (TiffManager mTiff = new TiffManager(currFile))
                {
                    mTiff.SplitTiffImage(dirSplitPath, EncoderValue.CompressionNone);
                }
            }
        }

        #region CropImage
        public void CropImage(string dirSplitPath, string dirClonePath, Dictionary<string, Rectangle> dicOrcRect)
        {
            IEnumerator findFiles = Directory.GetFiles(dirSplitPath).GetEnumerator();
            while (findFiles.MoveNext())
            {
                foreach (var item in dicOrcRect)
                {
                    this.CropImage(findFiles.Current.ToString(), dirClonePath, item.Key, item.Value);
                }
            }
        }

        public string CropImage(string strCropSrcImage, string dirCropPath, string strCropKey, Rectangle rcCrop)
        {
            string strResult = string.Empty;

            if (File.Exists(strCropSrcImage))
            {
                using (Image curImg = Image.FromFile(strCropSrcImage))
                {
                    using (Image imgChequeNo = this.Crop(curImg, rcCrop))
                    {
                        string saveFFN = Path.Combine(dirCropPath, string.Format("{0}_{1}.tiff",
                            Path.GetFileNameWithoutExtension(strCropSrcImage), strCropKey));

                        if (File.Exists(saveFFN))
                            File.Delete(saveFFN);
                        imgChequeNo.Save(saveFFN, ImageFormat.Tiff);
                        strResult = saveFFN;
                    }
                }
            }

            return strResult;
        }

        public Image Crop(Image image, Rectangle cropArea)
        {
            int dx = Convert.ToInt16(image.HorizontalResolution / 100);
            int dy = Convert.ToInt16(image.VerticalResolution / 100);
            Rectangle rcTemp = new Rectangle(cropArea.X * dx, cropArea.Y * dy,
                cropArea.Width * dx, cropArea.Height * dy);

            Bitmap bmp = new Bitmap(rcTemp.Width, rcTemp.Height);
            bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawImage(image, 0, 0, rcTemp, GraphicsUnit.Pixel);
            gfx.Dispose();

            return bmp;
        }

        private Image CropByClone(string imgFileFullName, Rectangle cropArea)
        {
            using (Bitmap srcImage = new Bitmap(imgFileFullName))
            {
                System.Drawing.Imaging.PixelFormat format = srcImage.PixelFormat;
                int dx = Convert.ToInt16(srcImage.HorizontalResolution / 100);
                int dy = Convert.ToInt16(srcImage.VerticalResolution / 100);
                Rectangle rcTemp = new Rectangle(cropArea.X * dx, cropArea.Y * dy,
                    cropArea.Width * dx, cropArea.Height * dy);
                return srcImage.Clone(rcTemp, format);
            }
        }
        #endregion

        #region TextUtilities
        /// <summary>
        /// Corrects letter cases.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CorrectLetterCases(String input)
        {
            // lower uppercase letters ended by lowercase letters except the first letter
            Regex regex = new Regex("(?<=\\p{L}+)(\\p{Lu}+)(?=\\p{Ll}+)");
            input = regex.Replace(input, new MatchEvaluator(LowerCaseText));

            //// lower uppercase letters begun by lowercase letters
            regex = new Regex("(?<=\\p{Ll}+)(\\p{Lu}+)");
            input = regex.Replace(input, new MatchEvaluator(LowerCaseText));

            return input;
        }

        private string LowerCaseText(Match m)
        {
            // Lowercase the matched string.
            return m.Value.ToLower();
        }

        /// <summary>
        /// Corrects common Tesseract OCR errors.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CorrectOCRErrors(String input)
        {
            // substitute letters frequently misrecognized by Tesseract 2.03
            return Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(input,
                        "\\b1(?=\\p{L}+\\b)", "l"), // 1 to l
                        "\\b11(?=\\p{L}+\\b)", "n"), // 11 to n
                        "\\bI(?![mn]+\\b)", "l"), // I to l
                        "(?<=\\b\\p{L}*)0(?=\\p{L}*\\b)", "o") // 0 to o
                //Regex.Replace("(?<!\\.) S(?=\\p{L}*\\b)", " s") // S to s
                //Regex.Replace("(?<![cn])h\\b", "n")
            ;
        }
        #endregion
    }
}
