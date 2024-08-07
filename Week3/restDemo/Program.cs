
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<User> users = new List<User>
{
    new User(1, "John", "Doe"),
    new User(2, "Jane", "Doe")
};

// List users
app.MapGet("/users", () => users);

// Get user by id
app.MapGet("/users/{id}", (int id) =>
{
    var user = users.FirstOrDefault(user => user.Id == id);
    if (user != null)
    {
        return Results.Ok(user);
    }
    else
    {
        return Results.NotFound("User with this id does not exist");
    }
});

// Create user
app.MapPost("/users/", ([FromBody] User user) => {
    users.Add(user);
    return Results.Created($"User {user.Firstname} added successfully", user);
}); 

// Update user first name


app.MapPatch("/users/Rename/{id}", (int id) => {
    string newName = "Kate";
    var user = users.FirstOrDefault(user => user.Id == id);
     if(user != null){
        user.Firstname = newName;
        return Results.Ok($"User with id {id} was renamed");
    }
    else{
        return Results.NotFound("User with this id does not exist");
    }


});

app.Run();
