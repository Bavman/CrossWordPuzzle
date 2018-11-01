using CrossWordPuzzle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CrossWordPuzzle.Game
{
    public class SolveBoard
    {

        public List<string> CheckWord(ObservableCollection<ObservableCollection<Cell>> displayBoard, List<PlacedWord> placedWords, List<string> foundWords)
        {
            var remaingPlacedWords = placedWords.Where(w => !foundWords.Contains(w.Word)).ToList();

            for (var i = 0; i < remaingPlacedWords.Count; i++)
            {
                
                if (remaingPlacedWords[i].Direction == WordDirection.Horizontal)
                {
                    var startPos = remaingPlacedWords[i].StartPos;

                    var correctLetterCount = 0;

                    for (var j = startPos.Item1; j < startPos.Item1+ remaingPlacedWords[i].Word.Length; j++)
                    {

                        if (displayBoard[startPos.Item2][j].LetterIn == remaingPlacedWords[i].Word[j - startPos.Item1])
                        {
                            correctLetterCount++;
                        }

                    }

                    if (correctLetterCount == remaingPlacedWords[i].Word.Length)
                    {
                        for (var j = startPos.Item1; j < startPos.Item1 + remaingPlacedWords[i].Word.Length; j++)
                        {
                            displayBoard[startPos.Item2][j].ForegroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
                            displayBoard[startPos.Item2][j].IsReadOnly = true;
                            displayBoard[startPos.Item2][j].FontWeight = "Bold";

                        }

                        foundWords.Add(remaingPlacedWords[i].Word);
                    }

                }
                else
                {
                    var startPos = remaingPlacedWords[i].StartPos;

                    var correctLetterCount = 0;

                    for (var j = startPos.Item2; j < startPos.Item2 + remaingPlacedWords[i].Word.Length; j++)
                    {

                        if (displayBoard[j][startPos.Item1].LetterIn == remaingPlacedWords[i].Word[j - startPos.Item2])
                        {
                            correctLetterCount++;
                        }

                    }

                    if (correctLetterCount == remaingPlacedWords[i].Word.Length)
                    {
                        for (var j = startPos.Item2; j < startPos.Item2 + remaingPlacedWords[i].Word.Length; j++)
                        {
                            displayBoard[j][startPos.Item1].ForegroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
                            displayBoard[j][startPos.Item1].IsReadOnly = true;
                            displayBoard[j][startPos.Item1].FontWeight = "Bold";
                        }

                        foundWords.Add(remaingPlacedWords[i].Word);
                    }

                }


            }

            return foundWords;
        }




        public Tuple<int, int> GetUpdatePosition(ObservableCollection<ObservableCollection<Cell>> displayBoardIn, Board compareBoard)
        {
            var updatePos = new Tuple<int, int>(0, 0);


            for (var i = 0; i < displayBoardIn.Count; i++)
            {
                for (var j = 0; j < displayBoardIn[i].Count; j++)
                {
                    if (displayBoardIn[i][j].LetterIn != compareBoard.Layout[i, j])
                    {
                        updatePos = new Tuple<int, int>(j, i);
                        compareBoard.Layout[i, j] = displayBoardIn[i][j].LetterIn;
                    }
                }
            }

            return updatePos;
        }
    }
}
