// Task_Minder
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
        Task newTask = new Task(nextId, description);
        tasks.Add(newTask);
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
                Console.WriteLine(task);
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
            Console.WriteLine("\nTask marked as complete.\n");
        }
        else
        {
            Console.WriteLine("\nTask not found.\n");
        }
        ViewTasks();
    }
}
