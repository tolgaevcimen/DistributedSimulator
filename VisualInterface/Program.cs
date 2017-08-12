using System;
using System.Windows.Forms;

namespace VisualInterface
{
    static class Program
    {
        public static Presenter Presenter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Presenter = new Presenter());
        }
    }
}
