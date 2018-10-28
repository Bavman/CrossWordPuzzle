using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossWordPuzzle.Game;

namespace CrossWordPuzzle.Game
{
    public class BoardControl
    {
        #region Singleton Setup
        static BoardControl _instance = null;

        private BoardControl()
        {

        }

        public static BoardControl Instance()
        {
            if (_instance == null)
            {
                _instance = new BoardControl();

            }

            return _instance;
        }

        #endregion

        public char _emptyChar = ' ';

        private int _horizontalCount = 12;
        private int _verticalCount = 12;

        public Board CrossWordboard;

        public void InitializeBoard()
        {
            CrossWordboard = new Board(_horizontalCount, _verticalCount, _emptyChar); // Array 1 is Row, Array 2 is Column
        }


        public void ResetBoard()
        {

            var rowLetters = new string(_emptyChar, _verticalCount);

            for (var i = 0; i < _horizontalCount; i++)
            {
                var row = new char[_horizontalCount];

                for (var j = 0; j < _horizontalCount; j++)
                {
                    CrossWordboard.Layout[i, j] = _emptyChar;
                    row[j] = _emptyChar;
                }

            }

        }

        // Is used to place word in grid arrays after checks have been done
        public void ForcePlaceWord(string word, Tuple<int, int> startPos, WordDirection direction)
        {
            var horizontalStart = startPos.Item1;
            var verticalStart = startPos.Item2;

            if (direction == WordDirection.Horizontal)
            {
                for (var i = horizontalStart; i < horizontalStart + word.Length; i++)
                {
                    CrossWordboard.Layout[startPos.Item2,i] = word[i - horizontalStart];
                }
            }

            if (direction == WordDirection.Vertical)
            {
                for (var i = verticalStart; i < verticalStart + word.Length; i++)
                {
                    CrossWordboard.Layout[i,startPos.Item1] = word[i - verticalStart];
                }
            }
        }

        public List<Tuple<int,int>> ReturnWordStartPositions(string word, int charIndex, WordDirection direction)
        {
            var potentialStartPositions = new List<Tuple<int, int>>();

            var wordLength = word.Length;


            if (charIndex < 0 || charIndex > wordLength)
            {
                return potentialStartPositions;
            }

            var letter = word[charIndex];
            

            // Scan whole board
            for (var i = 0; i < CrossWordboard.Layout.GetLength(0); i++)
            {

                for (var j = 0; j < CrossWordboard.Layout.GetLength(1); j++)
                {

                    // Find matching char (letter)
                    if (CrossWordboard.Layout[i,j] == letter)
                    {

                        if (direction == WordDirection.Horizontal)
                        {
                            // Horizontal Check if word is not too long and start of word fits on board
                            if ((wordLength - charIndex) + j <= _horizontalCount && j - charIndex >= 0)
                            {

                                potentialStartPositions.Add(new Tuple<int, int>(j - charIndex, i));

                            }
                        }

                        if (direction == WordDirection.Vertical)
                        {
                            // Vertical Check if word is not too long and start of word fits on board
                            if ((wordLength - charIndex) + i <= _verticalCount && i - charIndex >= 0)
                            {

                                potentialStartPositions.Add(new Tuple<int, int>(j, i - charIndex));

                            }
                        }
                    }   
                }
            }

            return potentialStartPositions;

        }

        // If word can be placed, it is
        public PlacedWord PlaceWord(string word, Tuple<int, int> startPos, WordDirection direction)
        {
            var tempIntersectPoas = new Tuple<int, int>(startPos.Item1 + 2, startPos.Item2);

            var canPlaceWord = CanWordBePlaced(word, startPos, direction);

            if (!canPlaceWord)
            {
                return null;
            }
            else
            {
                ForcePlaceWord(word, startPos, direction);
                return new PlacedWord { Word = word, StartPos = startPos, Direction = direction };
            }
        }

        // Check if word can be placed by doing horizontal and vertical 
        public bool CanWordBePlaced(string word, Tuple<int, int> startPos, WordDirection direction)
        {

            if (startPos.Item1 < 0 || startPos.Item2 < 0)
            {
                return false;
            }

            var wordLength = word.Length;

            // Scan word row and rows either side
            switch (direction)
            {
                case WordDirection.Horizontal:

                    if (startPos.Item1 + wordLength > _horizontalCount || startPos.Item2 >= _verticalCount)
                    {
                        return false;
                    }
                    // Row (Actual Word)
                    if (HorizontalWordScan(startPos, wordLength + 1, word, direction))
                    {
                        return false;
                    }

                break;

                case WordDirection.Vertical:

                    if (startPos.Item2 + wordLength > _verticalCount || startPos.Item1 >= _horizontalCount)
                    {
                        return false;
                    }

                    // Column (Actual Word)
                    if (VerticalWordScan(startPos, wordLength + 1, word, direction))
                    {
                        return false;
                    }

                break;

            }

            return true;
        }

