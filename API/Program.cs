using ConcertApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to my API");

const string getConcert = "GetConcert";

app.MapPost("/createaccount", async (HttpRequest request) =>
{
    var form = await request.ReadFormAsync();

    User newUser = new User(form["firstname"], 
                            form["lastname"], 
                            form["password"]);

    if (User.IsValidAccount(newUser))
    {
        User.CreateAccount(newUser);
        return Results.Ok($"New user added {newUser.FirstName} {newUser.LastName}");
    }
    else
    {
        return Results.BadRequest("Invalid account details. Password must contain atleast 7" +
             "characters, contain one digit and one upperchase character.");
    }
});

app.MapPost("/login", async (HttpRequest request) =>
{
    var form = await request.ReadFormAsync();

    User newUser = new User(form["firstname"], 
                            form["lastname"], 
                            form["password"]);

    if (User.TryLogin(newUser))
    {
        return Results.Ok("You have now logged in.");
    }
    else
    {
        return Results.BadRequest("Account was not found.");
    }
});

app.MapGet("/concerts", () => Concert.concerts);

app.MapGet("/concerts/{id}", (int id) =>
{
    var concert = Concert.concerts.Find(x => x.Id == id);

    if (concert == null)
        return Results.NotFound($"Concert with id:{id} was not found.");

    else return Results.Ok(concert);

}).WithName(getConcert);

app.MapPost("/concerts/", async (HttpRequest request) =>
{
    var form = await request.ReadFormAsync();

    if (!DateTime.TryParse(form["date"], out var date))
        return Results.BadRequest("Invalid date and time format.");

    else if (!int.TryParse(form["tickets"], out int tickets))
        return Results.BadRequest("Not a valid number of tickets.");

    else
    {
        var newConcert =
        new Concert(
            form["artist"],
            form["location"],
            date,
            tickets
            );

        Concert.concerts.Add(newConcert);
        return Results.CreatedAtRoute(getConcert, new { id = newConcert.Id }, newConcert);
    }
    
});

app.Run();
