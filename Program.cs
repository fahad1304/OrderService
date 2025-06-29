var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();                            // 👈 Enable Swagger
app.UseSwaggerUI();                          // 👈 UI for testing

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
