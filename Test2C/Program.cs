using Microsoft.EntityFrameworkCore;
using Test2C.Data;
using Test2C.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    ctx.Database.Migrate();
}

app.UseAuthorization();
app.MapControllers();
app.Run();