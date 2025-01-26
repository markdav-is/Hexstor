using Hexstor.Module.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexstor.Module.Client.ViewModels
{

    // enum for three styles of ships Red, Blue, and Yellow
    public enum CommandType
    {
        Play,
        Left,
        Right,
        Forward,
        Fire,
        ToggleRangeFinder
    }

    public enum MapCorner
    {
        NW, NE, SW, SE
    }


    /// <summary>
    /// Represents a double-coordinate hex system.
    /// https://www.redblobgames.com/grids/hexagons/#coordinates-doubled
    /// </summary>
    public class Command
    {
        // constrctor takes a row and column and calculates the x and y coordinates
        public Command()
        {
        }
        public int PlayerId { get; set; }
        public CommandType Type { get; set; }
        public MapCorner StartCorner{ get; set; } = MapCorner.NW;
        public ShipStyle ShipStyle { get; set; } = ShipStyle.Red;
        public int AttackRange { get; internal set; }
        public bool RangeFinderVisible { get; internal set; }
        internal SettingsViewModel.RangeShapes AttackShape { get; set; }
    }
}
