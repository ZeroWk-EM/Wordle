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

    public static void PrintGameGrid(char[,] matrix, int wordLength)
    {
        for (int i = 0; i < Logic.MaxTurn; i++)
        {
            for (int j = 0; j < wordLength; j++)
            {
                Console.Write($"[{matrix[i, j]}]");
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
            string winnerWord = game.ChooseRandomWord();
            char[,] matrix = game.CreateGameMatrix();
            int attempt = 0;
            Console.WriteLine($"DEBUG - CORRECT WORD [{winnerWord}]");
            Console.WriteLine($"Word have [{game.WordLength}] letter");
            while (turn > 0)
            {
                Console.Write("Insert phrase: ");
                string? toSend = Console.ReadLine();
                if (toSend != null && toSend.Length == game.WordLength)
                {
                    game.InserIntoMatrix(attempt, toSend, matrix);
                    turn--;
                    attempt++;
                    PrintGameGrid(matrix, winnerWord.Length);
                }
                else
                {
                    if (toSend != null)
                    {
                        Console.Write($"Length Error - The word entered is too ");
                        Console.WriteLine(toSend.Length > winnerWord.Length ? "Long" : "Short");
                    }

                }
            };
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