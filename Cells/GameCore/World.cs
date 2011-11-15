using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;
using Cells.Properties;
using Cells.Utils;

namespace Cells.GameCore
{
    /// <summary>
    /// Class representing the world, holding the maps and all the cells together
    /// </summary>
    public class World
    {
        private readonly Int16 _worldWidth = Settings.Default.WorldWidth;
        private readonly Int16 _worldHeight = Settings.Default.WorldHeight;
        private readonly Int16 _subViewSize = Settings.Default.SubViewSize;
        private readonly Int16 _minLandscapeHeight = Settings.Default.MinLandscapeHeight;
        private readonly Int16 _maxLandscapeHeight = Settings.Default.MaxLandscapeHeight;

        private readonly Map _masterMap;
        private readonly List<Cell> _cells = new List<Cell>();
        private readonly List<Cell> _deadCellsToRemove = new List<Cell>();

        private readonly IDictionary<Coordinates, Color> _updatedElements =
            new ConcurrentDictionary<Coordinates, Color>();

        /// <summary>
        /// Constructor
        /// </summary>
        internal World()
        {
            // Create the empty map of the world
            _masterMap = new Map(_worldWidth, _worldHeight);

            _cells = new List<Cell>();
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        internal void Initialize()
        {
            CreateInitialCellPopulation();

            CreatePlantMap();

            CreateRessourcesMap();
        }

        /// <summary>
        /// Returns a list of all the cells present in the game
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Cell> GetCells()
        {
            return _cells;
        }

        /// <summary>
        /// Retrieves the list of updated elements
        /// </summary>
        /// <returns>A list of coordinates where elements were updated</returns>
        internal IEnumerable<KeyValuePair<Coordinates, Color>> GetUpdatedElements()
        {
            return _updatedElements;
        }

        /// <summary>
        /// Registers the movement of the cell
        /// </summary>
        /// <param name="oldCoordinates">Coordinates where the cell was before it moved</param>
        /// <param name="newCoordinates">Coordinates where the cell is after it moved</param>
        /// <param name="team">Team Color of the cell</param>
        internal void RegisterCellMovement(Coordinates oldCoordinates, Coordinates newCoordinates, Color team)
        {
            // Add the cell movements to the logs
            _updatedElements.Add(oldCoordinates, Color.Black);
            _updatedElements.Add(newCoordinates, team);

            // Update the map
            this._masterMap.MoveCell(oldCoordinates, newCoordinates);
        }

        /// <summary>
        /// Clears the list caching the movement list
        /// </summary>
        internal void ResetMovementsList()
        {
            _updatedElements.Clear();
        }

        /// <summary>
        /// Function creating a view of the surroundings of the cell and returning it
        /// For anti-cheating purpose, the world gets the cells position himself instead of getting them as parameters
        /// </summary>
        /// <param name="cell">The cell asking</param>
        /// <returns>A SurroundingView of the location where the cell resides</returns>
        internal SurroundingView GetSurroundingsView(Cell cell)
        {
            return new SurroundingView(cell.Position,
                                       this._masterMap.GetSubset(cell.Position, _subViewSize, _subViewSize));
        }

        /// <summary>
        /// Increases the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to raise the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        internal void RaiseLandscape(Coordinates position)
        {
            if (_masterMap.GetLandscapeHeight(position) >= _maxLandscapeHeight)
                _masterMap.RaiseLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its maximum at this location");
        }

        /// <summary>
        /// Lowers the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to lower the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        internal void LowerLandscape(Coordinates position)
        {
            if (_masterMap.GetLandscapeHeight(position) <= _minLandscapeHeight)
                _masterMap.LowerLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its minimum at this location");
        }

        /// <summary>
        /// Increase the amount of ressources of the given amount at the given position
        /// </summary>
        /// <param name="position">The position where to perform the drop</param>
        /// <param name="life">The amount of ressources to drop</param>
        internal void DropRessources(Coordinates position, Int16 life)
        {
            _masterMap.IncreaseRessources(position, life);
        }

        /// <summary>
        /// Flags the cell as "can be removed"
        /// </summary>
        /// <param name="cell">The cell to remove</param>
        /// <remarks>
        /// We do not remove the cell right away
        /// The cells are effectively removed at the end of the game loop
        /// </remarks>
        internal void UnregisterCell(Cell cell)
        {
            _deadCellsToRemove.Add(cell);
        }

        /// <summary>
        /// Cleans the internal structures of all potentially remaining dead cells
        /// </summary>
        internal void RemoveDeadCells()
        {
            foreach (Cell deadCell in _deadCellsToRemove)
            {
                _cells.Remove(deadCell);
                _masterMap.RemoveCell(deadCell);

                if (_updatedElements.ContainsKey(deadCell.Position))
                {
                    _updatedElements.Remove(deadCell.Position);
                }
                _updatedElements.Add(deadCell.Position, Color.Black);
            }

            _deadCellsToRemove.Clear();
        }

        /// <summary>
        /// Creates a population of cells
        /// </summary>
        private void CreateInitialCellPopulation()
        {
            for (int i = 100; i < 120; i++)
            {
                for (int j = 100; j < 120; j++)
                {
                    // Create a brand new cell
                    Color color = i%2 == 0 ? Color.Yellow : Color.Red;
                    var newCell = new Cell(i, j, Convert.ToInt16(RandomGenerator.GetRandomInteger(50)), this, color);

                    InjectCell(newCell);
                }
            }
        }

        /// <summary>
        /// Injects the cell into the game
        /// </summary>
        /// <param name="newCell">The cell</param>
        private void InjectCell(Cell newCell)
        {
            // Add the cell to the cell list
            _cells.Add(newCell);

            // Implant the cell on the map
            _masterMap.ImplantCell(newCell);
        }

        private void CreateRessourcesMap()
        {
            _masterMap.ImplantRessources(new Coordinates(97, 97), 500, 0);
            _masterMap.ImplantRessources(new Coordinates(125, 125), 100, 0);
            _masterMap.ImplantRessources(new Coordinates(97, 117), 50, 0);
            _masterMap.ImplantRessources(new Coordinates(97, 125), 5, 0);
            _masterMap.ImplantRessources(new Coordinates(125, 100), 500, 0);
        }

        private void CreatePlantMap()
        {
            _masterMap.ImplantRessources(new Coordinates(99, 99), 50, 5);
            _masterMap.ImplantRessources(new Coordinates(123, 123), 50, 5);
            _masterMap.ImplantRessources(new Coordinates(99, 115), 50, 5);
            _masterMap.ImplantRessources(new Coordinates(99, 120), 50, 5);
            _masterMap.ImplantRessources(new Coordinates(123, 100), 50, 5);
        }

        internal void CreateSpawns(short spawnLife, Cell cell)
        {
            this.InjectCell(new Cell(cell.Position.X, cell.Position.Y, spawnLife, this, cell.GetTeamColor()));
            this.InjectCell(new Cell(cell.Position.X, cell.Position.Y, spawnLife, this, cell.GetTeamColor()));
        }
    }
}
