var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRouting();

var app = builder.Build();



app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
