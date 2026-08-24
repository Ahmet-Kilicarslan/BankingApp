using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Repositories;
using TransactionApi.Repositories.Interfaces;
using TransactionApi.Services;
using TransactionApi.Services.Interfaces;
using TransactionApi.Middleware;

var builder = WebApplication.CreateBuilder(args);



/*
 
 dotnet ef  migrations add something
 dotnet ef database update
 
 */

builder.Services.AddOpenApi();

builder.Services.AddDbContext<TransactionDbContext>(options =>

options.UseSqlite(builder.Configuration.GetConnectionString("TransactionDb"))
);

builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ITransactionRepository,TransactionRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient("ClientApi", client => {
    client.BaseAddress = new Uri(builder.Configuration["ClientApiUrl"]);
});

builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AuthApiUrl"]);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();


