using Wordle_Library;

namespace Tic_Tac_Toe;

internal class Program
{

    static public void Main()
    {
        const string FilePath = @"C:\Users\EmanueleMoncada\Desktop\fakedb\wordTable.txt";
        try
        {
            List<string> wordlist = File.ReadAllLines(FilePath).ToList();
            Logic game = new(wordlist);
            game.GetList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found! IDIOT");
        }

    }
}