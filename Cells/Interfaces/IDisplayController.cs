using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Interfaces
{
    public interface IDisplayController
    {
        IEnumerable<KeyValuePair<ICoordinates, Color>> GetUpdatedElements();

        IDictionary<ICoordinates, Color> updatedElements { get; set; }
    }
}
