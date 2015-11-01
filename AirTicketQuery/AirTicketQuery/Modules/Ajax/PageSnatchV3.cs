using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using AirTicketQuery.Modules.Code;

namespace AirTicketQuery.Modules.Ajax
{
    public class PageSnatch
    {
        delegate void BrowserEventHandler();
        private readonly string url = string.Empty;
        private int timeout;
        private const int defaultTimeout = 10;

        public PageSnatch(string url)
        {
            this.timeout = defaultTimeout;
            this.url = url;
        }

        public PageSnatch(string url, int timeout)
            : this(url)
        {
            this.timeout = timeout;
        }

        public Exception Error { set; get; }
        public string HTMLSourceCode { set; get; }
        public string TextAsync { set; get; }
        private bool completed = false;
        public void Navigate()
        {
            try
            {
                this.TextAsync = string.Empty;
                this.HTMLSourceCode = string.Empty;
                int interval = 500;
                Thread thread = new Thread(delegate()
                {
                    using (WebBrowser browser = new WebBrowser())
                    {
                        browser.ScriptErrorsSuppressed = false;
                        browser.ScrollBarsEnabled = false;
                        browser.AllowNavigation = true;
                        DateTime startTime = DateTime.Now;
                        bool isbusy = true;
                        bool isRefresh = false;
                        // bool completed = false;
                        int count = 6;
                        int index = 0;
                        int length = 0;
                        browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
                        do
                        {
                            //browser.DocumentCompleted += delegate(object sender, WebBrowserDocumentCompletedEventArgs e)
                            //{
                            //    this.HTMLSourceCode = browser.Document.Body.OuterHtml;
                            //    string url0 = browser.Document.Url.ToString();
                            //    completed = url0.Equals(e.Url.ToString());
                            //};

                            if (!isRefresh)
                                browser.Navigate(url);
                            while (browser.ReadyState != WebBrowserReadyState.Complete || !browser.StatusText.Equals("Done"))
                                System.Windows.Forms.Application.DoEvents();
                            if (this.completed)
                                this.TextAsync = browser.Document.Body.OuterHtml;
                            string strRegex = "(?<ITEM><figure class=\"flight-info none\" id=\"flight-info\" style=\"display: block;\">)[\\S\\s]*?(?=</figure>)";
                            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                            Match mc = re.Match(this.TextAsync);
                            if (mc.Length > 0)
                            {
                                System.Diagnostics.Debug.WriteLine(mc.Value);
                                break;
                            }
                            else
                                isRefresh = true;

                            if (browser.ReadyState == WebBrowserReadyState.Complete && browser.StatusText.Equals("Done"))
                            {
                                System.Windows.Forms.Application.DoEvents();
                                BrowserEventHandler browserEventHanler = delegate() { isbusy = !browser.IsBusy; };
                                browser.Invoke(browserEventHanler);

                                //try
                                //{
                                //    browser.Document.InvokeScript("flight.submit()");
                                //    isRefresh = true;
                                //    completed = false;
                                //}
                                //catch (Exception ex)
                                //{
                                //    System.Diagnostics.Debug.Write(ex);
                                //}
                            }

                            if (!isbusy)
                            {
                                int len = browser.Document.Body.OuterHtml.Length;
                                if (len == length) { index++; }
                                else { index = 0; length = len; }
                                if (index == count) { isbusy = false; }
                            }

                            if (this.completed)
                                length = browser.Document.Body.OuterHtml.Length;
                            System.Threading.Thread.Sleep(interval);
                            double t = Math.Ceiling((DateTime.Now - startTime).TotalSeconds);
                            if (t >= this.timeout)
                            {
                                this.Error = new Exception("Visiting about new exception delay, since the setting is timeout");
                                break;
                            }
                        } while (isbusy);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            catch (Exception ex) { throw ex; }
        }

        private void wb_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                this.completed = true;
                WebBrowser wbSender = sender as WebBrowser;
                this.TextAsync = wbSender.Document.Body.OuterHtml;
                string strRegex = "(?<ITEM><figure class=\"flight-info none\" id=\"flight-info\" style=\"display: block;\">)[\\S\\s]*?(?=</figure>)";
                Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match mc = re.Match(this.TextAsync);
                if (mc.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine(mc.Value);

                }
                else
                {
                    if (wbSender.Document.GetElementById("btn_flight_search") != null)
                        wbSender.Document.GetElementById("btn_flight_search").InvokeMember("click");
                }

                //if (wbSender.Document.Forms.Count > 0 && this.wbgmail.Document.Forms[0].All["Email"] != null)
                //{
                //    this.wbgmail.Document.Forms[0].All["Email"].SetAttribute("value", "viveywang@gmail.com");
                //    this.wbgmail.Document.Forms[0].All["Passwd"].SetAttribute("value", "vivey107083");
                //    this.wbgmail.Document.GetElementById("signIn").InvokeMember("click");
                //}
                //else
                //{
                //    int linkCount = this.wbgmail.Document.Links.Count;
                //    if (linkCount == 8)
                //    {
                //        for (int i = 0; i < linkCount; i++)
                //        {
                //            if (this.wbgmail.Document.Links[i].InnerText.ToLower().Contains("load basic html"))
                //            {
                //                this.wbgmail.Document.Links[i].InvokeMember("click");
                //                break;
                //            }
                //        }
                //    }
                //    else
                //    {
                ////        Log.LogInfo("wbgmail_DocumentCompleted Called.");
                //        if (this.wbgmail.Document.GetElementsByTagName("input").GetElementsByName("nvp_bu_d").Count > 0)
                //        {
                //            if (this.wbgmail.Document.GetElementById("to") != null && string.IsNullOrEmpty(this.wbgmail.Document.GetElementById("to").InnerText))
                //            {
                //                this.wbgmail.Document.GetElementById("to").SetAttribute("value", "13823061171@139.com");
                //                this.wbgmail.Document.GetElementsByTagName("input").GetElementsByName("subject")[0].SetAttribute("value", DateTime.Now.ToString("MMdd"));
                //            }
                //        }
                //        else if (this.wbgmail.Document.GetElementsByTagName("input").GetElementsByName("q").Count == 1)
                //        {
                //            if (this.wbgmail.Document.GetElementsByTagName("input").GetElementsByName("q")[0].OuterHtml.Contains("label:other-lp"))
                //            {
                //                this.ClickNewMail();
                //            }
                //        }

                //        if (this.wbgmail.Document.GetElementsByTagName("textarea").GetElementsByName("body").Count > 0)
                //        {
                //            this.wbgmail.Document.GetElementsByTagName("textarea").GetElementsByName("body")[0].Focus();
                //            if (InputLanguage.CurrentInputLanguage.LayoutName.Equals("US"))
                //            {
                //                InputLanguageCollection myInput = InputLanguage.InstalledInputLanguages;
                //                foreach (InputLanguage input in myInput)
                //                {
                //                    if (!input.LayoutName.Equals("US"))
                //                    {
                //                        InputLanguage.CurrentInputLanguage = input;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.LogErr(ex);
            }
        }
    }
}