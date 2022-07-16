using Neo4jClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var client = new BoltGraphClient(new Uri("bolt://localhost:7687"),"neo4j","root");


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


client.ConnectAsync();

builder.Services.AddSingleton<IGraphClient>(client);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
