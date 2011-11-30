using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Cells.Utils;
using Cells.Properties;
using Cells.Controller;
using Cells.Interfaces;
using Ninject;

namespace Cells.View
{
    public partial class CellsCanvas : Form
    {
        private readonly Color defaultBackgroundColor = Color.Black;
        private readonly short pixelSize = Settings.Default.PixelSize;
        private readonly GameController controller;
        private readonly IDisplayController displayController;
        private readonly Graphics canvas;

        /// <summary>
        /// Constructor
        /// </summary>
        public CellsCanvas(GameController controller)
        {
            InitializeComponent();

            this.displayController = NinjectGlobalKernel.GlobalKernel.Get<IDisplayController>();

            this.controller = controller;
            this.canvas = DrawBox.CreateGraphics();
            this.canvas.Clear(defaultBackgroundColor);

            PopulateBrains();
            PopulateMaps();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateMaps()
        {
            string[] maps = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");

            foreach (String map in maps)
                this.lbMaps.Items.Add(Path.GetFileNameWithoutExtension(map));

            // Preselect the first brain
            if (this.lbMaps.Items.Count > 0)
                this.lbMaps.SetSelected(0, true);
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
            this.controller.ResetGame();
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
            this.PaintPixels(this.controller.GetPixelsToPaint());
        }

        /// <summary>
        /// Perform loading of the form
        /// </summary>
        private void CellsCanvasLoad(object sender, EventArgs e)
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
            this.tBDamageOnOpponent.Text = Settings.Default.DamageOnOpponent.ToString();
        }

        /// <summary>
        /// Save all the settings currently in the UI to the default settings file
        /// </summary>
        private void SaveSettingChanges()
        {
            Settings.Default.PixelSize = Convert.ToInt16(this.tBCellSize.Text);
            Settings.Default.CostOfCellDivision = Convert.ToInt16(this.tBCellDivisionCost.Text);
            Settings.Default.CellMaxInitialLife = Convert.ToInt16(this.tBCellInitialLife.Text);
            Settings.Default.SensoryViewSize = Convert.ToInt16(this.tBCellSensoryViewSize.Text);
            Settings.Default.MaxAltitude = Convert.ToInt16(this.tBMaxAltitude.Text);
            Settings.Default.MaxNumberOfCells = Convert.ToInt16(this.tBMaxNumberCells.Text);
            Settings.Default.MinAltitude = Convert.ToInt16(this.tBMinAltitude.Text);
            Settings.Default.NumberOfTeams = Convert.ToInt16(this.tBNumberOfTeams.Text);
            Settings.Default.SpawnLifeThreshold = Convert.ToInt16(this.tBSpawnLifeThreshold.Text);
            Settings.Default.InitialPopulationPerBrain = Convert.ToInt16(this.tBIntialPopPerBrain.Text);
            Settings.Default.DamageOnOpponent = Convert.ToInt16(this.tBDamageOnOpponent.Text);
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

        /// <summary>
        /// Display the map in the game area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
