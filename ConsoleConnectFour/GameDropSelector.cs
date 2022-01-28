using System;
using System.Linq;

namespace ConsoleConnectFour
{
    public class GameDropSelector
    {
        public int size;
        public int column;
        public bool[] row;

        public GameDropSelector(int setSize)
        {
            size = setSize;
            column = size / 2 - 1;

            row = Enumerable.Repeat(false, size).ToArray();
            row[column] = true;
        }

        public void Move(bool direction)
        {
            if (direction)
            {
                // Check if reached column bound 
                if (column == size - 1)
                {
                    column = 0;
                }
                else
                {
                    column++;
                }
            }
            else
            {
                // Check if reached column bound  
                if (column == 0)
                {
                    column = size -1;
                }
                else
                {
                    column--;
                }
            }

            row = Enumerable.Repeat(false, size).ToArray();
            row[column] = true;
        }
    }
}

