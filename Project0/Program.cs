
// To do list
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
 * 
-EditTask:
Edits the description of a task by ID.
 
 */

public class Task
{
    //defines
    public int Id { get; set; }
    public string Describtion { get; set; }
    public bool IsCompleted { get; set; }

    //The Constructor
    public Task{

    id = Id ;
    Describtion = describtion ;
    IsCompleted = false ;

}

//To mark the tasks
public void MarkCompleted
{
    IsCompleted = true ;

}

// Override The string method for better view
public override string ToString()
{
    return $"{Id}. {Description} - {(IsCompleted ? "Completed" : "Pending")}";
}


public class ToDoList()
{
    //create list to store tasks and counter for the next ones
    private List<Task> tasks;
    private int nextId = 0;
}

//Constructor
public ToDoList()
{
    tasks = new List<Task>();
    nextId = 1;
}

// to add tasks
public void AddTask()
{
    Task newTask = new Task(nextId, describtion);
    tasks.Add(newTask);
    nextId++;
    Console.WriteLine("Task added successfully.");
}




}