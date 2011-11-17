using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cells.GameCore;
using Cells.Utils;
using Cells.Properties;

namespace Cells.GameEngine
{
    public partial class CellsCanvas : Form
    {
        private readonly Color _defaultBackgroundColor = Color.Black;
        private const long GameLoopLength = 50;
        private readonly short _pixelSize = Settings.Default.PixelSize;

        readonly Timer _timer = new Timer();
        readonly Graphics _canvas;
        
        // The engine game, dedicated to running the game loops
        private readonly GameCoreEngine _gameEngine = new GameCoreEngine();

        public CellsCanvas()
        {
            InitializeComponent();
            _canvas = DrawBox.CreateGraphics();
            _canvas.Clear(_defaultBackgroundColor);

            //Create BrainsList
            List<String> brains = _gameEngine.GetBrainsList();
            foreach (var brain in brains)
            {
                lBBrains.Items.Add(brain);
            }
        }

        #region CanvasPainting

        /// <summary>
        /// Paint a single pixel of color at the given coordinates
        /// </summary>
        /// <param name="coordvector">The coordinates where to paint</param>
        /// <param name="color">The color in which to paint</param>
        private void PaintSinglePixel(Coordinates coordvector, Color color)
        {
            _canvas.FillRectangle(
                new SolidBrush(color),
                new Rectangle(coordvector.X * _pixelSize, coordvector.Y * _pixelSize, _pixelSize, _pixelSize));
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
            _canvas.Clear(_defaultBackgroundColor);
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

        private void CellsCanvas_Load(object sender, EventArgs e)
        {
            PopulateUIFromSettings();
        }

        private void PopulateUIFromSettings()
        {
            this.tBCellDivisionCost.Text = Settings.Default.CostOfCellDivision.ToString();
            this.tBCellInitialLife.Text = Settings.Default.CellMaxInitialLife.ToString();
            this.tBCellSensoryViewSize.Text = Settings.Default.SensoryViewSize.ToString();
            this.tBCellSize.Text = Settings.Default.PixelSize.ToString();
            //this.tBDamageOnAggressiveOpponent.Text = Settings.Default..ToString();
            //this.tBDamageOnPassiveOpponent.Text = Settings.Default..ToString();
            this.tBMaxAltitude.Text = Settings.Default.MaxAltitude.ToString();
            this.tBMaxNumberCells.Text = Settings.Default.MaxNumberOfCells.ToString();
            this.tBMinAltitude.Text = Settings.Default.MinAltitude.ToString();
            this.tBNumberOfTeams.Text = Settings.Default.NumberOfTeams.ToString();
            this.tBSpawnLifeThreshold.Text = Settings.Default.SpawnLifeThreshold.ToString();
            this.tBViewSize.Text = Settings.Default.SensoryViewSize.ToString();
        }

        private void SaveSettingChanges()
        {
            Settings.Default.PixelSize = Convert.ToInt16(tBCellSize.Text);
            //Settings.Default. = Convert.ToInt16(tBDamageOnAggressiveOpponent.Text);
            //Settings.Default. = Convert.ToInt16(tBDamageOnPassiveOpponent.Text);

            Settings.Default.CostOfCellDivision = Convert.ToInt16(tBCellDivisionCost.Text);
            Settings.Default.CellMaxInitialLife = Convert.ToInt16(tBCellInitialLife.Text);
            Settings.Default.SensoryViewSize = Convert.ToInt16(tBCellSensoryViewSize.Text);
            Settings.Default.MaxAltitude = Convert.ToInt16(tBMaxAltitude.Text);
            Settings.Default.MaxNumberOfCells = Convert.ToInt16(tBMaxNumberCells.Text);
            Settings.Default.MinAltitude = Convert.ToInt16(tBMinAltitude.Text);
            Settings.Default.NumberOfTeams = Convert.ToInt16(tBNumberOfTeams.Text);
            Settings.Default.SpawnLifeThreshold = Convert.ToInt16(tBSpawnLifeThreshold.Text);
            Settings.Default.SensoryViewSize = Convert.ToInt16(tBViewSize.Text);
            
            Settings.Default.Save();
        }

        private void BSaveSettingsClick(object sender, EventArgs e)
        {
            SaveSettingChanges();
        }
    }
}
