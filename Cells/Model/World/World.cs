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
using Cells.Model.Mapping;
using Cells.Brain;
using System.Diagnostics;

namespace Cells.GameCore
{
    /// <summary>
    /// Class representing the world, holding the maps and all the cells together
    /// </summary>
    public class World : IWorld
    {
        private Map masterMap;
        private IList<String> brains;
        
        /// <summary>
        /// List of all the cells currently in game
        /// </summary>
        private IList<ICell> cells = new List<ICell>();

        /// <summary>
        /// List of all the cells created during the round
        /// </summary>
        private IList<ICell> newCellsToAdd = new List<ICell>();

        /// <summary>
        /// List of all the cells that died during the round
        /// </summary>
        private readonly List<ICell> deadCellsToRemove = new List<ICell>();

        private readonly IDictionary<ICoordinates, Color> updatedElements = new ConcurrentDictionary<ICoordinates, Color>();
        private readonly IList<Color> availableColors = new List<Color>();
        private readonly IKernel worldKernel = new StandardKernel(new WorldModule());
        private readonly IKernel brainKernel = new StandardKernel();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            availableColors.Add(Color.Red);
            availableColors.Add(Color.Blue);
            availableColors.Add(Color.Yellow);
            availableColors.Add(Color.Green);
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        public void Initialize(IList<String> availableBrains)
        {
            this.brains = availableBrains;

            IKernel kernel = new StandardKernel(new CellModule());

            masterMap = new Map(Settings.Default.WorldWidth, Settings.Default.WorldHeight);
            cells = new List<ICell>();
            
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
            return cells;
        }

        /// <summary>
        /// Retrieves the list of updated elements
        /// </summary>
        /// <returns>A list of coordinates where elements were updated</returns>
        public IEnumerable<KeyValuePair<ICoordinates, Color>> GetUpdatedElements()
        {
            return updatedElements;
        }

        /// <summary>
        /// Registers the movement of the cell
        /// </summary>
        /// <param name="oldCoordinates">Coordinates where the cell was before it moved</param>
        /// <param name="newCoordinates">Coordinates where the cell is after it moved</param>
        /// <param name="team">Team Color of the cell</param>
        public void RegisterCellMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, Color team)
        {
            // Add the cell movements to the logs
            if (!updatedElements.ContainsKey(oldCoordinates))        
                updatedElements.Add(oldCoordinates, Color.Black);

            if (!updatedElements.ContainsKey(newCoordinates) || updatedElements[newCoordinates] == Color.Black)
                updatedElements.Add(newCoordinates, team);

            // Update the map
            this.masterMap.MoveCell(oldCoordinates, newCoordinates);
        }

        /// <summary>
        /// Clears the list caching the movement list
        /// </summary>
        public void ResetMovementsList()
        {
            updatedElements.Clear();
        }

        /// <summary>
        /// Function creating a view of the surroundings of the cell and returning it
        /// For anti-cheating purpose, the world gets the cells position himself instead of getting them as parameters
        /// </summary>
        /// <param name="cell">The cell asking</param>
        /// <returns>A SurroundingView of the location where the cell resides</returns>
        public SurroundingView GetSurroundingsView(ICell cell)
        {
            MapTile[,] map = masterMap.GetSubset(cell.Position, Settings.Default.SensoryViewSize, Settings.Default.SensoryViewSize);
            return new SurroundingView(cell.Position, map);
        }

        /// <summary>
        /// Increases the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to raise the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        public void RaiseLandscape(ICoordinates position)
        {
            if (masterMap.GetLandscapeHeight(position) >= Settings.Default.MaxAltitude)
                masterMap.RaiseLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its maximum at this location");
        }

        /// <summary>
        /// Lowers the landscape height at the given position
        /// </summary>
        /// <param name="position">The coordinates where to lower the landscape</param>
        /// <remarks>The function throws an InvalidOperationException in case the operation cannot be performed</remarks>
        public void LowerLandscape(ICoordinates position)
        {
            if (masterMap.GetLandscapeHeight(position) <= Settings.Default.MinAltitude)
                masterMap.LowerLandscape(position);
            else
                throw new InvalidOperationException("Landscape is already at its minimum at this location");
        }

        /// <summary>
        /// Increase the amount of ressources of the given amount at the given position
        /// </summary>
        /// <param name="position">The position where to perform the drop</param>
        /// <param name="life">The amount of ressources to drop</param>
        public void DropRessources(ICoordinates position, Int16 life)
        {
            masterMap.IncreaseRessources(position, life);
        }

        /// <summary>
        /// Flags the cell as "can be removed"
        /// </summary>
        /// <param name="cell">The cell to remove</param>
        /// <remarks>
        /// We do not remove the cell right away
        /// The cells are effectively removed at the end of the game loop
        /// </remarks>
        public void UnregisterCell(ICell cell)
        {
            deadCellsToRemove.Add(cell);
        }

