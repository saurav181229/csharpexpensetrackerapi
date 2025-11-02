using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using CsvHelper;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.model;
using ExpenseTrackerAPI.services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.UseUrls("http://0.0.0.0:8080");
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// builder.WebHost.UseUrls("http://localhost:5000");
builder.Services.AddControllers(); 
builder.Services.AddSingleton<IfileSerivice, FileService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserContextService, UserContextService>();


// builder.Services.AddSingleton<ExpenseContext>();
builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



builder.Services.AddEndpointsApiExplorer();   
builder.Services.AddSwaggerGen();
var app = builder.Build();


//applying migrations

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ExpenseContext>();

//  await DataSeeder.SeedUsersAndAssignExpenseAsync(context);
        context.Database.Migrate();
        // for read from csv
//         if(!context.DisplayExpenses().Any() )
//         {
            
//             var projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
//             var csvPath = Path.Combine(projectRoot, "data", "expenses_50k.csv");
//             using var reader = new StreamReader(csvPath);
//             using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
//             var records = csv.GetRecords<ExpensesCsv>().ToList();

//             var connectionString = context.Database.GetConnectionString();

//              var stopwatch = Stopwatch.StartNew();
//             using (var sqlConnection = new SqlConnection(connectionString))
//             {
//                 sqlConnection.Open();

//                 using var bulkCopy = new SqlBulkCopy(sqlConnection)
//                 {
//                     DestinationTableName = "Expenses",
//                     BatchSize = 5000
//                 };


//                 var table = new DataTable();
              
//                 table.Columns.Add("Date", typeof(DateTime));
//                 table.Columns.Add("Category", typeof(string));
//                 table.Columns.Add("Amount", typeof(decimal));
//                 table.Columns.Add("Description", typeof(string));
//                 table.Columns.Add("UserId", typeof(int));
// Random random = new Random();
//                 foreach (var r in records)
//                 {
//                     int userId = random.Next(1, 4); 
//                     table.Rows.Add(r.Date, r.Category.ToString(), r.Amount, r.Description,userId);
//                 }
                    

//                 bulkCopy.ColumnMappings.Add("Date", "Date");
//     bulkCopy.ColumnMappings.Add("Category", "Category");
//     bulkCopy.ColumnMappings.Add("Amount", "Amount");
//                 bulkCopy.ColumnMappings.Add("Description", "Description");
//                 bulkCopy.ColumnMappings.Add("UserId", "UserId");
//                 bulkCopy.WriteToServer(table);
//             }



//             // using var reader = new StreamReader(csvPath);
//             // using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
//             // var records = csv.GetRecords<ExpensesCsv>().ToList();
//             // var expenses = records.Select(x => new Expense

//             // {
              
//             //     Amount = x.Amount,
//             //     Category = x.Category,
//             //     Date = x.Date,
//             //     Description = x.Description

//             // }).ToList();

//             // context.Expenses.AddRange(expenses);
//             // context.SaveChanges();
//             // stopwatch.Stop();

//         Console.WriteLine($" Bulk insert complete in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");

//         }

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred applying migrations or seeding the DB.");
    }
}


//swagger


// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    // app.MapOpenApi();
    app.UseSwagger();                         // Generates swagger.json
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
// }
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//hello test


app.MapControllers();

try
{
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ExpenseContext>();

    if (context.Database.CanConnect())
    {
        Console.WriteLine("✅ SQL Server connection successful.");
    }
    else
    {
        Console.WriteLine("❌ Cannot connect to SQL Server.");
    }
}
}
catch (Exception ex)
{
    Console.WriteLine(ex.Data);
}




app.Run();

