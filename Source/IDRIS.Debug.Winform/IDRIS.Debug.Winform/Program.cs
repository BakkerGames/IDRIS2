// Program.cs - 08/02/2018

using System;
using System.Windows.Forms;

namespace IDRIS.Debug.Winform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DebugForm myForm = new DebugForm();
            Application.Run(myForm);
        }
    }
}
