namespace Wordle_Library
{
    public class Logic
    {
        public const int MaxTurn = 6;
        public const char InitilizedMatrixSymbol = '*';
        private readonly List<string> _listword = new();
        private readonly List<char> existValue = new();
        private int _wordLength = 0;
        private string _word = "";

        public Logic(List<string> word)
        {
            this._listword = word.ConvertAll(low => low.ToLower());
        }

        public List<string> Listword { get { return new List<string>(_listword); } }

        public int WordLength
        {
            get { return _wordLength; }
            private set { _wordLength = value; }
        }

        private string Word
        {
            get { return _word; }
            set { _word = value; }
        }

        public List<char> ExistValue { get { return existValue.Distinct().ToList(); } }

        public string ChooseRandomWord()
        {
            Random roulette = new(DateTime.Now.Day);
            int randomNumber = roulette.Next(0, Listword.Count);
            string word = Listword[randomNumber];
            WordLength = word.Length;
            Word = word;
            return Word;
        }

        public char[,] CreateGameMatrix()
        {
            if (WordLength < 2)
            {
                throw new Exception("Use the ChooseRandomWord() function first to set Matrix length");
            }
            char[,] matrix = new char[MaxTurn, WordLength];
            for (int i = 0; i < Logic.MaxTurn; i++)
            {
                for (int j = 0; j < WordLength; j++)
                {
                    matrix[i, j] = InitilizedMatrixSymbol;
                }
            }
            return matrix;
        }

        public bool IsWinner(string word)
        {
            int counter = 0;

            for (int i = 0; i < WordLength; i++)
            {
                if (word[i] == Word[i])
                {
                    counter++;
                }
            }
            return counter == WordLength;
        }

        public void InserIntoMatrix(int attempt, string recive, char[,] matrix)
        {
            char[] recivetochar = recive.ToCharArray();
            existValue.Clear();

            for (int i = 0; i < WordLength; i++)
            {
                if (recive[i] == Word[i])
                {
                    matrix[attempt, i] = Char.ToUpper(recivetochar[i]);
                }
                else if (Word.Contains(recivetochar[i]))
                {
                    matrix[attempt, i] = recivetochar[i];
                    existValue.Add(recivetochar[i]);
                }
                else
                {
                    matrix[attempt, i] = recivetochar[i];
                }
            }
        }
    }
}