using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Shared.Models
{

    // enum for three styles of ships Red, Blue, and Yellow
    public enum ShipStyle
    {
        Red,
        Blue,
        Yellow
    }

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
        public ShipStyle Style { get; set; } = ShipStyle.Red;
        public string StyleCode => Style switch
        {
            ShipStyle.Red => "R",
            ShipStyle.Blue => "B",
            ShipStyle.Yellow => "Y",
            _ => "R"
        };
        public int Level { get; set; } = 1;



    }
}
