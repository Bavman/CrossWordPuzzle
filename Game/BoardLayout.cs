using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CrossWordPuzzle.Core;
using System.Diagnostics;

namespace CrossWordPuzzle.Game
{
    public class BoardLayout
    {

        public List<PlacedWord> PlacedWords = new List<PlacedWord>();

        private int[] _wordSizes = new int[] { 9, 9, 8, 8, 7, 7, 7, 6, 6, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 3 }; // 10 words

        private List<string> _usedWords = new List<string>();

        private int _wordsPlaced;
        private int _minWordsPlaced = 13;

        public string RetrieveWord(List<string> usedWords, int letterCount)
        {

            var wordList = WordList.Instance().Words.ToArray();

            var wordArray = wordList.Where(w => w.Length == letterCount).
                Except(usedWords).
                ToArray();

            var random = new Random();

            var word = string.Empty;

            if (wordArray == null)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("Ran out of words");

                return null;
            }

            if (wordArray.Length == 0)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("No words with the desired lettercount");

                return null;
            }

            return wordArray[random.Next(0, wordArray.Length)]; ;

        }



        public void PlaceAllWords()
        {

            PlacedWords.Clear();

            _wordsPlaced = 0;
            var solved = false;

            while (solved == false)
            {
                for (int i = 0; i < 3; i++)
                {

                    if (_wordsPlaced >= _minWordsPlaced)
                    {

                        solved = true;
                        break;

                    }

                    PlaceWords();
                }

                // Reset the board and start again if min word count in not met
                if (!solved)
                {
                    Board.Instance().InitializeBoard();
                    _wordsPlaced = 0;
                    _usedWords = new List<string>();
                    solved = false;
                }
            }
            Debug.WriteLine("Words Placed "+_wordsPlaced);
        }

        private void PlaceWords()
        {

            // First Word
            var word = RetrieveWord(new List<string> { }, _wordSizes[0]);
            var random = new Random();
            var horizontalPos = random.Next(0, 2);
            var vertiacalPos = random.Next(4, 6);
            var randomStartPos = new Tuple<int, int>(horizontalPos, vertiacalPos);

            // Place first word
            var placedWord = Board.Instance().PlaceWord(word, randomStartPos, WordDirection.Horizontal);

            PlacedWords.Add(placedWord);

            _usedWords.Add(word);

            // Rest of board
            var direction = WordDirection.Vertical;

            // Sequence through word size array
            for (var i = 1; i < _wordSizes.Length; i++)
            {

                var wordAndPosList = FindWordAndPositions(_wordSizes[i], direction);

                if (wordAndPosList != null)
                {

                    var startPositions = new List<Tuple<int, int>>(wordAndPosList.StartPosList);

                    word = wordAndPosList.Word;

                    List<int> usedCharsIndex = new List<int> { };

                    if (startPositions.Count > 0)
                    {

                        
                        var whileCount = 0;

                        // Try start positions until word placement is found
                        while (whileCount < startPositions.Count)
                        {
                            var listIndex = ReturnRandomNumberExcludingArrayInts(startPositions.Count, usedCharsIndex);

                            usedCharsIndex.Add(listIndex);

                            var startPos = startPositions[listIndex];

                            placedWord = Board.Instance().PlaceWord(word, startPos, direction);

                            if (placedWord != null)
                            {

                                _usedWords.Add(word);

                                PlacedWords.Add(placedWord);

                                if (direction == WordDirection.Horizontal)
                                {
                                    direction = WordDirection.Vertical;
                                }
                                else
                                {
                                    direction = WordDirection.Horizontal;
                                }

                                _wordsPlaced++;

                                break;
                            }

                            whileCount ++;
                        }   

                    }

                }
                
            }

        }


        private WordAndStartPositions FindWordAndPositions(int wordLength, WordDirection direction)
        {
            var startPosAttempts = 0;
            var word = String.Empty;
            var usedWords = new List<string>(_usedWords);
            var starPosList = new List<Tuple<int, int>> {};
            var maxCounts = 25;


            // Attempt to find start positions
            while (startPosAttempts < maxCounts)
            {
                word = RetrieveWord(usedWords, wordLength);

                var wordCharCount = 0;

                // Attemps cycling through letters
                while (wordCharCount < word.Length)
                {

                    var posList = Board.Instance().ReturnWordStartPositions(word, wordCharCount, direction);
                    starPosList.AddRange(posList);

                    wordCharCount ++;
                }

                if (starPosList.Count > 0)
                {

                    break;
                }

                usedWords.Add(word);
               
                startPosAttempts ++;
            }

            // Return null of no position found
            if (starPosList.Count == 0)
            {
                Debug.WriteLine("____________failed to find startPos");
                return null;
            }

            var wordAndPosList = new WordAndStartPositions();

            wordAndPosList.StartPosList = new List<Tuple<int,int>>(starPosList);
            wordAndPosList.Word = word;


            return wordAndPosList;

        }


        public int ReturnRandomNumberExcludingArrayInts(int length, List<int> usedCharsIndex)
        {
            var sequenceArray = Enumerable.Range(0, length).ToArray();

            var availableInts = sequenceArray.Except(usedCharsIndex).ToArray();


            Random random = new Random();

            var result = availableInts[random.Next(0, availableInts.Length)];
            
            return result;
        }


        private List<WordAndDefinition> SortPlacedWords(List<PlacedWord> placedWords)
        {
            var wordDefinitions = new List<WordAndDefinition>();
            // Sorted Horizontal or vertical

            // Sort in ascending

            return wordDefinitions;
        }

    }

    public class WordAndStartPositions
    {

        public List<Tuple<int, int>> StartPosList;
        public string Word;
        
    }

    public class PlacedWord
    {
        public Tuple<int, int> StartPos;
        public string Word;
        public WordDirection Direction;
    }

}
