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
        private readonly Color defaultBackgroundColor = Color.Black;
        private readonly short pixelSize = Settings.Default.PixelSize;
        private readonly GameController controller;
        private readonly Graphics canvas;

        /// <summary>
        /// Constructor
        /// </summary>
        public CellsCanvas(GameController controller)
        {
            InitializeComponent();

            this.controller = controller;
            this.canvas = DrawBox.CreateGraphics();
            this.canvas.Clear(defaultBackgroundColor);

            PopulateBrains();
        }

        /// <summary>
        /// Displays the available brains in the UI
        /// </summary>
        private void PopulateBrains()
        {
            foreach (String brainType in this.controller.GetAvailableBrainTypes())
                this.lBBrains.Items.Add(brainType);

            // Preselect the two first brains if present
            if (this.lBBrains.Items.Count > 0)
                this.lBBrains.SetSelected(0, true);

            if (this.lBBrains.Items.Count > 1)
                this.lBBrains.SetSelected(1, true);
        }

        /// <summary>
        /// Paint a single pixel of color at the given coordinates
        /// </summary>
        /// <param name="coordvector">The coordinates where to paint</param>
        /// <param name="color">The color in which to paint</param>
        private void PaintSinglePixel(ICoordinates coordvector, Color color)
        {
            this.canvas.FillRectangle(
                new SolidBrush(color),
                new Rectangle(coordvector.X * this.pixelSize, coordvector.Y * this.pixelSize, this.pixelSize, this.pixelSize));
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

        /// <summary>
        /// Is called when the button "start the engine" was pressed
        /// </summary>
        private void BStartEngineClick(object sender, EventArgs e)
        {
            this.controller.StartGame();
        }

        /// <summary>
        /// Is called when the button "stop the engine" was pressed
        /// </summary>
        private void BStopEngineClick(object sender, EventArgs e)
        {
            this.controller.StopGame();
            this.canvas.Clear(this.defaultBackgroundColor);
        }

        /// <summary>
        /// Is called when the button "save settings" was pressed
        /// </summary>
        private void BSaveSettingsClick(object sender, EventArgs e)
        {
            SaveSettingChanges();
        }

        /// <summary>
        /// This function renders what there is to display
        /// </summary>
        internal void RenderGame()
        {
            PaintPixels(this.controller.GetPixelsToPaint());
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
            this.tBIntialPopPerBrain.Text = Settings.Default.InitialPopulationPerBrain.ToString();
            this.tBDamageOnAggressiveOpponent.Text = Settings.Default.DamageOnAggressiveOpponent.ToString();
            this.tBDamageOnPassiveOpponent.Text = Settings.Default.DamageOnPassiveOpponent.ToString();
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
            Settings.Default.InitialPopulationPerBrain = Convert.ToInt16(tBIntialPopPerBrain.Text);
            Settings.Default.DamageOnAggressiveOpponent = Convert.ToInt16(tBDamageOnAggressiveOpponent.Text);
            Settings.Default.DamageOnPassiveOpponent = Convert.ToInt16(tBDamageOnPassiveOpponent.Text);
            Settings.Default.Save();

            if (Settings.Default.SensoryViewSize % 2 == 0)
                MessageBox.Show("Warning, the sensory view size must be an odd number");
        }

        /// <summary>
        /// Cleanup the open instances upon form close
        /// </summary>
        private void CellsCanvas_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.controller.StopGame();
            this.controller.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<string> GetSelectedBrains()
        {
            IList<String> list = new List<String>();
            foreach (object selectedItem in this.lBBrains.SelectedItems)
                list.Add(selectedItem.ToString());

            return list;
        }
    }
}
