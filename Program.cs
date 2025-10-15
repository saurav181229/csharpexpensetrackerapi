using ExpenseTrackerAPI.services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:4000");
builder.Services.AddControllers(); 
builder.Services.AddSingleton<IfileSerivice, FileService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();   
builder.Services.AddSwaggerGen(); 
var app = builder.Build();

//swagger


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
     app.UseSwagger();                         // Generates swagger.json
    app.UseSwaggerUI();   
}
app.UseRouting();
app.UseHttpsRedirection();




app.MapControllers();
app.Run();

