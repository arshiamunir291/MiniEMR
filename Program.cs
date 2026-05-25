using MiniEMR.Infrastructure;
using MiniEMR.Middlewares;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDatabase(builder.Configuration)
    .AddApplicationControllers()
    .AddApplicationServices()
    .AddJwtAuthentication(builder.Configuration)
    .AddApplicationCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("DevCors");
app.UseAuthentication();
app.UseMiddleware<CustomAuthorizationMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();