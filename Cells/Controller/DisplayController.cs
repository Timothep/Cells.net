using System.Collections.Generic;
using System.Drawing;
using Cells.Interfaces;
using Cells.Properties;
using Cells.Utils;
using Cells.View;
using Ninject;

namespace Cells.Controller
{
    /// <summary>
    /// This class holds the references to what shall be printed on the screen upon each round
    /// </summary>
    class DisplayController : IDisplayController
    {
        /// <summary>
        /// Dictionary holding all the static elements to be displayed
        /// Will only be filled at the begining, and then be only accessed
        /// </summary>
        public IDictionary<ICoordinates, Color> StaticElements { get; set; }
        
        /// <summary>
        /// Dictionary holding all the dynamic elements to be displayed
        /// Will be constantly reset and filled up
        /// </summary>
        public IDictionary<ICoordinates, Color> UpdatedElements { get; set; }

        /// <summary>
        /// Grid representing the background
        /// </summary>
        public VisualTile[,] BackgroundGrid;

        /// <summary>
        /// 
        /// </summary>
        private IColorPanel colorPanel;

        /// <summary>
        /// Constructor
        /// </summary>
        public DisplayController()
        {
            this.UpdatedElements = new Dictionary<ICoordinates, Color>();
            this.StaticElements = new Dictionary<ICoordinates, Color>();
            this.BackgroundGrid = new VisualTile[Settings.Default.WorldWidth,Settings.Default.WorldHeight];
            this.colorPanel = NinjectGlobalKernel.GlobalKernel.Get<IColorPanel>();
        }

        /// <summary>
        /// Adds the whole static element dictionary to the updated elements for painting
        /// </summary>
        public void PaintWholeBackground()
        {
            this.UpdatedElements = this.StaticElements;
        }

        /// <summary>
        /// Registers the given color at the given coordinates on the background
        /// </summary>
        /// <param name="elementCoordinates"></param>
        /// <param name="elementColor"></param>
        public void SetStaticElement(ICoordinates elementCoordinates, DisplayQualifier qualifier)
        {
            StaticElements.Add(elementCoordinates, this.colorPanel.GetCorrespondingColor(qualifier));
            BackgroundGrid[elementCoordinates.X, elementCoordinates.Y] = new VisualTile(this.colorPanel.GetCorrespondingColor(qualifier));
        }

        /// <summary>
        /// Retrieves the list of updated elements
        /// </summary>
        /// <returns>A list of coordinates where elements were updated</returns>
        public IDictionary<ICoordinates, Color> GetPaintJobs()
        {
            return UpdatedElements;
        }

        /// <summary>
        /// Adds the color at the given coordinates to be painted during the next loop
        /// </summary>
        /// <param name="elementCoordinates"></param>
        /// <param name="qualifier"></param>
        public void SetDynamicElement(ICoordinates elementCoordinates, DisplayQualifier qualifier)
        {
            if (!this.UpdatedElements.ContainsKey(elementCoordinates))
                this.UpdatedElements.Add(elementCoordinates, this.colorPanel.GetCorrespondingColor(qualifier));
            else if (this.UpdatedElements[elementCoordinates] == this.BackgroundGrid[elementCoordinates.X, elementCoordinates.Y].GetColor())
                this.UpdatedElements[elementCoordinates] = this.colorPanel.GetCorrespondingColor(qualifier);
        }

        /// <summary>
        /// Signals the DisplayControler that the background has to be painted at the given coordinates again
        /// </summary>
        /// <param name="coordinates"></param>
        public void SetBackgroundToBePaintAt(ICoordinates coordinates)
        {
            if (!this.UpdatedElements.ContainsKey(coordinates))
                this.UpdatedElements.Add(coordinates, this.BackgroundGrid[coordinates.X,coordinates.Y].GetColor());
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetDynamicDisplay()
        {
            this.UpdatedElements.Clear();
        }
    }
}
