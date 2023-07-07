namespace Wordle_Library
{
    public class Logic
    {
        readonly List<string> listword = new List<string>();
        public Logic(List<string> word)
        {
            this.listword = word;
        }

        // Test Method to check correct open file
        public void GetList()
        {
            foreach (string word in listword)
            {
                Console.WriteLine(word);
            }
        }
    }
}