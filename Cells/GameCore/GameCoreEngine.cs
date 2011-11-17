using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Cells.Interfaces;
using Cells.Utils;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;

namespace Cells.GameCore
{
    public class GameCoreEngine : IPartImportsSatisfiedNotification
    {
        // List of the length of each cycle (for stats and performance purpose)
        readonly List<Double> _cycleLength = new List<Double>();

        [ImportMany]
        public IEnumerable<IBrain> Brains { get; set; }

        // The world where all is happening
        World _world;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameCoreEngine()
        {
            Compose();
        }

        private void Compose()
        {
            // AssemblyCatalog takes an assembly and  looks for all Imports and Exports within it
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

            // AggregateCatalog holds multiple  ComposablePartCatalogs
            var aggregator = new AggregateCatalog();
            aggregator.Catalogs.Add(assemblyCatalog);

            var container = new CompositionContainer(aggregator);
            container.ComposeParts(this);
        }

        public void OnImportsSatisfied()
        {
            foreach (var brain in this.Brains)
            {
                
            }
        }

        /// <summary>
        /// Start the CoreEngine (this will create the initial population)
        /// </summary>
        public void StartGame()
        {
            _world = new World();
            _world.Initialize();
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

            IEnumerable<Cell> allCells = _world.GetCells();
            Debug.WriteLine("Number of alive cells: " + allCells.Count().ToString());

            if (null != allCells)
            {
                foreach (Cell currentCell in _world.GetCells())
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

        /// <summary>
        /// Get the names of all the brains that were discovered by MEF
        /// </summary>
        /// <returns>A list of all the names</returns>
        internal List<string> GetBrainsList()
        {
            List<String> lsBrains = new List<string>();

            lsBrains.AddRange(Brains.Select(brain => brain.GetType().ToString()));

            return lsBrains;
        }
    }
}
