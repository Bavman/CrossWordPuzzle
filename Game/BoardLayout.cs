using CrossWordPuzzle.Core;
using CrossWordPuzzle.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CrossWordPuzzle.Game
{
    public class BoardLayout
    {
        public IEnumerable<PlacedWord> GroupedWords;

        public List<PlacedWord> PlacedWords = new List<PlacedWord>();
        
        public IEnumerable<Definition> Definitions;

        private Random _random = new Random();

        int _count;

        private int[] _wordSizes = 
        {
            9, 7, 7, 7, 7, 7, 6, 6, 6, 5, 5, 5, 4, 4, 4, 
            6, 6, 5, 5, 5, 5, 5, 6, 6, 5, 5, 5, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3
        };

        private List<string> _usedWords = new List<string>();

        private int _wordsPlaced;
        private int _minWordsPlaced = 15;


        // Keeps trying to solve board until the _minWordsPlaced count is reached 
        public void StartPlaceAllWords()
        {
            PlacedWords.Clear();
            _usedWords.Clear();
            _wordsPlaced = 0;
            _count = 0;

            var solved = false;

            while (solved == false)
            {
                PlaceWords();

                if (_wordsPlaced >= _minWordsPlaced)
                {
                    solved = true;
                    break;
                }
                Debug.WriteLine("_wordsPlaced " + _wordsPlaced);
                // Reset the board and start again if min word count in not met
                if (!solved)
                {
                    CrossWordBoard.Instance().ResetBoard(CrossWordBoard.Instance().CrossWordboard, ' ');

                    _wordsPlaced = 0;
                    _usedWords = new List<string>();
                    solved = false;
                }
            }

            // Sort and find definition of placed words then apply formatting
            var SortedPlacedWords = SortPlacedWordsByPosition(PlacedWords);

            GroupedWords = GroupPlacedWords(SortedPlacedWords);

            var definitions = ReturnDefinitionArray(GroupedWords);

            Definitions = ApplyDefinitionsFormatting(definitions);

            Debug.WriteLine("Words Placed " + _wordsPlaced);
        }



        // Sort and find definition of placed words
        private IEnumerable<PlacedWord> GroupPlacedWords(List<PlacedWord> placedWords)
        {
            var sortedHorizontalPlacedWords = SortPlacedWordsByAxis(placedWords, WordDirection.Horizontal);
            var sortedVerticalPlacedWords = SortPlacedWordsByAxis(placedWords, WordDirection.Vertical);

            sortedHorizontalPlacedWords.Insert(0, (new PlacedWord { Definition = "ACROSS" }));
            sortedVerticalPlacedWords.Insert (0 , (new PlacedWord { Definition = "DOWN" }));

            var joinedLists = sortedHorizontalPlacedWords.Concat(sortedVerticalPlacedWords);

            return joinedLists;
        }

        // Asign definition from placedWords to definition class
        private IEnumerable<Definition> ReturnDefinitionArray(IEnumerable<PlacedWord> placedWords)
        {
            var definitions = placedWords.Select(d => new Definition { Phrase = d.Definition, Index = d.DefinitionIndex });

            return definitions;
        }

        // Apply bold on definitions that are "ACROSS" and "BOLD"
        private IEnumerable<Definition> ApplyDefinitionsFormatting(IEnumerable<Definition> definitions)
        {

            var def = definitions.ToArray();

            for (var i = 0; i < def.Length; i++)
            {
                if (def[i].Phrase == "ACROSS" || def[i].Phrase == "DOWN")
                {
                    def[i].FontWeight = "Bold";
                }
                else
                {
                    def[i].FontWeight = "Normal";
                }
            }

            return def;
        }

        // Sort placed words based on start postions.
        public List<PlacedWord> SortPlacedWordsByAxis(List<PlacedWord> placedWords, WordDirection direction)
        {

            var placedWordsSorted = new List<PlacedWord>();
            // Sorted Horizontal or vertical
            if (direction == WordDirection.Horizontal)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => Convert.ToInt32(w.DefinitionIndex)).ToList();
            }
            if (direction == WordDirection.Vertical)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => Convert.ToInt32(w.DefinitionIndex)).ToList();
            }

            
            return placedWordsSorted;
        }

        // Sort placed words into horizontal order and assign definition and index to placedWord
        public List<PlacedWord> SortPlacedWordsByPosition(List<PlacedWord> placedWords)
        {
            var placedWordsSorted = new List<PlacedWord>();

            // Sort in ascending
            placedWordsSorted = placedWords.OrderBy(w => w.StartPos.Item2).ThenBy(w => w.StartPos.Item1).ToList();

            return FindDefinitionsAndAssignLocationIndex(placedWordsSorted);
        }

        // After words are placed and sorted, this method looks up the placed word definition
        public List<PlacedWord> FindDefinitionsAndAssignLocationIndex(List<PlacedWord> placedWords)
        {
            var placedWordWithDeets = new List<PlacedWord>();

            var previousStartPos = new Tuple<int,int>(-1,-1);

            var count = 0;

            for (var i = 0; i < placedWords.Count; i++)
            {
                var definitionArray = WordList.Instance().WordAndDefinitions.Where(d => d.Word == placedWords[i].Word).ToArray();

                if (definitionArray == null)
                {
                    return null;
                }
                var definition = definitionArray[0].Definition;

                if (!previousStartPos.Equals(placedWords[i].StartPos))
                {
                    count++;
                }

                placedWordWithDeets.Add(new PlacedWord
                {
                    Word = placedWords[i].Word,
                    StartPos = placedWords[i].StartPos,
                    Direction = placedWords[i].Direction,
                    DefinitionIndex = (count).ToString(),
                    Definition = definition,
                });

                previousStartPos = placedWords[i].StartPos;
            }

            return placedWordWithDeets;
        }

        // Get new word from WordList Words based no letter count
        public string RetrieveWord(List<string> usedWords, int letterCount)
        {

            var wordList = WordList.Instance().WordAndDefinitions.Select(w => w.Word).ToArray();

            var wordArray = wordList.Where(w => w.Length == letterCount).
                Except(usedWords).
                ToArray();

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
            
            return wordArray[_random.Next(wordArray.Length)]; ;

        }

        // Try placing all words on board itterating through _wordSizes array for word sizes
        private void PlaceWords()
        {
            _count++;
            PlacedWords.Clear();

            // First Word
            PlaceWords_FirstWord();

            Debug.WriteLine("count {0} ", _count);

            // Rest of board
            var direction = WordDirection.Vertical;

            // Sequence through word size array
            for (var i = 1; i < _wordSizes.Length; i++)
            {

                var wordAndPosList = FindWordAndPositions(_wordSizes[i], direction);

                if (wordAndPosList != null)
                {

                    var startPositions = new List<Tuple<int, int>>(wordAndPosList.StartPosList);

                    var word = wordAndPosList.Word;

                    var usedCharsIndex = new List<int> { };

                    if (startPositions.Count > 0)
                    {

                        var whileCount = 0;

                        // Try start positions until word placement is found
                        while (whileCount < startPositions.Count)
                        {
                            // Randomly itterate through start positions
                            var listIndex = ReturnRandomNumberExcludingArrayInts(startPositions.Count, usedCharsIndex);
                            usedCharsIndex.Add(listIndex);

                            var startPos = startPositions[listIndex];

                            var placedWord = CrossWordBoard.Instance().PlaceWord(word, startPos, direction);

                            if (placedWord != null)
                            {

                                _usedWords.Add(word);

                                if (direction == WordDirection.Horizontal)
                                {
                                    direction = WordDirection.Vertical;
                                }
                                else
                                {
                                    direction = WordDirection.Horizontal;
                                }

                                PlacedWords.Add(placedWord);

                                _wordsPlaced++;

                                // Break from while if wordcount reached
                                if (_wordsPlaced >= _minWordsPlaced)
                                {
                                    break;
                                }

                                break;
                            }

                            whileCount++;
                        }

                    }

                }

            }

        }

        // Place first word on board
        private void PlaceWords_FirstWord()
        {
            var word = RetrieveWord(new List<string> { }, _wordSizes[0]);

            var random = new Random();
            var horizontalPos = random.Next(0, 1);
            var vertiacalPos = random.Next(4, 6);
            var randomStartPos = new Tuple<int, int>(horizontalPos, vertiacalPos);

            // Place first word
            var placedWord = CrossWordBoard.Instance().PlaceWord(word, randomStartPos, WordDirection.Horizontal);
            if (placedWord != null)
            {
                PlacedWords.Add(placedWord);
            }
            else
            {
                Debug.WriteLine("IsNull");
            }
            _usedWords.Add(word);
        }

        // Itterates through board looking for potential letters where words can intersect
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

                    var posList = CrossWordBoard.Instance().ReturnWordStartPositions(word, wordCharCount, direction);
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

            var wordAndPosList = new WordAndStartPositions
            {
                StartPosList = new List<Tuple<int, int>>(starPosList),
                Word = word
            };

            return wordAndPosList;
        }

        // Returns a random number from a int List considering the used numbers
        public int ReturnRandomNumberExcludingArrayInts(int length, List<int> usedNumbers)
        {
            var sequenceArray = Enumerable.Range(0, length).ToArray();

            var availableInts = sequenceArray.Except(usedNumbers).ToArray();


            var random = new Random();

            var result = availableInts[random.Next(0, availableInts.Length)];
            
            return result;
        }


        // Generate board that holds the across and down definition numbers
        public void ApplyDefinitionLocationsToDisplayBoard(IEnumerable<PlacedWord> placedWords, ObservableCollection<ObservableCollection<Cell>> displayBoard)
        {
            foreach (var placedWord in placedWords)
            {

                if (placedWord.DefinitionIndex != null)
                {
                    displayBoard[placedWord.StartPos.Item2][placedWord.StartPos.Item1].DefinitionLocation = placedWord.DefinitionIndex;
                }

            }


        }
    }

}