        /// <summary>
        /// Cleans the public structures of all potentially remaining dead cells
        /// </summary>
        public void RemoveDeadCells()
        {
            foreach (Cell deadCell in deadCellsToRemove)
            {
                RejectCell(deadCell);
            }

            deadCellsToRemove.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deadCell"></param>
        private void RejectCell(Cell deadCell)
        {
            this.cells.Remove(deadCell);
            masterMap.RemoveCell(deadCell);

            if (updatedElements.ContainsKey(deadCell.Position))
            {
                updatedElements.Remove(deadCell.Position);
            }

            updatedElements.Add(deadCell.Position, Color.Black);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddNewlyCreatedCells()
        {
            foreach (Cell newCell in this.newCellsToAdd)
            {
                this.InjectCell(newCell);

                if (!updatedElements.ContainsKey(newCell.Position))
                    updatedElements.Add(newCell.Position, newCell.GetTeamColor());
                else
                {
                    if (updatedElements[newCell.Position] == Color.Black)
                        updatedElements[newCell.Position] = newCell.GetTeamColor();
                }                    
            }

            newCellsToAdd.Clear();
        }

        /// <summary>
        /// Creates a population of cells
        /// </summary>
        private void CreateInitialCellPopulation()
        {
            foreach (String brainType in this.brains)
            {
                Color teamColor = availableColors[RandomGenerator.GetRandomInt16((Int16)availableColors.Count)];
                availableColors.Remove(teamColor);

                CreateCellPopulation(brainType, Settings.Default.InitialPopulationPerBrain, teamColor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brainType"></param>
        /// <param name="numberOfCells"></param>
        /// <param name="teamColor"></param>
        private void CreateCellPopulation(String brainType, short numberOfCells, Color teamColor)
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                CreateCell(brainType, teamColor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brainType"></param>
        /// <param name="teamColor"></param>
        /// <param name="spawnLife"></param>
        /// <param name="position"></param>
        private void CreateCell(String brainType, Color teamColor, Int16? spawnLife = null, ICoordinates position = null)
        {
            var cell = this.worldKernel.Get<ICell>();

            var brain = this.brainKernel.Get(Type.GetType(brainType)) as IBrain;
            cell.SetBrain(brain);

            cell.Position = position ?? (cell.Position = GetRandomCoordinates());
            spawnLife = spawnLife ?? Settings.Default.CellMaxInitialLife;

            cell.SetLife(RandomGenerator.GetRandomInt16((Int16)spawnLife));
            cell.SetTeam(teamColor);

            RegisterNewCell(cell);
        }

        private ICoordinates GetRandomCoordinates()
        {
            ICoordinates coord = new Coordinates();
            coord.X = RandomGenerator.GetRandomInt16((Int16)(Settings.Default.WorldWidth - 1));
            coord.Y = RandomGenerator.GetRandomInt16((Int16)(Settings.Default.WorldHeight - 1));
            Debug.WriteLine(coord.X.ToString() + " " + coord.Y.ToString());
            return coord;
        }

        /// <summary>
        /// Register a new cell into the game
        /// </summary>
        /// <param name="newCell"></param>
        private void RegisterNewCell(ICell newCell)
        {
            this.newCellsToAdd.Add(newCell);
        }

        /// <summary>
        /// Injects the cell into the game 
        /// (should only be done by the world itself just before the begining of the turn)
        /// </summary>
        /// <param name="newCell">The cell</param>
        private void InjectCell(ICell newCell)
        {
            // Add the cell to the cell list
            this.cells.Add(newCell);

            // Implant the cell on the map
            this.masterMap.ImplantCell(newCell);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateRessourcesMap()
        {
            masterMap.ImplantRessources(GetRandomCoordinates(), 500, 0);
            masterMap.ImplantRessources(GetRandomCoordinates(), 100, 0);
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 0);
            masterMap.ImplantRessources(GetRandomCoordinates(), 5, 0);
            masterMap.ImplantRessources(GetRandomCoordinates(), 500, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreatePlantMap()
        {
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
            masterMap.ImplantRessources(GetRandomCoordinates(), 50, 5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spawnLife"></param>
        /// <param name="cell"></param>
        public void CreateSpawns(short spawnLife, Cell cell)
        {
            this.CreateCell(cell.GetAttachedBrainType(), cell.GetTeamColor(), spawnLife, cell.Position);
            this.CreateCell(cell.GetAttachedBrainType(), cell.GetTeamColor(), spawnLife, cell.Position);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class WorldModule : NinjectModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            Bind<ICell>().To<Cell>();
        }
    }
}
