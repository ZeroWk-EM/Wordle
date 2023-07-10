using Wordle_Library;

namespace Tic_Tac_Toe;

internal class Program
{
    // DEGUB FUNCTION TO PRINT ALL LIST
    public static void PrintList(List<string> listToPrint)
    {
        foreach (string word in listToPrint)
        {
            Console.WriteLine(word);
        }
    }

    public static void PrintGrid(int wordLength)
    {
        for (int i = 0; i < Logic.MaxTurn; i++)
        {
            for (int j = 0; j < wordLength; j++)
            {
                Console.Write("[]");
            }
            Console.WriteLine();
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
                PrintGrid(winnerWord.Length);
                Console.ReadKey();
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