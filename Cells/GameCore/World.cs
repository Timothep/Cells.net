﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.Utils;

namespace Cells.GameCore
{
    /// <summary>
    /// Class representing the world, holding the maps and all the cells together
    /// </summary>
    public class World
    {
        private const short WorldWidth = 100;
        private const short WorldHeight = 100;

        private readonly Map _ressourcesMap;
        private readonly Map _plantMap;
        private readonly Map _cellsMap;

        private readonly List<Cell> _cells = new List<Cell>();
        private readonly IDictionary<Coordinates, Color> _updatedElements = new ConcurrentDictionary<Coordinates, Color>();

        /// <summary>
        /// Constructor
        /// </summary>
        public World()
        {
            // Create the empty maps of the world
            _ressourcesMap = new Map(WorldWidth, WorldHeight);
            _plantMap = new Map(WorldWidth, WorldHeight);
            _cellsMap = new Map(WorldWidth, WorldHeight);

            _cells = new List<Cell>();
        }

        /// <summary>
        /// Initializes the world as we know it
        /// </summary>
        internal void Initialize()
        {
            CreateInitialCellPopulation();
        }

        /// <summary>
        /// Returns a list of all the cells present in the game
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cell> GetCells()
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
            _updatedElements.Add(oldCoordinates, Color.Black);
            _updatedElements.Add(newCoordinates, team);
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
                    _cells.Add(new Cell(i, j, 10, this, color));
                }
            }
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
        /// <returns>A MapView of the place</returns>
        internal SurroundingView GetSurroundingsView(Cell cell)
        {
            var surroundingView = new SurroundingView();
            surroundingView.SetCenterCoordinates(cell.Position);
            surroundingView.SetCellView(this._cellsMap.GetMapExtract(cell.Position));
            surroundingView.SetPlantView(this._plantMap.GetMapExtract(cell.Position));
            surroundingView.SetRessourceView(this._ressourcesMap.GetMapExtract(cell.Position));

            return surroundingView;
        }
    }
}
