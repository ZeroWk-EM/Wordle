namespace Wordle_Library
{
    public class Logic
    {
        private readonly List<string> _listword = new List<string>();
        public Logic(List<string> word)
        {
            this._listword = word;
        }

        public List<string> Listword { get { return _listword; } }


    }
}