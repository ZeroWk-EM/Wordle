namespace Wordle_Library
{
    public class Logic
    {
        private readonly List<string> _listword = new();
        private int _wordLength = 0;
        private string _word = "";
        public const int MaxTurn = 6;
        public const char InitilizedMatrixSymbol = '_';

        public Logic(List<string> word)
        {
            this._listword = word;
        }
        public List<string> Listword { get { return _listword; } }
        public int WordLength
        {
            get { return _wordLength; }
            set { _wordLength = value; }
        }
        private string Word
        {
            get { return _word; }
            set { _word = value; }
        }
        public string ChooseRandomWord()
        {
            Random roulette = new();
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


        public void InserIntoMatrix(int attempt, string recive, char[,] matrix)
        {
            char[] recivetochar = recive.ToCharArray();

            for (int i = 0; i < WordLength; i++)
            {
                if (recive[i] == Word[i])
                {
                    matrix[attempt, i] = Char.ToUpper(recivetochar[i]);
                }
                else
                {
                    matrix[attempt, i] = recivetochar[i];
                }
            }
        }
    }
}