using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Tests.Utilities
{
    public class TestObjects
    {
        public static Map GetTestMap()
        {
            char[,] mapView = {
                          {'.','.','#'},
                          {'.','#','.'},
                          {'#','#','#'}
                       };

            return new Map(mapView);
        }
    }
}