        // Scan cells horizontally and returns true if any cells are not empty, excludes the ignore cell
        private bool HorizontalWordScan(Tuple<int, int> startPos, int length, string word, WordDirection direction)
        {
            
            var start = startPos.Item1;
            var end = start + length;

            for (var i = start; i < end; i++)
            {
                // Ignore if scan hits out of range cells
                if (i >= 0 && i < _horizontalCount) 
                {
                    if (CheckHorizontalIntersectionsAndPadding(i, startPos, word))
                    {
                        return true;
                    }
                    
                }

            }

            return false;
        }

        // Scan cells vertically and returns true if any cells are not empty, excludes the ignore cell
        private bool VerticalWordScan(Tuple<int, int> startPos, int length, string word, WordDirection direction)
        {

            var start = startPos.Item2;
            var end = start + length;

            for (var i = start; i < end; i++)
            {

                // Ignore if scan hits out of range cells
                if (i >= 0 && i < _verticalCount)
                {
                    if (CheckVerticalIntersectionsAndPadding(i, startPos, word))
                    {
                        return true;
                    }
                    
                }

            }

            return false;
        }


        // Checks Horizontal Intersections
        private bool CheckHorizontalIntersectionsAndPadding(int i, Tuple<int, int> startPos, string word)
        {
            // Check letter before word
            if (i - 1 > 0)
            {
                if (i == startPos.Item1)
                {
                    if (CrossWordboard.Layout[startPos.Item2, i - 1] != _emptyChar)
                    {
                        return true;
                    }
                    //_board[startPos.Item2][i - 1] = '*';
                }
            }

            // Word Row
            if (CrossWordboard.Layout[startPos.Item2, i] != _emptyChar)
            {

                // Check if same character at intersection, if not return true
                if (i < word.Length + startPos.Item1)
                {

                    if (word[i - startPos.Item1] != CrossWordboard.Layout[startPos.Item2, i])
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else // Padding
            {
                // Only allow padding check to be word length
                if (i < word.Length + startPos.Item1)
                {
                    // Check Padding Up
                    if (startPos.Item2 - 1 >= 0)
                    {

                        if (CrossWordboard.Layout[startPos.Item2 - 1, i] != _emptyChar)
                        {
                            return true;
                        }
                        //_board[startPos.Item2 - 1][i] = '*';
                    }

                    // Check Padding Down
                    if (startPos.Item2 + 1 < _verticalCount)
                    {

                        if (CrossWordboard.Layout[startPos.Item2 + 1, i] != _emptyChar)
                        {

                            return true;
                        }
                        //_board[startPos.Item2 + 1][i] = '*';
                    }

                }

            }
            //_board[startPos.Item2][i] = '*';

            return false;
        }

        
        // Checks Vertical Intersections
        private bool CheckVerticalIntersectionsAndPadding(int i, Tuple<int, int> startPos, string word)
        {

            // Check letter before word
            if (i - 1 > 0)
            {
                if (i == startPos.Item2)
                {
                    if (CrossWordboard.Layout[i - 1, startPos.Item1] != _emptyChar)
                    {
                        //Debug.WriteLine("HitPos {0},{1}", (i - 1), startPos.Item1);
                        //Debug.WriteLine("Pinged Cell Before");
                        return true;
                    }
                    //_board[i - 1][startPos.Item1] = '*';
                    
                }
            }
            // Word Column
            if (CrossWordboard.Layout[i, startPos.Item1] != _emptyChar)
            {
                
                if (i < word.Length + startPos.Item2)
                {   // Check if same character at intersection, if not return true
                    if (word[i - startPos.Item2] != CrossWordboard.Layout[i, startPos.Item1])
                    {
                        //Debug.WriteLine("HitPos {0},{1}", (i), startPos.Item1);
                        //Debug.WriteLine("Pinged During Word");
                        return true;
                    }
                    
                }
                else
                {
                    //Debug.WriteLine("HitPos {0},{1}", (i), startPos.Item1);
                    //Debug.WriteLine("Pinged During Word");
                    return true;
                }
                
            }
            else // Padding
            {
                // Only allow padding check to be word length
                if (i < word.Length + startPos.Item2)
                {
                    // Check Padding Left
                    if (startPos.Item1 - 1 >= 0)
                    {

                        if (CrossWordboard.Layout[i, startPos.Item1 - 1] != _emptyChar)
                        {
                            //Debug.WriteLine("HitPos {0},{1}", (i), (startPos.Item1 - 1));
                            //Debug.WriteLine("Pinged Padding Left");
                            return true;
                        }
                        //_board[i][startPos.Item1 - 1] = '*';
                    }

                    // Check Padding Right
                    if (startPos.Item1 + 1 < _horizontalCount)
                    {

                        if (CrossWordboard.Layout[i, startPos.Item1 + 1] != _emptyChar)
                        {
                            //Debug.WriteLine("HitPos {0},{1}", (i), (startPos.Item1 + 1));
                            //Debug.WriteLine("Pinged Padding Right");
                            return true;
                        }
                        //_board[i][startPos.Item1 + 1] = '*';

                    }

                }

            }
            //_board[i][startPos.Item1] = '*';
            return false;
        }

        public void CheckForVacantSpace()
        {
            var vacantRows = new List<VacantSpace>();

            var writingRow = false;
            var vacantRowCount = 0;

            Tuple<int, int> rowStart = null;

            // Horizontal Spaces
            for (var i = 0; i < CrossWordboard.Layout.GetLength(0); i++)
            {
                for (var j = 0; j < CrossWordboard.Layout.GetLength(1); j++)
                {

                    if (vacantRowCount >= 3)
                    {
                        writingRow = true;
                        if (rowStart == null)
                        {
                            rowStart = new Tuple<int, int>(j - 3, i);
                        }

                    }

                    // Finish row counting
                    if (j == _horizontalCount - 1)
                    {

                        vacantRowCount = 0;

                        if (writingRow)
                        {
                            writingRow = false;

                            var rowEnd = new Tuple<int, int>(j, i);

                            vacantRows.Add(new VacantSpace() { VacantStart = rowStart, VacantEnd = rowEnd });

                            rowStart = null;

                        }

                    }

                    if (CrossWordboard.Layout[i, j] == _emptyChar)
                    {
                        if (j == 0)
                        {
                            vacantRowCount = 0;
                        }
                        vacantRowCount++;

                    }
                    else // If cell not empty
                    {
                        vacantRowCount = 0;

                        if (writingRow)
                        {
                            writingRow = false;

                            var rowEnd = new Tuple<int, int>(j, i);

                            vacantRows.Add(new VacantSpace() { VacantStart = rowStart, VacantEnd = rowEnd });

                            rowStart = null;

                        }

                    }

                }

            }

            var vacantArray = new int[0];

            for (var i = 0; i < vacantRows.Count; i++)
            {


            }

            var arr1 = new int[] { 1, 2, 3, 4, 5};
            var arr2 = new int[] { 2, 3, 4};

            vacantArray = CompareIntArrays(arr1, arr2);

            for (var i = 0; i < vacantArray.Length; i++)
            {
                Debug.WriteLine(vacantArray[i]);
            }
        }


        private int[] CompareIntArrays(int[] intArray1, int[] intArray2)
        {
            var returnArray = new List<int>();

            for (var i = 0; i < intArray1.Length; i++)
            {
                var num = intArray1[i];

                for (var j = 0; j < intArray2.Length; j++)
                {
                    if (num == intArray2[j])
                    {
                        returnArray.Add(num);
                    }
                }
            }

            return returnArray.ToArray();
        }



        public void DisplayBoard(char[,] board)
        {
            Debug.WriteLine("    0 1 2 3 4 5 6 7 8 9 1 2\n");
            for (var i = 0; i < board.GetLength(0); i++)
            {

                //var rowString = new string(_board[i]);

                var rowString = String.Empty;

                for (var j = 0; j < board.GetLength(1); j++)
                {
                    rowString += board[i, j] + " ";
                }

                var rowCount = "";

                if (i < 10)
                {
                    rowCount = "0" + i.ToString();
                }
                else
                {
                    rowCount = i.ToString();
                }
                Debug.WriteLine(rowCount + "   "+rowString);

            }

        }


        public char[][] ConvertBoardTo2Arrays(char[,] board)
        {
            var returnBoard = new char[_verticalCount][];


            for (var i = 0; i < board.GetLength(0); i++)
            {
                returnBoard[i] = new char[_horizontalCount];

                for (var j = 0; j < board.GetLength(1); j++)
                {
                    returnBoard[i][j] = board[i, j];
                }

            }

            return returnBoard;

        }

        public char[,] ConvertBoardToMultidimensionalArray(char[][] board)
        {
            var returnBoard = new char[_verticalCount, _horizontalCount];


            for (var i = 0; i < board.Length; i++)
            {

                for (var j = 0; j < board[i].Length; j++)
                {
                    returnBoard[i,j] = board[i][j];
                }

            }

            return returnBoard;

        }
    }


    public class VacantSpace
    {
        public Tuple<int, int> VacantStart { get; set; }
        public Tuple<int, int> VacantEnd { get; set; }
    }

}


