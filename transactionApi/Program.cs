using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Repositories;
using TransactionApi.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<TransactionDbContext>(options =>

options.UseSqlite(builder.Configuration.GetConnectionString("TransactionDb"))
);

builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ITransactionRepository,TransactionRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


