using Wordle_Library;

namespace Wordle
{
    internal class Program
    {

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

                    if (matrix[i, j] != Logic.InitilizedMatrixSymbol && matrix[i, j] == Char.ToUpper(matrix[i, j]))
                    {
                        Console.Write($"[\x1b[1;32m{matrix[i, j]}\x1b[1;0m]");
                    }
                    else
                    {
                        Console.Write($"[{matrix[i, j]}]");
                    }
                }
                Console.WriteLine();
            }
        }

        static public void Main()
        {
            const string FilePath = @"C:\Users\EmanueleMoncada\Desktop\fakedb\wordTable.txt";
            int turn = Logic.MaxTurn;
            string winnerWord = "";
            bool winning = false;
            try
            {
                List<string> wordlist = File.ReadAllLines(FilePath).ToList();
                Logic game = new(wordlist);
                winnerWord = game.ChooseRandomWord();
                char[,] matrix = game.CreateGameMatrix();
                int attempt = 0;
                Console.WriteLine($"DEBUG - CORRECT WORD [{winnerWord}]");
                Console.WriteLine($"Word have [{game.WordLength}] letter");
                while (turn > 0)
                {
                    Console.Write("Insert phrase: ");
                    string? toSend = Console.ReadLine();
                    if (toSend != null)
                    {
                        toSend = toSend.Trim().ToLower().Replace(" ", "");
                    }

                    if (toSend != null && toSend.Length == game.WordLength)
                    {
                        game.InserIntoMatrix(attempt, toSend, matrix);
                        turn--;
                        attempt++;
                        Console.Clear();
                        PrintGameGrid(matrix, winnerWord.Length);

                        if (game.ExistValue.Count > 0)
                        {
                            Console.Write("Value ");
                            foreach (char sentence in game.ExistValue)
                            {
                                Console.Write($"[\u001b[1;33m{sentence.ToString().ToUpper()}\u001b[1;0m]");

                            }
                            Console.WriteLine(" exist but the position is wrong");
                        }
                        if (game.IsWinner(toSend))
                        {
                            turn = 0;
                            winning = true;
                        }
                        else if (!game.IsWinner(toSend) && turn > 0)
                        {
                            Console.WriteLine($"Word have [{game.WordLength}] letter");
                        }
                        else
                        {
                            Console.WriteLine("End");
                        }
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
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(winning ? "You Win" : $"I'm sorry but you lose!\nCorrect word is [{winnerWord}]");
        }
    }
}