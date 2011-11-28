using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.Interfaces;
using Cells.Utils;

namespace Cells.Controller
{
    /// <summary>
    /// This class holds the references to what shall be printed on the screen upon each round
    /// </summary>
    class DisplayController : IDisplayController
    {
        public IDictionary<ICoordinates, Color> updatedElements { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DisplayController()
        {
            this.updatedElements = new Dictionary<ICoordinates, Color>();
        }

        /// <summary>
        /// Retrieves the list of updated elements
        /// </summary>
        /// <returns>A list of coordinates where elements were updated</returns>
        public IEnumerable<KeyValuePair<ICoordinates, Color>> GetUpdatedElements()
        {
            return updatedElements;
        }
    }
}
