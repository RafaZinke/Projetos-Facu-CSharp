using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar serviço de CORS, definindo uma política chamada "PermitirReactApp"
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // origem do seu front-end React
            .AllowAnyHeader()                      // permite qualquer header
            .AllowAnyMethod();                     // permite GET, POST, PUT, DELETE etc.
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Registrar o DbContext do EF com PostgreSQL
builder.Services.AddDbContext<ReservaDbContext>(options =>
    options.UseNpgsql("Host=localhost;Database=reservasdb;Username=postgres;Password=postgres"));
// comentário original: rodar o dotnet ef database update na nova maquina dentro do console de gerenciador de pacotes
// comentário original: rodar antes do update dotnet tool install --global dotnet-ef caso de erro
// comentário original: entrar em http://localhost:5041/swagger após executar

var app = builder.Build();

// 3. Aplicar a política de CORS antes de mapear os controllers
app.UseCors("PermitirReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
