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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBankingUi",
        policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient("CustomerApi",
    client => { client.BaseAddress = new Uri(builder.Configuration["CustomerApiUrl"]); });

builder.Services.AddHttpClient("AuthApi",
    client => { client.BaseAddress = new Uri(builder.Configuration["AuthApiUrl"]); });

builder.Services.AddHttpClient("AccountApi",
    client => { client.BaseAddress = new Uri(builder.Configuration["AccountApiUrl"]); });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseCors("AllowBankingUi");

app.MapControllers();

app.Run();