using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;
using Cells.Interfaces;
using Cells.Properties;
using Cells.Utils;
using Ninject;
using Ninject.Modules;

namespace Cells.GameCore
{
    /// <summary>
    /// Class representing the world, holding the maps and all the cells together
    /// </summary>
    public class World : IWorld
    {
        private readonly Map _masterMap;
        private readonly List<String> _brains;
        private readonly List<ICell> _cells = new List<ICell>();
        private readonly List<ICell> _deadCellsToRemove = new List<ICell>();
        private readonly IDictionary<Coordinates, Color> _updatedElements = new ConcurrentDictionary<Coordinates, Color>();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            _masterMap = new Map(Settings.Default.WorldWidth, Settings.Default.WorldHeight);
            _cells = new List<ICell>();

            Initialize();
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        private void Initialize()
        {
            CreateInitialCellPopulation();
            CreatePlantMap();
            CreateRessourcesMap();
        }

        /// <summary>
        /// Returns a list of all the cells present in the game
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICell> GetCells()
        {
            return _cells;
        }

        /// <summary>
        /// Retrieves the list of updated elements
        /// </summary>
        /// <returns>A list of coordinates where elements were updated</returns>
        public IEnumerable<KeyValuePair<Coordinates, Color>> GetUpdatedElements()
        {
            return _updatedElements;
        }

        /// <summary>
        /// Registers the movement of the cell
        /// </summary>
        /// <param name="oldCoordinates">Coordinates where the cell was before it moved</param>
        /// <param name="newCoordinates">Coordinates where the cell is after it moved</param>
        /// <param name="team">Team Color of the cell</param>
        public void RegisterCellMovement(Coordinates oldCoordinates, Coordinates newCoordinates, Color team)
        {
            // Add the cell movements to the logs
            if (!_updatedElements.ContainsKey(oldCoordinates))        
                _updatedElements.Add(oldCoordinates, Color.Black);

            if (_updatedElements.ContainsKey(newCoordinates) && _updatedElements[newCoordinates] != Color.Black)
            {
                throw new InvalidOperationException("Trying to move a cell to a position where a cell already resides");
            }
            else
                _updatedElements.Add(newCoordinates, team);

            // Update the map
            this._masterMap.MoveCell(oldCoordinates, newCoordinates);
        }

        /// <summary>
        /// Clears the list caching the movement list
        /// </summary>
        public void ResetMovementsList()
        {
            _updatedElements.Clear();
        }

        /// <summary>
        /// Function creating a view of the surroundings of the cell and returning it
        /// For anti-cheating purpose, the world gets the cells position himself instead of getting them as parameters
        /// </summary>
        /// <param name="cell">The cell asking</param>
        /// <returns>A SurroundingView of the location where the cell resides</returns>
        public SurroundingView GetSurroundingsView(Cell cell)
        {
            MapTile[,] map = this._masterMap.GetSubset(cell.Position, Settings.Default.SensoryViewSize, Settings.Default.SensoryViewSize);
            return new SurroundingView(cell.Position, map);
        }

        /// <summary>
        /// Increases the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to raise the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        public void RaiseLandscape(Coordinates position)
        {
            if (_masterMap.GetLandscapeHeight(position) >= Settings.Default.MaxAltitude)
                _masterMap.RaiseLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its maximum at this location");
        }

        /// <summary>
        /// Lowers the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to lower the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        public void LowerLandscape(Coordinates position)
        {
            if (_masterMap.GetLandscapeHeight(position) <= Settings.Default.MinAltitude)
                _masterMap.LowerLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its minimum at this location");
        }

        /// <summary>
        /// Increase the amount of ressources of the given amount at the given position
        /// </summary>
        /// <param name="position">The position where to perform the drop</param>
        /// <param name="life">The amount of ressources to drop</param>
        public void DropRessources(Coordinates position, Int16 life)
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
        public void UnregisterCell(Cell cell)
        {
            _deadCellsToRemove.Add(cell);
        }

        /// <summary>
        /// Cleans the public structures of all potentially remaining dead cells
        /// </summary>
        public void RemoveDeadCells()
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
            CreateCellPopulation(Settings.Default.InitialPopulationPerTeam, Color.Red);
            CreateCellPopulation(Settings.Default.InitialPopulationPerTeam, Color.Yellow);
        }

        private void CreateCellPopulation(short numberOfCells, Color teamColor)
        {
            Ninject.IKernel kernel = new StandardKernel(new WorldModule());

            for (int i = 0; i < numberOfCells; i++)
            {
                var cell = kernel.Get<ICell>();
                InjectCell(cell);

                //Coordinates newCoordinates = GetRandomCoordinates();
                //Int16 initialLife = (Int16)RandomGenerator.GetRandomInteger(Settings.Default.CellMaxInitialLife);
                //var cell = new Cell();//newCoordinates.X, newCoordinates.Y, initialLife, this, teamColor);
            }
        }

        private Coordinates GetRandomCoordinates()
        {
            return new Coordinates(
                RandomGenerator.GetRandomInt16((Int16)(Settings.Default.WorldWidth - 1)), 
                RandomGenerator.GetRandomInt16((Int16)(Settings.Default.WorldHeight - 1)));
        }

        /// <summary>
        /// Injects the cell into the game
        /// </summary>
        /// <param name="newCell">The cell</param>
        private void InjectCell(ICell newCell)
        {
            // Add the cell to the cell list
            _cells.Add(newCell);

            // Implant the cell on the map
            _masterMap.ImplantCell(newCell);
        }

        private void CreateRessourcesMap()
        {
            _masterMap.ImplantRessources(GetRandomCoordinates(), 500, 0);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 100, 0);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 0);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 5, 0);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 500, 0);
        }

        private void CreatePlantMap()
        {
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            _masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
        }

        //public void CreateSpawns(short spawnLife, Cell cell)
        //{
        //    this.InjectCell(new Cell(cell.Position.X, cell.Position.Y, spawnLife, this, cell.GetTeamColor()));
        //    this.InjectCell(new Cell(cell.Position.X, cell.Position.Y, spawnLife, this, cell.GetTeamColor()));
        //}
    }

    internal class WorldModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICell>().To<Cell>();
        }
    }
}
