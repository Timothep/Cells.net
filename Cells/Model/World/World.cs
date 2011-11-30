using System;
using System.Collections.Generic;
using System.Drawing;
using Cells.Interfaces;
using Cells.Model.Cells;
using Cells.Properties;
using Cells.Utils;
using Ninject;
using Ninject.Modules;
using Cells.Model.Mapping;
using System.Diagnostics;

namespace Cells.Model.World
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
        private IList<IInternalCell> cells = new List<IInternalCell>();

        /// <summary>
        /// List of all the cells created during the round
        /// </summary>
        private readonly IList<IInternalCell> newCellsToAdd = new List<IInternalCell>();

        private IMapFactory mapFactory;

        /// <summary>
        /// List of all the cells that died during the round
        /// </summary>
        private readonly List<IInternalCell> deadCellsToRemove = new List<IInternalCell>();

        private readonly IList<DisplayQualifier> availableTeams = new List<DisplayQualifier>();

        private readonly IKernel localKernel = new StandardKernel(new localModule());
        private readonly IKernel globalKernel = new StandardKernel();

        private IDisplayController displayController;

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            this.displayController  = NinjectGlobalKernel.GlobalKernel.Get<IDisplayController>();
            this.mapFactory = this.localKernel.Get<IMapFactory>();

            this.masterMap          = new Map(Settings.Default.WorldWidth, Settings.Default.WorldHeight);
            this.cells              = new List<IInternalCell>();
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        public void Initialize(IList<String> availableBrains)
        {
            this.brains = availableBrains;

            //this.masterMap = this.mapFactory.GetMap();

            CreateGeometryMap();
            CreateInitialCellPopulation();
            CreatePlantMap();
            CreateRessourcesMap();
        }

        /// <summary>
        /// Tabula Rasa
        /// </summary>
        public void Reset()
        {
            if (this.brains != null)
                this.brains.Clear();
    
            if (this.cells != null)
                this.cells.Clear();

            PopulateAvailableTeams();
        }

        /// <summary>
        /// Fill up the world with the loaded terrain
        /// </summary>
        private void CreateGeometryMap()
        {
            Int16[,] map = this.mapFactory.CreateMapFromFile();

            for (Int16 x = 0; x < map.GetLength(0); x++ )
            {
                for (Int16 y = 0; y < map.GetLength(1); y++)
                {
                    this.masterMap.Grid[x, y].Altitude = map[x, y];
                    this.displayController.SetStaticElement(new Coordinates(x, y), (DisplayQualifier)map[x, y]);
                }
            }
        }

        /// <summary>
        /// Returns a list of all the cells present in the game
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IInternalCell> GetCells()
        {
            return this.cells;
        }

        /// <summary>
        /// Registers the movement of the cell
        /// </summary>
        /// <param name="oldCoordinates">Coordinates where the cell was before it moved</param>
        /// <param name="newCoordinates">Coordinates where the cell is after it moved</param>
        /// <param name="team">Team Color of the cell</param>
        public void RegisterCellMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, DisplayQualifier team)
        {
            // Clear the previous position from the display
            this.displayController.SetBackgroundToBePaintAt(oldCoordinates);

            // Signals the new position to display
            this.displayController.SetDynamicElement(newCoordinates, team);
            
            // Update the map
            this.masterMap.MoveCell(oldCoordinates, newCoordinates);
        }

        /// <summary>
        /// Clears the list caching the movement list
        /// </summary>
        public void ResetMovementsList()
        {
            this.displayController.UpdatedElements.Clear();
        }

        /// <summary>
        /// Function creating a view of the surroundings of the cell and returning it
        /// For anti-cheating purpose, the world gets the cells position himself instead of getting them as parameters
        /// </summary>
        /// <param name="cell">The cell asking</param>
        /// <returns>A SurroundingView of the location where the cell resides</returns>
        public SurroundingView GetSurroundingsView(IInternalCell cell)
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
        public void UnregisterCell(IInternalCell cell)
        {
            deadCellsToRemove.Add(cell);
        }

        /// <summary>
        /// Cleans the public structures of all potentially remaining dead cells
        /// </summary>
        public void RemoveDeadCells()
        {
            foreach (Cell deadCell in deadCellsToRemove)
                RejectCell(deadCell);

            deadCellsToRemove.Clear();
        }

        /// <summary>
        /// Commit removal of the current cell from the game
        /// </summary>
        /// <param name="deadCell"></param>
        private void RejectCell(Cell deadCell)
        {
            this.cells.Remove(deadCell);
            this.masterMap.RemoveCell(deadCell);
            this.displayController.SetBackgroundToBePaintAt(deadCell.Position);
        }

        /// <summary>
        /// This function adds all the newly created cells to the game
        /// Those cells orginate from splittings of the previous round
        /// </summary>
        public void AddNewlyCreatedCellsToTheGame()
        {
            foreach (Cell newCell in this.newCellsToAdd)
            {
                this.InjectCell(newCell);
                this.displayController.SetDynamicElement(newCell.Position,newCell.GetTeamQualifier());
            }

            newCellsToAdd.Clear();
        }

        /// <summary>
        /// Creates a population of cells for each selected braintype
        /// </summary>
        private void CreateInitialCellPopulation()
        {
            foreach (String brainType in this.brains)
            {
                DisplayQualifier team = availableTeams[0];
                availableTeams.Remove(team);

                CreateCellPopulation(brainType, Settings.Default.InitialPopulationPerBrain, team);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brainType"></param>
        /// <param name="numberOfCells"></param>
        /// <param name="teamColor"></param>
        private void CreateCellPopulation(String brainType, short numberOfCells, DisplayQualifier team)
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                CreateCell(brainType, team);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brainType"></param>
        /// <param name="teamNumber"></param>
        /// <param name="spawnLife"></param>
        /// <param name="position"></param>
        private void CreateCell(String brainType, DisplayQualifier teamNumber, Int16? spawnLife = null, ICoordinates position = null)
        {
            var cell = this.localKernel.Get<IInternalCell>();

            var brain = this.globalKernel.Get(Type.GetType(brainType)) as IBrain;
            cell.SetBrain(brain);

            cell.Position = position ?? (cell.Position = GetRandomCoordinates());
            spawnLife = spawnLife ?? Settings.Default.CellMaxInitialLife;

            cell.SetLife(RandomGenerator.GetRandomInt16((Int16)spawnLife));
            cell.SetTeam(teamNumber);

            RegisterNewCell(cell);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        private void RegisterNewCell(IInternalCell newCell)
        {
            if (newCell.Position.X == 100)
            {}

            this.newCellsToAdd.Add(newCell);
        }

        /// <summary>
        /// Injects the cell into the game 
        /// (should only be done by the world itself just before the begining of the turn)
        /// </summary>
        /// <param name="newCell">The cell</param>
        private void InjectCell(IInternalCell newCell)
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
            for (int i = 0; i < 20; i++)
                masterMap.ImplantRessources(GetRandomCoordinates(), 500, 0);

            masterMap.ImplantRessources(new Coordinates(25, 25), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(50, 50), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(75, 75), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(25, 50), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(50, 25), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(25, 75), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(75, 25), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(75, 50), 5000, 0);
            masterMap.ImplantRessources(new Coordinates(50, 75), 5000, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreatePlantMap()
        {
            for (int i = 0; i < 20; i++)
                masterMap.ImplantRessources(GetRandomCoordinates(), 100, 10);

            masterMap.ImplantRessources(new Coordinates(26, 26), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(51, 51), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(76, 76), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(26, 51), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(51, 26), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(26, 76), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(76, 26), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(76, 51), 5100, 10);
            masterMap.ImplantRessources(new Coordinates(51, 76), 5100, 10);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spawnLife"></param>
        /// <param name="cell"></param>
        public void CreateSpawns(short spawnLife, Cell cell)
        {
            // We don't create spawns at the same place
            ICoordinates positionSpawn = new Coordinates();
            positionSpawn.X = cell.Position.X;
            positionSpawn.Y = cell.Position.Y;

            if (Helper.CoordinatesAreValid((Int16)(positionSpawn.X + 1), positionSpawn.Y))
                positionSpawn.X += 1;
            else
                positionSpawn.X += -1;

            this.CreateCell(cell.GetAttachedBrainType(), cell.GetTeamQualifier(), spawnLife, cell.Position);
            this.CreateCell(cell.GetAttachedBrainType(), cell.GetTeamQualifier(), spawnLife, positionSpawn);
        }

        /// <summary>
        /// Retrieves the GameMap
        /// </summary>
        /// <returns></returns>
        public IMap GetMap()
        {
            return this.masterMap as IMap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="ressources"></param>
        public void ReduceRessources(ICoordinates coordinates, Int16 ressources)
        {
            this.masterMap.DecreaseRessources(coordinates, ressources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public Int16 GetAmountOfRessourcesLeft(ICoordinates coordinates)
        {
            return this.masterMap.GetAmountOfRessourcesLeft(coordinates);
        }

        private void PopulateAvailableTeams()
        {
            this.availableTeams.Clear();
            this.availableTeams.Add(DisplayQualifier.Team1);
            this.availableTeams.Add(DisplayQualifier.Team2);
            this.availableTeams.Add(DisplayQualifier.Team3);
            this.availableTeams.Add(DisplayQualifier.Team4);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class localModule : NinjectModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            Bind<IInternalCell>().To<Cell>();
            Bind<IMapFactory>().To<MapFactory>();
        }
    }
}
