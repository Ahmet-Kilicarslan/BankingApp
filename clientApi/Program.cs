using ClientApi.Controllers;
using ClientApi.Data;
using ClientApi.Middleware;
using ClientApi.Repositories;
using ClientApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<ClientDbContext>(options=>
   options.UseSqlite(builder.Configuration.GetConnectionString("ClientDb")));

builder.Services.AddScoped<IClientRepository,ClientRepository>();
builder.Services.AddScoped<IClientService,ClientService>();

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters { 
    
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience= builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    
    
    };

});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

