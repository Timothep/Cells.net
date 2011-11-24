using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Cells.Interfaces;
using Cells.Utils;
using Cells.GameCore.Cells;
using Ninject;
using Ninject.Modules;
using Cells.Brain;
using Cells.GameCore;
using Cells.View;
using System.Windows.Forms;

namespace Cells.Controller
{
    public class GameController
    {
        // Game loop stuff
        private const long GameLoopLength = 50;
        readonly Cells.Controller.Timer _timer = new Cells.Controller.Timer();

        // List of the length of each cycle (for stats and performance purpose)
        readonly List<Double> _cycleLength = new List<Double>();

        // Brain broker gathering brains via MEF
        BrainDiscoveryManager bDM = new BrainDiscoveryManager();

        // The view
        CellsCanvas _view;

        // The world where all is happening
        World _world;
        private bool _running = true;

        public GameController()
        {
            _view = new CellsCanvas(this);
            _view.Show();
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
                _timer.Reset();
                Loop();
                _view.RenderGame();
                Application.DoEvents();
                while (_timer.GetTicks() < GameLoopLength) { }
            }
        }

        /// <summary>
        /// Start the CoreEngine (this will create the initial population)
        /// </summary>
        public void StartGame()
        {
            IKernel kernel = new StandardKernel(new GameCoreEngineModule());
            _world = kernel.Get<World>();
        }

        /// <summary>
        /// Stop the engine (this will destroy all the living cells)
        /// </summary>
        public void StopGame()
        {
            _world = null;
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
            if (null == _world)
            {
                return;
            }

            _world.ResetMovementsList();

            IEnumerable<ICell> allCells = _world.GetCells();
            Debug.WriteLine("Number of alive cells: " + allCells.Count().ToString());

            if (null != allCells)
            {
                foreach (ICell currentCell in _world.GetCells())
                {
                    //// Death comes first
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

            _world.RemoveDeadCells();
        }

        /// <summary>
        /// Gets a list of the coordinates where a cell shall be painted
        /// </summary>
        /// <returns>A list of coordinates and the team it belongs to</returns>
        public IEnumerable<KeyValuePair<Coordinates, Color>> GetUpdatedElements()
        {
            if (null == _world)
                return null;
            
            return _world.GetUpdatedElements();
        }

        internal IEnumerable<String> GetAvailableBrainTypes()
        {
            return this.bDM.GetAvailableBrainTypes();
        }

        internal void Close()
        {
            this._running = false;
        }
    }

    internal class GameCoreEngineModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>().InSingletonScope();
        }

        
    }

}
