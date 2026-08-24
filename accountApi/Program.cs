using AccountApi.Data;
using AccountApi.Repositories;
using AccountApi.Repositories.Interfaces;
using AccountApi.Services;
using AccountApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TransactionApi.Repositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AccountDbContext>(options =>

options.UseSqlite(builder.Configuration.GetConnectionString("AccountDb"))
);
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
