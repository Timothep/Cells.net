using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cells.GameCore;
using Cells.Utils;

namespace Cells.GameEngine
{
    public partial class CellsCanvas : Form
    {
        private Color DefaultBackgroundColor = Color.Black;
        private const long GameLoopLength = 50;
        private const short PixelWidth = 1;
        private const short PixelHeight = 1;

        readonly Timer _timer = new Timer();
        readonly Graphics _canvas;
        
        // The engine game, dedicated to running the game loops
        private readonly GameCoreEngine _gameEngine = new GameCoreEngine();

        public CellsCanvas()
        {
            InitializeComponent();
            _canvas = DrawBox.CreateGraphics();
            _canvas.Clear(DefaultBackgroundColor);
        }

        #region CanvasPainting

        /// <summary>
        /// Paint a single pixel of color at the given coordinates
        /// </summary>
        /// <param name="coordvector">The coordinates where to paint</param>
        /// <param name="color">The color in which to paint</param>
        private void PaintSinglePixel(Coordinates coordvector, Color color)
        {
            _canvas.DrawRectangle(new Pen(color), coordvector.X, coordvector.Y, PixelWidth, PixelHeight);
        }

        /// <summary>
        /// Paints the list of pixels passed as a parameter
        /// </summary>
        /// <param name="modificationsDictionary">A list of KeyValuePair containting coordinates at which to paint and the color to use</param>
        private void PaintPixels(IEnumerable<KeyValuePair<Coordinates, Color>> modificationsDictionary)
        {
            if (null == modificationsDictionary)
                return;
            
            foreach (KeyValuePair<Coordinates, Color> keyValuePair in modificationsDictionary)
            {
                PaintSinglePixel(keyValuePair.Key, keyValuePair.Value);
            }
        }

        #endregion

        #region ButtonMgmt

        private void BStartEngineClick(object sender, EventArgs e)
        {
            _gameEngine.StartGame();   
        }

        private void BStopEngineClick(object sender, EventArgs e)
        {
            _gameEngine.StopGame();
            _canvas.Clear(DefaultBackgroundColor);
        }

        #endregion

        /// <summary>
        /// This function loops infinitely
        /// </summary>
        internal void GameLoop()
        {
            while (Created)
            {
                _timer.Reset();
                GameLogic();
                RenderGame();
                Application.DoEvents();
                while (_timer.GetTicks() < GameLoopLength) {}
            }
        }

        /// <summary>
        /// This function calls the main logic of the game
        /// </summary>
        private void GameLogic()
        {
            _gameEngine.Loop();
        }

        /// <summary>
        /// This function renders what there is to display
        /// </summary>
        private void RenderGame()
        {
            PaintPixels(_gameEngine.GetUpdatedElements());
        }
    }
}
