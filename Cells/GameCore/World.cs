﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;

namespace Cells.GameCore
{
    public class World
    {
        private const short WorldWidth = 100;
        private const short WorldHeight = 100;
        private const short InitialPopulation = 10;

        private readonly Map _ressourcesMap;
        private readonly Map _plantMap;
        private readonly Map _cellsMap;

        private List<Cell> _cells = new List<Cell>();
        private IDictionary<Coordinates, Color> _updatedElements = new ConcurrentDictionary<Coordinates, Color>();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            // Create the empty maps of the world
            this._ressourcesMap = new Map(WorldWidth, WorldHeight);
            this._plantMap = new Map(WorldWidth, WorldHeight);
            this._cellsMap = new Map(WorldWidth, WorldHeight);

            this._cells = new List<Cell>();
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        internal void Initialize()
        {
            CreateMaps();
            CreateInitialCellPopulation();
        }

        /// <summary>
        /// Returns a list of all the cells present in the game
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cell> GetCells()
        {
            return this._cells;
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
            this._updatedElements.Add(oldCoordinates, Color.Black);
            this._updatedElements.Add(newCoordinates, team);
        }

        /// <summary>
        /// Create the different maps
        /// </summary>
        private void CreateMaps()
        {
            // Create plant map
            // -> none for now

            // Create ressource map
            // -> none for now

            // Create height map
            // -> none for now
        }

        /// <summary>
        /// Creates a population of cells
        /// </summary>
        private void CreateInitialCellPopulation()
        {
            for (int i = 100 ; i < 120 ; i++)
            {
                for (int j = 100; j < 120 ; j++)
                {
                    Color color = i % 2 == 0 ? Color.Yellow : Color.Red;
                    this._cells.Add(new Cell(i, j, 10, this, color));
                }
            }
        }

        /// <summary>
        /// Clears the list caching the movement list
        /// </summary>
        internal void ResetMovementsList()
        {
            this._updatedElements.Clear();
        }

        /// <summary>
        /// Function creating a MapView of the surroundings of the cell and returning it
        /// For anti-cheating purpose, the world gets the cells position himself 
        /// instead of getting them as parameters
        /// </summary>
        /// <param name="cell">The cell asking</param>
        /// <returns>A MapView of the place</returns>
        internal IDictionary<string, MapView> GetMapView(Cell cell)
        {
            IDictionary<string, MapView> lMV = new Dictionary<String, MapView>();
            Coordinates cellCoordinates = cell.Position;

            lMV.Add("Ressources", this._ressourcesMap.CreateMapView(cellCoordinates));
            lMV.Add("Plants", this._plantMap.CreateMapView(cellCoordinates));
            lMV.Add("Cells", this._cellsMap.CreateMapView(cellCoordinates));

            return lMV;
        }
    }
}
