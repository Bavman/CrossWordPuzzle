﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWordPuzzle.Game
{
    public class Board
    {

        public char[,] Layout;

        public Board(int width, int height, char emptyChar)
        {
            Layout = InitializeBoard(width, height, emptyChar);
        }


        private char[,] InitializeBoard(int width, int hieght, char emptyChar)
        {

            var crossWordBoard = new char[width, hieght];

            var rowLetters = new string(emptyChar, hieght);


            crossWordBoard = new char[width, hieght];

            for (var i = 0; i < width; i++)
            {
                var row = new char[width];

                for (var j = 0; j < width; j++)
                {
                    crossWordBoard[i, j] = emptyChar;
                    row[j] = emptyChar;
                }

            }


            return crossWordBoard;
        }
    }
}
