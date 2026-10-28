var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting(); 
//app.UseEndpoints(endpoints => { endpoints.MapHub<GameHub>("/gameHub"); });
app.MapHub<GameHub>("/gameHub");

app.Run();
