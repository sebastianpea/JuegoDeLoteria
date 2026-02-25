using Servidor.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<HubDeJuego>("/juego");
app.Run("http://0.0.0.0:5000");