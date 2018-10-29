using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWordPuzzle.Game
{
    public class Board
    {

        public char[,] Layout { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public char EmptyChar { get; set; }

        public Board(int width, int height, char emptyChar)
        {
            Width = width;
            Height = height;
            EmptyChar = emptyChar;

            Layout = InitializeBoard(width, height, emptyChar);
        }


        private char[,] InitializeBoard(int width, int height, char emptyChar)
        {

            var crossWordBoard = new char[height, width];

            for (var i = 0; i < height; i++)
            {

                for (var j = 0; j < width; j++)
                {
                    crossWordBoard[i, j] = emptyChar;
                }

            }


            return crossWordBoard;
        }
    }
}
