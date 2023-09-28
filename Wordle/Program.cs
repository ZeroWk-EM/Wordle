using NLog;
using System.Configuration;
using Wordle_Library;
using Wordle_Library.Enum;

namespace Wordle
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static void PrintGameBoard(List<Attempt> listToPrint)
        {
            foreach (Attempt word in listToPrint)
            {
                int i = 0;
                foreach (Position position in word.Positions)
                {

                    switch (position)
                    {
                        case Position.Ok:
                            Console.Write($"[\x1b[1;32m{word.Word[i]}\x1b[1;0m]");
                            break;
                        case Position.Wrong:
                            Console.Write($"[\x1b[1;33m{word.Word[i]}\x1b[1;0m]");
                            break;
                        case Position.Missing:
                            Console.Write($"[\x1b[1;31m{word.Word[i]}\x1b[1;0m]");
                            break;
                    }
                    i++;
                }
                Console.WriteLine();
            }
        }



        static public void Main()
        {
            Console.Title = "Wordle Game";
            string? path = ConfigurationManager.AppSettings["FilePath"];
            int turn = Logic.MaxTurn;
            string winnerWord;
            bool winning = false;
            if (path != null)
            {
                try
                {
                    List<string> wordlist = File.ReadAllLines(path).ToList();

                    if (wordlist.Count == 0)
                    {
                        Console.WriteLine("\u001b[1;31mCould not start the game because of an unexpected error, please contact the technical support.\x1b[1;0m");
                        logger.Error("LOG - The collection is empty");
                        Environment.Exit(1);
                    }
                    Logic game = new(wordlist);
                    winnerWord = game.ChooseRandomWord();
                    Console.WriteLine(winnerWord);
                    Console.WriteLine($"Word have [{game.WordLength}] letter");
                    while (turn > 0)
                    {
                        Console.Write("\nInsert phrase: ");
                        string? toSend = Console.ReadLine();
                        if (toSend != null)
                        {
                            toSend = toSend.Trim().ToLower().Replace(" ", "");
                            if (toSend.Length == game.WordLength)
                            {
                                game.VerifyPosition(toSend);
                                turn--;
                                PrintGameBoard(game.GameBoard);
                                if (game.IsWinner(toSend))
                                {
                                    turn = 0;
                                    winning = true;
                                }
                            }
                            else
                            {
                                Console.Write($"\u001b[1;31mLength Error - The word entered is too ");
                                Console.WriteLine(toSend.Length > winnerWord.Length ? "Long\u001b[1;0m" : "Short\u001b[1;0m");
                            }
                        }
                    };
                    Console.WriteLine(winning ? "\n\u001b[1;32mYou Win\u001b[1;0m" : $"\n\u001b[1;31mI'm sorry but you lose!\nCorrect word is \u001b[1;0m\u001b[1;96m{winnerWord}\u001b[1;0m");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    logger.Error($"LOG - {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Specify path in app.setting");
                logger.Error($"LOG - Path in app.setting is null");
            }
        }
    }
}