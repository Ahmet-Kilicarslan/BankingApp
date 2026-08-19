using Microsoft.EntityFrameworkCore;
using ClientApi.Data;
using ClientApi.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<ClientDbContext>(options=>
   options.UseSqlite(builder.Configuration.GetConnectionString("ClientDb")));

builder.Services.AddScoped<IClientRepository,ClientRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();
               
app.MapControllers();

app.Run();

