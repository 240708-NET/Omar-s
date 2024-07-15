
class Program{


        static void Main( string[] args){

        Console.WriteLine("High/Low Running");

        
        //Variables
        int targetNumber;
        int guessNumber;
        int roundCount = 0;
        string guessString;


        // Creating a Random number;

        Random rand = new Random();
        targetNumber = rand.Next(11);




        do{
           //roundCount = roundCount +1;
           //roundCount +=1;
           roundCount++;

        Console.Write("Please enter a guess between -1 and 11: " );
        guessString = Console.ReadLine();

        guessNumber = Int32.Parse( guessString );

        if(guessNumber == targetNumber){

        Console.WriteLine("Hey , Nice Job!");

        }

        else if( guessNumber > targetNumber){

        Console.WriteLine("Oops , too high!");

        }

        else{

        Console.WriteLine("Oops , too Low!");

        }
     }while(guessNumber != targetNumber);
        Console.WriteLine("Thanks for playing! ");
        Console.WriteLine("You took {0} rounds to guess the answer!", roundCount);

  }


}
