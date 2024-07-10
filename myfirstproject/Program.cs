public class Program{

	static void Main(string[] args){
        
	Console.WriteLine("Hello, World!");

	Console.WriteLine("Please enter your name : ");
	
	string username = "Omar";

	username = "something";

	username = Console.ReadLine();

	//Console.WriteLine("Welcome to Revature: ");

	//Console.WriteLine( username );

	//Console.WriteLine("Welcome to Revature: " + username);
	
	//Console.WriteLine("Welcome to Revature: {0}" , username );

	Console.WriteLine($"Welcome to Revature: {username}");
  
	bool runChoice = true;

if(runChoice == true)
{
  Console.WriteLine("runChoice is true");
}
else if(runChoice == false)
{
  Console.WriteLine("runChoice is false");
}
else{
Console.WriteLine("nothing");
}

	


}


}
