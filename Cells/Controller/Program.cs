using System;
using System.Windows.Forms;

namespace Cells.Controller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameController gameController = new GameController();

            gameController.Run();
        }
    }
}
