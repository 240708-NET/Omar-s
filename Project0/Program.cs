
// Task Minder

/*

-AddTask:
 Adds a new task to the list.
 * 
-RemoveTask:
 Removes a task from the list by ID.
 * 
-ViewTasks:
 Displays all tasks in the list.
 * 
-MarkTaskComplete:
Marks a task as complete by ID.


Task Minder menu offers the following options:

- Add a Task: Prompts the user to enter a task description and adds it to the list.
- Remove a Task: Displays all tasks and prompts the user to enter the ID of the task to remove.
- View Tasks: Displays all tasks with their current status.
- Mark a Task as Complete: Displays all tasks and prompts the user to enter the ID of the task to mark as complete.

 */

public class Task
{
    // Properties
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    // The Constructor
    public Task(int id, string description)
    {
        Id = id;
        Description = description;
        IsCompleted = false;
    }

    // To mark the tasks
    public void MarkComplete()
    {
        IsCompleted = true;
    }

    // Override The string method for better view
    public override string ToString()
    {
        return $"{Id}. {Description} - {(IsCompleted ? "Completed" : "Pending")}";
    }
}




public class ToDoList
{
    // Create list to store tasks and counter for the next ones
    private List<Task> tasks;
    private int nextId ;

    // Constructor
    public ToDoList()
    {
        tasks = new List<Task>();
        nextId = 1;
    }

    // To add tasks
    public void AddTask(string description)
    {
        int newId = tasks.Count + 1; 
        Task newTask = new Task(nextId, description);
        tasks.Add(newTask);
        RenumberTasks();
        nextId++;
        Console.WriteLine("\nTask added successfully.\n");
        ViewTasks();
    }

    // Method to remove a task by ID
    public void RemoveTask(int id)
    {
        Task taskToRemove = tasks.Find(t => t.Id == id);
        if (taskToRemove != null)
        {
            tasks.Remove(taskToRemove);
            RenumberTasks();
               for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }
            Console.WriteLine("\nTask removed successfully.\n");
        }
        else
        {
            Console.WriteLine("\nTask not found.\n");
        }
        ViewTasks();
    }

   // Method to renumber tasks sequentially
    private void RenumberTasks()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].Id = i + 1;
        }
    }

    // Method to display all tasks
    public void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("\nNo tasks available.\n");
        }
        else
        {
             foreach (Task task in tasks)
            {
                if (task.IsCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(task);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(task);
                }
                Console.ResetColor();
            }
        }
    }

    // Method to mark a task as complete by ID
    public void MarkTaskComplete(int id)
    {
        Task taskToComplete = tasks.Find(t => t.Id == id);
        if (taskToComplete != null)
        {
            taskToComplete.MarkComplete();
            Console.WriteLine("\nTask marked as complete. \n ");
        }
        else
        {
            Console.WriteLine("\nTask not found.\n");
        }
        ViewTasks();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ToDoList toDoList = new ToDoList();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("****** Welcome To Task Minder ******\n");
            Console.WriteLine("1. Let's add a Task!");
            Console.WriteLine("2. Want to Remove a Task?");
            Console.WriteLine("3. To-Do List? Let's see it!");
            Console.WriteLine("4. Task Complete? High-five!");
            Console.WriteLine("5. Exit - Are you sure?");
            Console.Write("Choose your adventure (1-5): ");

            string input = Console.ReadLine();
            Console.Clear();

            if (input == "1")
            {
                Console.Write("Enter Task Description: ");
                string description = Console.ReadLine();
                toDoList.AddTask(description);
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (input == "2")
            {
                toDoList.ViewTasks();
                Console.Write("Enter Task Id to Remove: ");
                if (int.TryParse(Console.ReadLine(), out int removeid))
                {
                    toDoList.RemoveTask(removeid);
                }
                else
                {
                    Console.Write("Invalid Id.");
                }
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (input == "3")
            {
                toDoList.ViewTasks();
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (input == "4")
            {
                toDoList.ViewTasks();
                Console.Write("Enter Task Id to Mark as complete: ");
                if (int.TryParse(Console.ReadLine(), out int completed))
                {
                    toDoList.MarkTaskComplete(completed);
                }
                else
                {
                    Console.Write("Invalid Id.");
                }
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (input == "5")
            {
                Console.WriteLine("Exiting Task Minder....");
                isRunning = false;
            }
            else
            {
                Console.WriteLine("Invalid number. Please choose an option from (1-5).");
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        Console.WriteLine("Thank you for using TaskMinder!");
    }
}
