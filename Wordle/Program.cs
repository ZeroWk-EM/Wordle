using Wordle_Library;

namespace Tic_Tac_Toe;

internal class Program
{

    public static void PrintList(List<string> listToPrint)
    {
        foreach (string word in listToPrint)
        {
            Console.WriteLine(word);
        }
    }

    static public void Main()
    {
        const string FilePath = @"C:\Users\EmanueleMoncada\Desktop\fakedb\wordTable.txt";
        int turn = Logic.MaxTurn;
        try
        {
            List<string> wordlist = File.ReadAllLines(FilePath).ToList();
            Logic game = new(wordlist);
            // Choose Winner Word
            string winnerWord = game.ChooseRandomWord();
            Console.WriteLine($"DEBUG - PAROLA VINCENTE ==> [{winnerWord}]");
            while (turn > 0)
            {
                turn--;
            }
            Console.WriteLine("You Lose!");
            Console.WriteLine($"The correct answer was [{winnerWord}]");
            Console.ReadKey();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found! IDIOT");
        }
        catch (Exception e)
        {
            //Catch other Exception
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Finish");
        }

    }
}