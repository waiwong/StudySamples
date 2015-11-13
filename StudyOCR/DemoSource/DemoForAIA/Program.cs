using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BWHITD.Win.Lib;

namespace DemoForAIA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            CustomExceptionHandler eh = new CustomExceptionHandler();
            Application.ThreadException += eh.OnThreadException;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
