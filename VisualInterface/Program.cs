using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualInterface
{
    static class Program
    {
        public static Random Randomizer = new Random();
        public static Presenter Presenter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            var currentHeight1 = (Math.Log(1, 3));
            var currentHeight2 = (Math.Log(2, 3));
            var currentHeight3 = (Math.Log(3, 3));
            var currentHeight4 = (Math.Log(4, 3));
            var currentHeight5 = (Math.Log(5, 3));
            var currentHeight6 = (Math.Log(6, 3));
            var currentHeight7 = (Math.Log(7, 3));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Presenter = new Presenter());
        }
    }
}
