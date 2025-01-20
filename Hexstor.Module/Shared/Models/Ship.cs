using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Shared.Models
{
    /// <summary>
    /// Represents a double-coordinate hex system.
    /// https://www.redblobgames.com/grids/hexagons/#coordinates-doubled
    /// </summary>
    public class Ship
    {
        // constrctor takes a row and column and calculates the x and y coordinates
        public Ship()
        {
        }

        public int Heading { get; set; } = 1;

   
    }
}
