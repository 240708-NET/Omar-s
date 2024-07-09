

class Program
	{

		static void Main(String[] args)
		{

			Console.WriteLine(" --- Rock Paper scissors --- ");

			string PlayerMove;
			String ComputerMove;
			Random rand = new Random();
			string[] moves = { "Rock", "Paper", "scissors" };

			

				Console.WriteLine("Enter Your Move (Rock, Paper, Scissors): ");
				PlayerMove = Console.ReadLine();

				if (PlayerMove != "Rock" && PlayerMove != "Paper" && PlayerMove != "Scissors");
				{
					
					
				}

				ComputerMove = moves[rand.Next(3)];
				Console.WriteLine("Computer Chose : " + ComputerMove);


				if (ComputerMove == PlayerMove)
				{
					Console.WriteLine("It's a Draw !");
				}
				else if (
					(PlayerMove == "Rock" && ComputerMove == "Scissors") ||
					(PlayerMove == "Paper" && ComputerMove == "Rock") ||
					(PlayerMove == "Scissors" && ComputerMove == "Paper")
						)

				{
					Console.WriteLine("You win !");
				}

				else
				{
					Console.WriteLine(" Oops Computer Win !");
				}

			
		}
	}




