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
    public class DoubCoord
    {
        // constrctor takes a row and column and calculates the x and y coordinates
        public DoubCoord(int row, int col)
        {
            Row = row;
            Col = col;
            Xcord = col; // always for double-height aka flat-top hexes
            Ycord = (row * 2) + (col % 2); // 1,2,3,4,5 = 2,4,6,8,10 plus 1 if odd col
        }

        public int Row;
        public int Col;
        public int Xcord;
        public int Ycord;
        public override string ToString()
        {
            return $"{Xcord}, {Ycord}";
        }

        private string[] faces = { "A", "B", "C", "D", "E", "F" };
        
        public string GetFace(int direction) {
            return faces[direction - 1];
        }
        
        public DoubCoord Forward(int direction)
        {
            int[][] oddDirections =
            {
                [-1, 0 ], // direction 1 N
                [ 0, 1 ], // direction 2 NE
                [ 1, 1 ], // direction 3 SE
                [ 1, 0 ], // direction 4 S
                [ 1, -1], // direction 5 SW
                [ 0, -1]  // direction 6 NW
            };

            int[][] evenDirections = 
            {
                [ -1,  0 ], // direction 1 N
                [ -1,  1 ], // direction 2 NE
                [  0,  1 ], // direction 3 SE
                [  1,  0 ], // direction 4 S
                [  0, -1 ], // direction 5 SW
                [ -1, -1 ]  // direction 6 NW
            };

            int[] selectedDirection = Col % 2 != 0 ? oddDirections[direction - 1] : evenDirections[direction - 1];
            return new DoubCoord(Row + selectedDirection[0], Col + selectedDirection[1]);
        }
    }
}
