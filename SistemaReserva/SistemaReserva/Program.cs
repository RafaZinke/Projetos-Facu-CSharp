using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ReservaDbContext>(options =>
options.UseNpgsql("Host=localhost;Database=reservasdb;Username=postgres;Password=postgres"));
//rodar o dotnet ef database update na nova maquina dentro do console de gerenciador de pacotes
//rodar antes do update dotnet tool install --global dotnet-ef caso de erro
//entrar em http://localhost:5041/swagger após executar


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
