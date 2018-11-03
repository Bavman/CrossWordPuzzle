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
        // Returns a list of words that have been placed each time a character is entered into the board. 
        // It considers the existing placed words and removes from calculation
        // Confirms the word is correct and adjusts colour to suit. Also used to count the score.
        public Tuple<List<PlacedWord>, PlacedWord> CheckWordOnCharUpdate(ObservableCollection<ObservableCollection<Cell>> displayBoard, List<PlacedWord> placedWords, List<PlacedWord> foundWords)
        {
            var remaingPlacedWords = placedWords.Where(w => !foundWords.Select( x => x.Word).Contains(w.Word)).ToList();

            var solvedWord = new PlacedWord();

            for (var i = 0; i < remaingPlacedWords.Count; i++)
            {

                if (remaingPlacedWords[i].Direction == WordDirection.Horizontal)
                {
                    var startPos = remaingPlacedWords[i].StartPos;

                    var correctLetterCount = 0;

                    // Check all letters of a placedWord
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
                            FormatTextBox(displayBoard, startPos.Item2, j);
                        }

                        solvedWord = remaingPlacedWords[i];
                        foundWords.Add(solvedWord);
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
                            FormatTextBox(displayBoard, j, startPos.Item1);
                        }

                        solvedWord = remaingPlacedWords[i];
                        foundWords.Add(solvedWord);
                    }

                }

            }

            return new Tuple<List<PlacedWord>, PlacedWord>(foundWords, solvedWord);
        }

        // Formats XAML TextBox for found words
        private void FormatTextBox(ObservableCollection<ObservableCollection<Cell>> displayBoard, int array1Index, int array2Index)
        {
            displayBoard[array1Index][array2Index].ForegroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
            displayBoard[array1Index][array2Index].IsReadOnly = true;
            displayBoard[array1Index][array2Index].FontWeight = "Bold";
        }

        // Change definition font colour if word found
        public void StyleDefinition(ObservableCollection<Definition> definitions, PlacedWord foundWord)
        {
            // Remove items that whose definition.Phrase == ACROSS and DOWN
            var definitionsAcrossDownRemoved = definitions.Where(d => d.Phrase != "ACROSS").Where(d => d.Phrase != "DOWN");

            var definition = definitionsAcrossDownRemoved.Where(d => d.Index == foundWord.DefinitionIndex)
                .Where(d => d.Direction == foundWord.Direction).ToArray();

            if (definition.Length != 0)
            {
                definition[0].TextDecoration = "Strikethrough";
                definition[0].ForegroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
            }
            
        }

        // Calculates updated array position based on previous board and recenelty updated char
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
