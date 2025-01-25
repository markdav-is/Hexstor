using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Client.ViewModels
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

        public int PlayerId { get; set; }
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

        // Targetting, Range settings
        public bool isRangeFinderShown { get; set; } = false;
        public int AttackRange { get; set; } = 3;
        // bit of a stretch, this would be better defined in a Weapon class instead of part of the ship
        public enum RangeShapes  {
            Line,
            Cone,
            Circle
        }
        public RangeShapes RangeShape { get; set; } = RangeShapes.Cone;

    }
}
