using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);
// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
    services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // ignore omitted parameters on models to enable optional params (e.g.User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddScoped<IUserService, UserService>();
}
var app = builder.Build();
// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.MapControllers();
}
app.Run("http://localhost:4000");


// using System.Text.Json.Serialization;
// using Microsoft.EntityFrameworkCore;
// using WebApplication1.Helpers;
// using WebApplication1.Services;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Services
// var services = builder.Services;
//
// // 👉 Connexion à PostgreSQL
// services.AddDbContext<DataContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));
//
// services.AddCors();
// services.AddControllers().AddJsonOptions(x =>
// {
//     x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//     x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
// });
//
// services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// services.AddScoped<IUserService, UserService>();
//
// var app = builder.Build();
//
// // Pipeline
// app.UseCors(x => x
//     .AllowAnyOrigin()
//     .AllowAnyMethod()
//     .AllowAnyHeader());
//
// app.UseMiddleware<ErrorHandlerMiddleware>();
// app.MapControllers();
//
// app.Run("http://localhost:4000");
