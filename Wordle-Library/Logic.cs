using System.ComponentModel.DataAnnotations;

namespace Wordle_Library
{
    public class Logic
    {
        private readonly List<string> _listword = new List<string>();
        private int _wordLength = 0;
        private string _word = "";
        public const int MaxTurn = 6;

        public Logic(List<string> word)
        {
            this._listword = word;
        }

        public List<string> Listword { get { return _listword; } }

        private int WordLength
        {
            get { return _wordLength; }
            set { _wordLength = value; }
        }

        private string Word
        {
            get { return _word; }
            set { _word = value; }
        }


        // FUNCTION TO CHOOSE RANDOM WORD
        public string ChooseRandomWord()
        {
            Random roulette = new();
            int randomNumber = roulette.Next(0, Listword.Count);
            string word = Listword[randomNumber];
            WordLength = word.Length;
            Word = word;
            return Word;
        }

        public string[,] CreateGameMatrix()
        {
            if (WordLength < 2)
            {
                throw new Exception("Use the ChooseRandomWord() function first to set Matrix length");
            }

            string[,] gameMatrix = new string[MaxTurn, WordLength];
            return gameMatrix;
        }

   
    }
}