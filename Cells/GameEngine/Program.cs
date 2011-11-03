using System;
using System.Windows.Forms;

namespace Cells.GameEngine
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
            //Application.Run(new CellsCanvas());

            CellsCanvas gameCanvas = new CellsCanvas();
            gameCanvas.Show();
            gameCanvas.GameLoop();
        }
    }
}
