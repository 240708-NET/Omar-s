
class Game
{
    // Variables
    Random rand = new Random();
    int targetNumber;
    int guessNumber = -1;
    public int roundCount = 0;
    string guessString = "";

    // Constructor
    public Game( )
    {
        targetNumber = rand.Next(11);
    }

    // Methods
    public void PlayGame( )
    {
        do
        {
            roundCount++;

            Console.Write("Please enter a guess between 0 and 10: ");
            guessString = Console.ReadLine();

            if (int.TryParse(guessString, out guessNumber))
            {
                if (guessNumber == targetNumber)
                {
                    Console.WriteLine("Hey, Nice Job!");
                }
                else if (guessNumber > targetNumber)
                {
                    Console.WriteLine("Oops, too high!");
                }
                else
                {
                    Console.WriteLine("Oops, too low!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (guessNumber != targetNumber);
    }
}
