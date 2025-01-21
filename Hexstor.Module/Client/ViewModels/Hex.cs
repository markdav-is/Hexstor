using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Client.ViewModels
{
    /// <summary>
    /// Represents a double-coordinate hex system.
    /// https://www.redblobgames.com/grids/hexagons/#coordinates-doubled
    /// </summary>
    public class Hex
    {
        // constrctor takes a row and column and calculates the x and y coordinates
        public Hex()
        {
        }

        public DoubCoord DoubCoord { get; set; }
        public Ship Ship { get; set; }


    }
}
