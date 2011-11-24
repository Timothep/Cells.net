using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cells.GameCore
{
    class MapFactory
    {
        private const Int16 MapWidth = 500;
        private const Int16 MapHeight = 500;

        private Map _map;

        public Map GetMap()
        {
            return this._map;
        }

        public void CreateMap()
        {

        }
    }
}
