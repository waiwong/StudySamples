using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DemoForAIA
{
    /// <summary>
    /// Summary description for TiffManager.
    /// </summary>
    public class TiffManager : IDisposable
    {
        private string mImageFileName;
        private int mPageNumber;
        private Image image;
        private string mTempWorkingDir;

        public TiffManager()
        {
        }

        public TiffManager(string imageFileName)
        {
            this.mImageFileName = imageFileName;
            this.image = Image.FromFile(this.mImageFileName);
            this.GetPageNumber();
        }

        /// <summary>
        /// Read the image file for the page number.
        /// </summary>
        private void GetPageNumber()
        {
            Guid objGuid = this.image.FrameDimensionsList[0];
            FrameDimension objDimension = new FrameDimension(objGuid);

            //Gets the total number of frames in the .tiff file
            this.mPageNumber = this.image.GetFrameCount(objDimension);

            return;
        }

        /// <summary>
        /// Read the image base string,
        /// Assert(GetFileNameStartString(@"c:\test\abc.tif"),"abc")
        /// </summary>
        /// <param name="strFullName"></param>
        /// <returns></returns>
        private string GetFileNameStartString(string strFullName)
        {
            int posDot = this.mImageFileName.LastIndexOf(".");
            int posSlash = this.mImageFileName.LastIndexOf(@"\");
            return this.mImageFileName.Substring(posSlash + 1, posDot - posSlash - 1);
        }

        /// <summary>
        /// This function will output the image to a TIFF file with specific compression format
        /// </summary>
        /// <param name="outPutDirectory">The splited images' directory</param>
        /// <param name="format">The codec for compressing</param>
        /// <returns>splited file name array list</returns>
        public ArrayList SplitTiffImage(string outPutDirectory, EncoderValue format)
        {
            string fileStartString = outPutDirectory + "\\" + this.GetFileNameStartString(this.mImageFileName);
            ArrayList splitedFileNames = new ArrayList();
            try
            {
                Guid objGuid = this.image.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                //Saves every frame as a separate file.
                Encoder enc = Encoder.Compression;
                int curFrame = 0;
                for (int i = 0; i < this.mPageNumber; i++)
                {
                    this.image.SelectActiveFrame(objDimension, curFrame);
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(enc, (long)format);
                    ImageCodecInfo info = this.GetEncoderInfo("image/tiff");

                    //Save the master bitmap
                    string fileName = string.Format("{0}{1}.TIF", fileStartString, i.ToString());
                    this.image.Save(fileName, info, ep);
                    splitedFileNames.Add(fileName);

                    curFrame++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return splitedFileNames;
        }

        /// <summary>
        /// This function will output the image to a ImageFormat file
        /// </summary>
        /// <param name="outPutDirectory">The splited images' directory</param>
        /// <param name="format">The codec for compressing</param>
        /// <returns>splited file name array list</returns>
        public ArrayList SplitTiffImageToFormat(string outPutDirectory, EncoderValue format, ImageFormat imgFormat)
        {
            string fileStartString = outPutDirectory + "\\" + this.GetFileNameStartString(this.mImageFileName);
            ArrayList splitedFileNames = new ArrayList();
            try
            {
                Guid objGuid = this.image.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                //Saves every frame as a separate file.
                // Encoder enc = Encoder.Compression;
                int curFrame = 0;
                for (int i = 0; i < this.mPageNumber; i++)
                {
                    this.image.SelectActiveFrame(objDimension, curFrame);
                    using (Bitmap bmp = new Bitmap(this.image))
                    {
                        string saveFileName = String.Format("{0}{1}.{2}", fileStartString, i.ToString(), imgFormat.ToString());
                        bmp.Save(saveFileName, imgFormat);
                        splitedFileNames.Add(saveFileName);
                    }

                    curFrame++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return splitedFileNames;
        }

        /// <summary>
        /// This function will join the TIFF file with a specific compression format
        /// </summary>
        /// <param name="imageFiles">string array with source image files</param>
        /// <param name="outFile">target TIFF file to be produced</param>
        /// <param name="compressEncoder">compression codec enum</param>
        public void JoinTiffImages(string[] imageFiles, string outFile, EncoderValue compressEncoder)
        {
            try
            {
                //If only one page in the collection, copy it directly to the target file.
                if (imageFiles.Length == 1)
                {
                    File.Copy(imageFiles[0], outFile, true);
                    return;
                }

                //use the save encoder
                Encoder enc = Encoder.SaveFlag;

                EncoderParameters ep = new EncoderParameters(2);
                ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
                ep.Param[1] = new EncoderParameter(Encoder.Compression, (long)compressEncoder);

                Bitmap pages = null;
                int frame = 0;
                ImageCodecInfo info = this.GetEncoderInfo("image/tiff");

                foreach (string strImageFile in imageFiles)
                {
                    if (frame == 0)
                    {
                        pages = (Bitmap)Image.FromFile(strImageFile);

                        //save the first frame
                        pages.Save(outFile, info, ep);
                    }
                    else
                    {
                        //save the intermediate frames
                        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);

                        Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                        pages.SaveAdd(bm, ep);
                    }

                    if (frame == imageFiles.Length - 1)
                    {
                        //flush and close.
                        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
                        pages.SaveAdd(ep);
                    }

                    frame++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return;
        }

        /// <summary>
        /// This function will join the TIFF file with a specific compression format
        /// </summary>
        /// <param name="imageFiles">array list with source image files</param>
        /// <param name="outFile">target TIFF file to be produced</param>
        /// <param name="compressEncoder">compression codec enum</param>
        public void JoinTiffImages(ArrayList imageFiles, string outFile, EncoderValue compressEncoder)
        {
            try
            {
                //If only one page in the collection, copy it directly to the target file.
                if (imageFiles.Count == 1)
                {
                    File.Copy((string)imageFiles[0], outFile, true);
                    return;
                }

                //use the save encoder
                Encoder enc = Encoder.SaveFlag;

                EncoderParameters ep = new EncoderParameters(2);
                ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
                ep.Param[1] = new EncoderParameter(Encoder.Compression, (long)compressEncoder);

                Bitmap pages = null;
                int frame = 0;
                ImageCodecInfo info = this.GetEncoderInfo("image/tiff");

                foreach (string strImageFile in imageFiles)
                {
                    if (frame == 0)
                    {
                        pages = (Bitmap)Image.FromFile(strImageFile);

                        //save the first frame
                        pages.Save(outFile, info, ep);
                    }
                    else
                    {
                        //save the intermediate frames
                        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);

                        Bitmap bm = (Bitmap)Image.FromFile(strImageFile);
                        pages.SaveAdd(bm, ep);
                        bm.Dispose();
                    }

                    if (frame == imageFiles.Count - 1)
                    {
                        //flush and close.
                        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
                        pages.SaveAdd(ep);
                    }

                    frame++;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                throw;
            }

            return;
        }

        /// <summary>
        /// Remove a specific page within the image object and save the result to an output image file.
        /// </summary>
        /// <param name="pageNumber">page number to be removed</param>
        /// <param name="compressEncoder">compress encoder after operation</param>
        /// <param name="strFileName">filename to be outputed</param>
        public void RemoveAPage(int pageNumber, EncoderValue compressEncoder, string strFileName)
        {
            try
            {
                //Split the image files to single pages.
                ArrayList arrSplited = this.SplitTiffImage(this.mTempWorkingDir, compressEncoder);

                //Remove the specific page from the collection
                string strPageRemove = string.Format("{0}\\{1}{2}.TIF", this.mTempWorkingDir, this.GetFileNameStartString(this.mImageFileName), pageNumber);
                arrSplited.Remove(strPageRemove);

                this.JoinTiffImages(arrSplited, strFileName, compressEncoder);
            }
            catch (Exception)
            {
                throw;
            }

            return;
        }

        /// <summary>
        /// Getting the supported codec info.
        /// </summary>
        /// <param name="mimeType">description of mime type</param>
        /// <returns>image codec info</returns>
        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; j++)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }

            throw new Exception(mimeType + " mime type not found in ImageCodecInfo");
        }

        /// <summary>
        /// Return the memory steam of a specific page
        /// </summary>
        /// <param name="pageNumber">page number to be extracted</param>
        /// <returns>image object</returns>
        public Image GetSpecificPage(int pageNumber)
        {
            MemoryStream ms = null;
            Image retImage = null;
            try
            {
                ms = new MemoryStream();
                Guid objGuid = this.image.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                this.image.SelectActiveFrame(objDimension, pageNumber);
                this.image.Save(ms, ImageFormat.Bmp);

                retImage = Image.FromStream(ms);

                return retImage;
            }
            catch (Exception)
            {
                ms.Close();
                retImage.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Convert the existing TIFF to a different codec format
        /// </summary>
        /// <param name="compressEncoder"></param>
        public void ConvertTiffFormat(string strNewImageFileName, EncoderValue compressEncoder)
        {
            //Split the image files to single pages.
            ArrayList arrSplited = this.SplitTiffImage(this.mTempWorkingDir, compressEncoder);
            this.JoinTiffImages(arrSplited, strNewImageFileName, compressEncoder);
        }

        /// <summary>
        /// Image file to operate
        /// </summary>
        public string ImageFileName
        {
            get
            {
                return this.mImageFileName;
            }
            set
            {
                this.mImageFileName = value;
            }
        }

        /// <summary>
        /// Buffering directory
        /// </summary>
        public string TempWorkingDir
        {
            get
            {
                return this.mTempWorkingDir;
            }
            set
            {
                this.mTempWorkingDir = value;
            }
        }

        /// <summary>
        /// Image page number
        /// </summary>
        public int PageNumber
        {
            get
            {
                return this.mPageNumber;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.image.Dispose();
            System.GC.SuppressFinalize(this);
        }

        #endregion
    }
}
