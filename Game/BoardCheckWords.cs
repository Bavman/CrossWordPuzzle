using CrossWordPuzzle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWordPuzzle.Game
{
    public class BoardCheckWords
    {

        public List<string> CheckWord(ObservableCollection<ObservableCollection<Cell>> displayBoard, List<PlacedWord> placedWords, List<string> foundWords)
        {

            var remaingPlacedWords = placedWords.Where(w => !foundWords.Contains(w.Word)).ToList();


            for (var i = 0; i < remaingPlacedWords.Count; i++)
            {
                

                if (placedWords[i].Direction == WordDirection.Horizontal)
                {
                    var startPos = placedWords[i].StartPos;

                    for (var j = startPos.Item1; j < startPos.Item1+placedWords[i].Word.Length; j++)
                    {

                        if (displayBoard[startPos.Item2][j].LetterIn == placedWords[i].Word[j - startPos.Item1])
                        {

                        }

                    }

                }
                else
                {

                }


            }

            return foundWords;
        }
    }
}
