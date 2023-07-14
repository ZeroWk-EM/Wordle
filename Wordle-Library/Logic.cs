namespace Wordle_Library
{
    public class Logic
    {
        public const int MaxTurn = 6;
        public const char InitilizedMatrixSymbol = '*';
        private readonly List<string> _listword = new();
        private readonly List<char> _existValue = new();
        private readonly List<string> _gameBoard = new();
        private int _wordLength = 0;
        private string _word = "";

        public Logic(List<string> word)
        {
            this._listword = word.ConvertAll(low => low.ToLower().Trim());
        }

        public List<string> Listword
        {
            get
            {
                return new List<string>(_listword);
            }
        }

        public List<char> ExistValue
        {
            get
            {
                return new List<char>(_existValue.Distinct().ToList());
            }
        }

        public List<string> GameBoard
        {
            get
            {
                return new List<string>(_gameBoard);
            }
        }

        public int WordLength
        {
            get { return _wordLength; }
            private set { _wordLength = value; }
        }

        public string Word
        {
            get { return _word; }
            private set { _word = value; }
        }

        public string ChooseRandomWord()
        {
            Random roulette = new(Environment.TickCount);
            int randomNumber = roulette.Next(0, Listword.Count);
            string word = Listword[randomNumber];
            WordLength = word.Length;
            Word = word;
            return Word;
        }

        public bool IsWinner(string word)
        {
            foreach (string s in Listword)
            {
                if (s == word) return true;
            }
            return false;
        }

        public void InsertWord(string wordToInsert)
        {
            string toAdd = "";
            _existValue.Clear();
            // Validate input parms
            if (wordToInsert.Length != WordLength)
            {
                throw new ArgumentException("The word you entered is bigger or lower than the winning word");
            }

            for (int i = 0; i < WordLength; i++)
            {
                if (wordToInsert[i] == Word[i])
                {
                    toAdd += Char.ToUpper(wordToInsert[i]);
                }
                else if (Word.Contains(wordToInsert[i]))
                {
                    toAdd += wordToInsert[i];
                    _existValue.Add(wordToInsert[i]);
                }
                else
                {
                    toAdd += wordToInsert[i];
                }
            }
            _gameBoard.Add(toAdd);
        }
    }
}