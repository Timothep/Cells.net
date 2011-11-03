using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Cells.Utils;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;

namespace Cells.GameCore
{
    public class GameCoreEngine
    {
        // List of the length of each cycle (for stats and performance purpose)
        readonly List<Double> _cycleLength = new List<Double>();
        
        // The world where all is happening
        World _world;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameCoreEngine()
        {
            
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
                return;

            _world.ResetMovementsList();

            IEnumerable<Cell> allCells = _world.GetCells();

            if (null != allCells)
            {
                foreach (Cell currentCell in _world.GetCells())
                {
                    CellAction action = currentCell.Think();
                    currentCell.Do(action);
                }
            }
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
    }
}
