using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
namespace AirTicketQuery.Modules.Ajax
{
    /// <summary>   
    /// 获取网站异步源码类   
    /// </summary>   
    public class PageSnatchV2
    {
        #region field
        private WebBrowser browser;
        /// <summary>   
        /// 默认异步加载延时5s   
        /// </summary>   
        private int timeout;
        private const int defaultTimeout = 5 * 1000;
        #endregion
        #region ctor
        public PageSnatchV2()
        {
            this.IsBusy = false;  //标志为完成状态,可开始新一导航   
            this.timeout = defaultTimeout;
        }
        public PageSnatchV2(string url)
            : this()
        {
            this.Url = url; //绑定URL   
        }
        public PageSnatchV2(string url, int timeout)
            : this(url)  //调用一个构造参数   
        {
            this.timeout = timeout; //绑定延时   
        }
        #endregion
        #region event
        /// <summary>   
        /// 在控件导航到新文档并开始加载该文档时发生   
        /// </summary>   
        public event SnatchingEventHandler Snatching;
        /// <summary>   
        /// 在控件完成加载文档时发生   
        /// </summary>   
        public event SnatchedEventHandler Snatched;
        /// <summary>   
        /// 异步文档加载完毕发生   
        /// </summary>   
        public event SnatchCompletedEventHandler SnatchCompleted;
        #endregion
        #region property
        /// <summary>   
        /// 获取或设置是否取消异步数据加载   
        /// </summary>   
        public bool Cancel { set; get; }
        /// <summary>   
        /// 获取加载文档是否完成   
        /// </summary>   
        public bool IsBusy { private set; get; }
        /// <summary>   
        /// 验证网址是否有效   
        /// </summary>   
        /// <returns></returns>   
        private bool IsValidate
        {
            get { return Regex.IsMatch(Url, @"http(s)?://([/w-]+/.)+[/w-]+(/[/w- ./?%&=]*)?"); }
        }
        private string OuterHtml
        {
            get
            {
                string text = string.Empty;
                this.Execute(delegate()
                {
                    text = browser.Document.Body.OuterHtml;
                });
                return text;
            }
        }
        /// <summary>   
        /// 获取或设置加载异步数延时间隔(默认延时,最小设置值5s)   
        /// </summary>   
        public int Timeout
        {
            get { return this.timeout; }
            set
            {
                if (value > defaultTimeout) this.timeout = value;
            }
        }
        /// <summary>   
        /// 获取或设置当前文档的 URL.   
        /// </summary>   
        public string Url { set; get; }
        #endregion
        #region methods
        /// <summary>   
        /// 释放文档资源   
        /// </summary>   
        private void Dispose()
        {
            this.Execute(delegate()
            {
                browser.Stop();
                browser.Dispose();
                browser = null;
            });
        }
        /// <summary>   
        /// WebBrowser 跨线程获取数据代理方法   
        /// </summary>   
        /// <param name="browserEventHanler"></param>   
        private void Execute(BrowserEventHandler browserEventHanler)
        {
            if (this.browser != null)
            {
                this.browser.Invoke(browserEventHanler);
            }
        }
        /// <summary>   
        /// 将指定的URL资源加载到WebBrowser控件   
        /// </summary>   
        public void Navigate()
        {
            this.Navigate(DBNull.Value);
        }
        /// <summary>   
        /// 将指定的URL资源加载到WebBrowser控件   
        /// </summary>   
        /// <param name="url">网址</param>   
        public void Navigate(string url)
        {
            this.Url = url;
            this.Navigate(DBNull.Value);
        }
        /// <summary>   
        /// 将指定的URL资源加载到WebBrowser控件   
        /// <param name="argument">一个对象,包含网页内容抓取完毕要使用的数据</param>   
        /// </summary>   
        public void Navigate(object argument)
        {
            if (this.IsBusy) throw new Exception("This document is busy！");
            // if (!this.IsValidate) throw new Exception("This url is wrong！");
            int interval = 500;
            this.IsBusy = true;  //网页异步加载状态   
            bool completed = false;
            SnatchCompletedEventArgs scea = new SnatchCompletedEventArgs();  //事件模型   
            scea.Argument = argument;
            try
            {
                this.browser = new WebBrowser();  //初始化导航网页对象   
                this.browser.ScriptErrorsSuppressed = false;
                this.browser.Navigated += delegate(object sender, WebBrowserNavigatedEventArgs e)
                {
                    if (this.Snatching != null)
                    {
                        SnatchingEventArgs sea = new SnatchingEventArgs();
                        sea.Argument = argument;
                        sea.Url = e.Url;
                        this.Snatching(this, sea);
                    }
                };
                this.browser.DocumentCompleted += delegate(object sender, WebBrowserDocumentCompletedEventArgs e)
                {
                    scea.Url = e.Url;
                    scea.Text = browser.Document.Body.OuterHtml;
                    string url0 = browser.Document.Url.ToString();
                    completed = url0.Equals(e.Url.ToString());
                    if (this.Snatched != null)
                    {
                        SnatchedEventArgs sea = new SnatchedEventArgs();
                        sea.Url = e.Url;
                        this.Snatched(this, sea);
                    }
                };
                this.browser.Navigate(Url);  //导航到当前文档   
                //BackgroundWorker worker = new BackgroundWorker();
                //worker.DoWork += delegate(object obj, DoWorkEventArgs dow)
                //{
                while (!completed && !Cancel)
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(interval);
                }
                int count = 6;
                int index = 0;
                int length = 0;
                DateTime startTime = DateTime.Now;   //异步开始计时初始值   
                while (this.IsBusy && !Cancel)
                {
                    System.Threading.Thread.Sleep(interval);
                    double t = Math.Ceiling((DateTime.Now - startTime).TotalMilliseconds);
                    if (t >= this.Timeout)
                    {
                        scea.Error = new Exception("Visiting about new exception delay, since the setting is timeout");
                        break;
                    }
                    this.Execute(delegate() { this.IsBusy = !browser.IsBusy; });
                    if (!IsBusy)
                    {
                        this.IsBusy = true;
                        int len = this.OuterHtml.Length;
                        if (len == length) { index++; }
                        else { index = 0; length = len; }
                        if (index == count) { this.IsBusy = false; }
                    }
                    length = this.OuterHtml.Length;
                }
                if (!Cancel)
                {
                    if (SnatchCompleted != null)
                    {
                        scea.TextAsync = this.OuterHtml;
                        scea.Timeout = (int)Math.Ceiling((DateTime.Now - startTime).TotalMilliseconds); //计算所耗时间   
                        SnatchCompleted(this, scea); //触发文档加载完毕事件    
                    }
                }
                //Dispose();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //};
                //worker.RunWorkerAsync();
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>   
        /// 将指定的URL资源加载到WebBrowser控件   
        /// </summary>   
        /// <param name="url">网址</param>   
        /// <param name="argument">一个对象,包含网页内容抓取完毕要使用的数据</param>   
        public void Navigate(string url, object argument)
        {
            this.Url = url;
            this.Navigate(argument);
        }
        /// <summary>   
        /// 将指定的URL资源加载到WebBrowser控件   
        /// </summary>   
        /// <param name="url">网址</param>   
        /// <param name="argument">一个对象,包含网页内容抓取完毕要使用的数据</param>   
        /// <param name="timeOut">异步数据延时等待时间</param>   
        public void Navigate(string url, object argument, int timeout)
        {
            this.Url = url;
            this.timeout = timeout;
            this.Navigate(argument);
        }
        #endregion
    }
    #region delegate
    /// <summary>   
    /// 内部方法代理   
    /// </summary>   
    delegate void BrowserEventHandler();
    /// <summary>   
    /// 表示将处理 Yyc.Net.PageSnatchV2 类的 Yyc.Net.PageSnatchV2.Snatching 事件的方法   
    /// </summary>   
    /// <param name="sender">事件源: Yyc.Net.PageSnatchV2</param>   
    /// <param name="e">包含事件数据: Yyc.Net.SnatchingEventArgs</param>   
    public delegate void SnatchingEventHandler(object sender, SnatchingEventArgs e);
    /// <summary>   
    /// 表示将处理 Yyc.Net.PageSnatchV2 类的 Yyc.Net.PageSnatchV2.Snatched 事件的方法   
    /// </summary>   
    /// <param name="sender">事件源: Yyc.Net.PageSnatchV2</param>   
    /// <param name="e">包含事件数据: Yyc.Net.SnatchedEventArgs</param>   
    public delegate void SnatchedEventHandler(object sender, SnatchedEventArgs e);
    /// <summary>   
    /// 表示将处理 Yyc.Net.PageSnatchV2 类的 Yyc.Net.PageSnatchV2.SnatchCompleted 事件的方法   
    /// </summary>   
    /// <param name="sender">事件源: Yyc.Net.PageSnatchV2</param>   
    /// <param name="e">包含事件数据: Yyc.Net.SnatchCompletedEventArgs</param>   
    public delegate void SnatchCompletedEventHandler(object sender, SnatchCompletedEventArgs e);
    #endregion
    #region model
    /// <summary>   
    /// 为 Yyc.Net.PageSnatchV2.Snatching 事件提供数据   
    /// </summary>   
    public class SnatchingEventArgs
    {
        /// <summary>   
        /// 事件处理程序中执行的后台操作使用的参数   
        /// </summary>   
        public object Argument { set; get; }
        /// <summary>   
        /// 获取当前导航到的文档位置   
        /// </summary>   
        public Uri Url { set; get; }
    }
    /// <summary>   
    /// 为 Yyc.Net.PageSnatchV2.SnatchedEventArgs 事件提供数据   
    /// </summary>   
    public class SnatchedEventArgs
    {
        /// <summary>   
        /// 获取当前导航到的文档位置   
        /// </summary>   
        public Uri Url { set; get; }
    }
    /// <summary>   
    /// 为 Yyc.Net.PageSnatchV2.SnatchCompleted 事件提供数据   
    /// </summary>   
    public class SnatchCompletedEventArgs
    {
        /// <summary>   
        /// 获取或设置静态源文本   
        /// </summary>   
        public string Text { set; get; }
        /// <summary>   
        /// 获取或设置异步网页源文本   
        /// </summary>   
        public string TextAsync { set; get; }
        /// <summary>   
        /// 获取或设置加载文档异常   
        /// </summary>   
        public Exception Error { set; get; }
        /// <summary>   
        /// 获取或设置加载异步验证延时(ms)   
        /// </summary>   
        public int Timeout { set; get; }
        /// <summary>   
        /// 事件处理程序中执行的后台操作使用的参数   
        /// </summary>   
        public object Argument { set; get; }
        /// <summary>   
        /// 获取当前导航到的文档位置   
        /// </summary>   
        public Uri Url { set; get; }
    }
    #endregion
}