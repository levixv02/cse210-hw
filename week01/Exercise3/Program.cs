using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("Higher");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        }

        Console.Write("Press any key to exit... Do you want to play again? ");
        string playAgain = Console.ReadLine();
        if (playAgain.Equals("yes", StringComparison.OrdinalIgnoreCase))
        {
            Main(args); // Restart the game
        }
        else
        {
            Console.WriteLine("Thanks for playing our game! See you later...");
        }
    }
}
