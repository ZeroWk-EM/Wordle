namespace Wordle_Library
{
    public class Logic
    {
        private readonly List<string> _listword = new List<string>();
        public const int MaxTurn = 6;
        public Logic(List<string> word)
        {
            this._listword = word;
        }

        public List<string> Listword { get { return _listword; } }

        public string ChooseRandomWord()
        {
            Random roulette = new Random();
            int randomNumber = roulette.Next(0, Listword.Count);
            return Listword[randomNumber];
        }

    }
}