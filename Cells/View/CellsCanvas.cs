using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cells.GameCore;
using Cells.Utils;
using Cells.Properties;
using Cells.Controller;
using Cells.Interfaces;

namespace Cells.View
{
    public partial class CellsCanvas : Form
    {
        private readonly Color _defaultBackgroundColor = Color.Black;
        private readonly short _pixelSize = Settings.Default.PixelSize;
        private readonly GameController _controller;
        readonly Graphics _canvas;

        /// <summary>
        /// Constructor
        /// </summary>
        public CellsCanvas(GameController controller)
        {
            InitializeComponent();

            _controller = controller;
            _canvas = DrawBox.CreateGraphics();
            _canvas.Clear(_defaultBackgroundColor);

            PopulateBrains();
        }

        /// <summary>
        /// Displays the available brains in the UI
        /// </summary>
        private void PopulateBrains()
        {
            foreach (String brainType in this._controller.GetAvailableBrainTypes())
                this.lBBrains.Items.Add(brainType);
        }

        #region CanvasPainting

        /// <summary>
        /// Paint a single pixel of color at the given coordinates
        /// </summary>
        /// <param name="coordvector">The coordinates where to paint</param>
        /// <param name="color">The color in which to paint</param>
        private void PaintSinglePixel(ICoordinates coordvector, Color color)
        {
            _canvas.FillRectangle(
                new SolidBrush(color),
                new Rectangle(coordvector.X * _pixelSize, coordvector.Y * _pixelSize, _pixelSize, _pixelSize));
        }

        /// <summary>
        /// Paints the list of pixels passed as a parameter
        /// </summary>
        /// <param name="modificationsDictionary">A list of KeyValuePair containting coordinates at which to paint and the color to use</param>
        private void PaintPixels(IEnumerable<KeyValuePair<ICoordinates, Color>> modificationsDictionary)
        {
            if (null == modificationsDictionary)
                return;
            
            foreach (KeyValuePair<ICoordinates, Color> keyValuePair in modificationsDictionary)
            {
                PaintSinglePixel(keyValuePair.Key, keyValuePair.Value);
            }
        }

        #endregion

        #region ButtonMgmt

        /// <summary>
        /// Is called when the button "start the engine" was pressed
        /// </summary>
        private void BStartEngineClick(object sender, EventArgs e)
        {
            _controller.StartGame();
        }

        /// <summary>
        /// Is called when the button "stop the engine" was pressed
        /// </summary>
        private void BStopEngineClick(object sender, EventArgs e)
        {
            _controller.StopGame();
            _canvas.Clear(_defaultBackgroundColor);
        }

        /// <summary>
        /// Is called when the button "save settings" was pressed
        /// </summary>
        private void BSaveSettingsClick(object sender, EventArgs e)
        {
            SaveSettingChanges();
        }

        #endregion

        /// <summary>
        /// This function renders what there is to display
        /// </summary>
        internal void RenderGame()
        {
            PaintPixels(_controller.GetUpdatedElements());
        }

        /// <summary>
        /// Perform loading of the form
        /// </summary>
        private void CellsCanvas_Load(object sender, EventArgs e)
        {
            PopulateUIFromSettings();
        }

        /// <summary>
        /// Populate the UI with the default settings file
        /// </summary>
        private void PopulateUIFromSettings()
        {
            this.tBCellDivisionCost.Text = Settings.Default.CostOfCellDivision.ToString();
            this.tBCellInitialLife.Text = Settings.Default.CellMaxInitialLife.ToString();
            this.tBCellSensoryViewSize.Text = Settings.Default.SensoryViewSize.ToString();
            this.tBCellSize.Text = Settings.Default.PixelSize.ToString();
            this.tBMaxAltitude.Text = Settings.Default.MaxAltitude.ToString();
            this.tBMaxNumberCells.Text = Settings.Default.MaxNumberOfCells.ToString();
            this.tBMinAltitude.Text = Settings.Default.MinAltitude.ToString();
            this.tBNumberOfTeams.Text = Settings.Default.NumberOfTeams.ToString();
            this.tBSpawnLifeThreshold.Text = Settings.Default.SpawnLifeThreshold.ToString();
            this.tBViewSize.Text = Settings.Default.SensoryViewSize.ToString();

            //this.tBDamageOnAggressiveOpponent.Text = Settings.Default..ToString();
            //this.tBDamageOnPassiveOpponent.Text = Settings.Default..ToString();
        }

        /// <summary>
        /// Save all the settings currently in the UI to the default settings file
        /// </summary>
        private void SaveSettingChanges()
        {
            Settings.Default.PixelSize = Convert.ToInt16(tBCellSize.Text);
            Settings.Default.CostOfCellDivision = Convert.ToInt16(tBCellDivisionCost.Text);
            Settings.Default.CellMaxInitialLife = Convert.ToInt16(tBCellInitialLife.Text);
            Settings.Default.SensoryViewSize = Convert.ToInt16(tBCellSensoryViewSize.Text);
            Settings.Default.MaxAltitude = Convert.ToInt16(tBMaxAltitude.Text);
            Settings.Default.MaxNumberOfCells = Convert.ToInt16(tBMaxNumberCells.Text);
            Settings.Default.MinAltitude = Convert.ToInt16(tBMinAltitude.Text);
            Settings.Default.NumberOfTeams = Convert.ToInt16(tBNumberOfTeams.Text);
            Settings.Default.SpawnLifeThreshold = Convert.ToInt16(tBSpawnLifeThreshold.Text);
            Settings.Default.SensoryViewSize = Convert.ToInt16(tBViewSize.Text);

            //Settings.Default. = Convert.ToInt16(tBDamageOnAggressiveOpponent.Text);
            //Settings.Default. = Convert.ToInt16(tBDamageOnPassiveOpponent.Text);
            
            Settings.Default.Save();
        }

        private void CellsCanvas_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._controller.StopGame();
            this._controller.Close();
        }
    }
}
