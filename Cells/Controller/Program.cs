using System;
using System.Windows.Forms;
using Cells.View;
using Cells.Controller;

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
            GameController _gameController = new GameController();

            _gameController.Run();
        }
    }
}
