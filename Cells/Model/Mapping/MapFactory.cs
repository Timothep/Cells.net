using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Model.Mapping
{
    class MapFactory : IMapFactory
    {
        public MapFactory()
        {

        }

        public Int16[,] CreateMapFromFile()
        {
            Int16[,] map = new Int16[100, 100];

            if (File.Exists("map1.map"))
            {
                var allLines = File.ReadAllLines("map1.map");

                for (Int16 x = 0; x < 100; x++ )
                    for (Int16 y = 0; y < 100; y++)
                    {
                        char[] chararray = allLines[y].ToCharArray();
                        map[x, y] = Convert.ToInt16(chararray.GetValue(x).ToString());
                    }

                return map;
            }
            else
                return null;
        }
    }
}
