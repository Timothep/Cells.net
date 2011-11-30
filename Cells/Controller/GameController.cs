using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.Interfaces;
using Cells.Model;
using Cells.Model.Brain;
using Cells.Utils;
using Ninject;
using Cells.View;
using System.Windows.Forms;

namespace Cells.Controller
{
    public class GameController
    {
        // Game loop stuff
        private const long GameLoopLength = 50;
        readonly Cells.Controller.Timer timer = new Cells.Controller.Timer();

        // List of the length of each cycle (for stats and performance purpose)
        readonly List<Double> cycleLength = new List<Double>();

        // Brain broker gathering brains via MEF
        BrainDiscoveryManager bDM = new BrainDiscoveryManager();

        private IDisplayController displayController;

        // The view
        CellsCanvas view;

        // The world where all is happening
        IWorld world;
        private bool _running = true;

        private IEnumerable<String> selectedBrainList;

        public GameController()
        {
            this.view = new CellsCanvas(this);
            this.view.Show();
            this.displayController  =   NinjectGlobalKernel.GlobalKernel.Get<IDisplayController>();
            this.world              =   NinjectGlobalKernel.GlobalKernel.Get<IWorld>();
        }

        /// <summary>
        /// Function starting the game loop
        /// </summary>
        internal void Run()
        {
            GameLoop();
        }

        /// <summary>
        /// This function loops infinitely
        /// </summary>
        internal void GameLoop()
        {
            while (this._running)
            {
                this.timer.Reset();
                Loop();
                this.view.RenderGame();
                Application.DoEvents();
                while (this.timer.GetTicks() < GameLoopLength) { }
            }
        }

        /// <summary>
        /// Start the CoreEngine (this will create the initial population)
        /// </summary>
        public void StartGame()
        {
            this.selectedBrainList = this.view.GetSelectedBrains();
            this.world.Initialize(this.selectedBrainList as IList<String>);

            // Render the background and paint it
            this.displayController.PaintWholeBackground();
            this.displayController.GetPaintJobs();
            this.view.RenderGame();
        }

        /// <summary>
        /// Stop the engine (this will destroy all the living cells)
        /// </summary>
        public void StopGame()
        {
            if(this.world != null)
                NinjectGlobalKernel.GlobalKernel.Release(this.world);

            this.world = NinjectGlobalKernel.GlobalKernel.Get<IWorld>(); ;
        }

        /// <summary>
        /// This loop function encapsulates the real loop and takes FPS statistics at the same time
        /// </summary>
        public void Loop()
        {
            Tick();
        }

        /// <summary>
        /// This is the main loop of the game
        /// Calls each cell and gives it some time to think
        /// Checks that the action is indeed valid
        /// Applies the resulting action to the cell
        /// </summary>
        private void Tick()
        {
            if (null == this.world)
                return;

            // Reset
            this.world.AddNewlyCreatedCellsToTheGame();
            this.world.ResetMovementsList();

            IEnumerable<IInternalCell> allCells = this.world.GetCells();

            if (null != allCells)
            {
                foreach (IInternalCell currentCell in this.world.GetCells())
                {
                    // Death comes first
                    currentCell.DecreaseLife();
                    if (currentCell.GetLife() <= 0)
                    {
                        currentCell.Die();
                        continue;
                    }

                    // Alive cells get to do something
                    CellAction action = currentCell.Think();
                    currentCell.Do(action);
                }
            }

            this.world.RemoveDeadCells();
        }

        /// <summary>
        /// Gets a list of the coordinates where a cell shall be painted
        /// </summary>
        /// <returns>A list of coordinates and the team it belongs to</returns>
        public IEnumerable<KeyValuePair<ICoordinates, Color>> GetPixelsToPaint()
        {
            if (null == this.world)
                return null;
            
            return this.displayController.GetPaintJobs();
        }

        /// <summary>
        /// This function returns an IEnumerable containing the available brain types that were discovered
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<String> GetAvailableBrainTypes()
        {
            return this.bDM.GetAvailableBrainTypes();
        }

        /// <summary>
        /// Signals to the game contoller that the game loop has to be stopped
        /// </summary>
        internal void Close()
        {
            this._running = false;
        }

        internal void ResetGame()
        {
            this.world.Reset();
        }
    }
}
