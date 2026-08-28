using AccountApi.Data;
using AccountApi.Repositories;
using AccountApi.Repositories.Interfaces;
using AccountApi.Services;
using AccountApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AccountDbContext>(options =>

options.UseSqlite(builder.Configuration.GetConnectionString("AccountDb"))
);
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    var rsa = RSA.Create();
    rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(builder.Configuration["Jwt:PublicKey"]), out _);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new RsaSecurityKey(rsa)
    };
});

builder.Services.AddAuthorization();

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
