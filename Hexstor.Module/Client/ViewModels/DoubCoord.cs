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
        public DoubCoord Forward(int direction)
        {
            DoubCoord newCoord = null;
            if (Col % 2 != 0)
            {  //odd
                switch (direction)
                {
                    case 1:
                        newCoord = new DoubCoord(Row - 1, Col);
                        break;
                    case 2:
                        newCoord = new DoubCoord(Row, Col + 1);
                        break;
                    case 3:
                        newCoord = new DoubCoord(Row + 1, Col + 1);
                        break;
                    case 4:
                        newCoord = new DoubCoord(Row + 1, Col);
                        break;
                    case 5:
                        newCoord = new DoubCoord(Row+1, Col - 1);
                        break;
                    case 6:
                        newCoord = new DoubCoord(Row, Col - 1);
                        break;
                }
            }
            else
            { // even
                switch (direction)
                {
                    case 1:
                        newCoord = new DoubCoord(Row - 1, Col);
                        break;
                    case 2:
                        newCoord = new DoubCoord(Row - 1, Col + 1);
                        break;
                    case 3:
                        newCoord = new DoubCoord(Row, Col + 1);
                        break;
                    case 4:
                        newCoord = new DoubCoord(Row+1, Col);
                        break;
                    case 5:
                        newCoord = new DoubCoord(Row, Col - 1);
                        break;
                    case 6:
                        newCoord = new DoubCoord(Row-1, Col - 1);
                        break;
                }
            }
            return newCoord;
        }
    }
}
