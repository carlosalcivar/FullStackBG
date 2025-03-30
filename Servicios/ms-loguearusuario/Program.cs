
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ms_loguearusuario.Controllers.Constans;
using ms_loguearusuario.Controllers.Impl;
using ms_loguearusuario.Repository.Contract;
using ms_loguearusuario.Repository.Db;
using ms_loguearusuario.Repository.Impl;
using ms_loguearusuario.Service.Contract;
using ms_loguearusuario.Service.Impl;
using Serilog;

namespace ms_loguearusuario
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Validar parametros
            string HOSTNAME = Environment.GetEnvironmentVariable("HOSTNAME")!;
            if (string.IsNullOrEmpty(HOSTNAME)) throw new ArgumentException("No esta definida la variable HOSTNAME");

            string PORT = Environment.GetEnvironmentVariable("PORT")!;
            if (string.IsNullOrEmpty(PORT)) throw new ArgumentException("No esta definida la variable PORT");

            string DBNAME = Environment.GetEnvironmentVariable("DBNAME")!;
            if (string.IsNullOrEmpty(DBNAME)) throw new ArgumentException("No esta definida la variable DBNAME");

            string USER = Environment.GetEnvironmentVariable("USER")!;
            if (string.IsNullOrEmpty(USER)) throw new ArgumentException("No esta definida la variable USER");

            string PASSWORD = Environment.GetEnvironmentVariable("PASSWORD")!;
            if (string.IsNullOrEmpty(PASSWORD)) throw new ArgumentException("No esta definida la variable PASSWORD");

            var Connection = builder.Configuration.GetConnectionString("SQLConnection");
            if (string.IsNullOrEmpty(Connection)) throw new ArgumentException("La variable 'SQLConnection' no está definida en el archivo de configuración.");

            string ISSUER = Environment.GetEnvironmentVariable("ISSUER")!;
            if (string.IsNullOrEmpty(ISSUER)) throw new ArgumentException("No esta definida la variable ISSUER");

            string AUDIENCE = Environment.GetEnvironmentVariable("AUDIENCE")!;
            if (string.IsNullOrEmpty(AUDIENCE)) throw new ArgumentException("No esta definida la variable AUDIENCE");

            string EXPIRE = Environment.GetEnvironmentVariable("EXPIRE")!;
            if (string.IsNullOrEmpty(EXPIRE)) throw new ArgumentException("No esta definida la variable EXPIRE");

            string KEYSECERT = Environment.GetEnvironmentVariable("KEYSECERT")!;
            if (string.IsNullOrEmpty(KEYSECERT)) throw new ArgumentException("No esta definida la variable KEYSECERT");

            string KEYENCRIT = Environment.GetEnvironmentVariable("KEYENCRIT")!;
            if (string.IsNullOrEmpty(KEYENCRIT)) throw new ArgumentException("No esta definida la variable KEYENCRIT");

            string LOG_DIRECTORY = Environment.GetEnvironmentVariable("LOG_DIRECTORY")!;
            if (string.IsNullOrEmpty(LOG_DIRECTORY)) throw new ArgumentException("No esta definida la variable LOG_DIRECTORY");

            string ORIGIN_CORS = Environment.GetEnvironmentVariable("ORIGIN_CORS")!;
            if (string.IsNullOrEmpty(ORIGIN_CORS)) throw new ArgumentException("No esta definida la variable ORIGIN_CORS");
            #endregion

            #region Conexion Base de datos
            Connection = Connection.Replace("{HostName}", HOSTNAME);
            Connection = Connection.Replace("{Port}", PORT);
            Connection = Connection.Replace("{DBName}", DBNAME);
            Connection = Connection.Replace("{User}", USER);
            Connection = Connection.Replace("{Password}", PASSWORD);

            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(Connection));

            #endregion

            #region Log
            // Configuración básica de Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{LOG_DIRECTORY}/log-{General.Tipo_Servicio}-{General.Nombre_Servicio}-.txt", rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .CreateLogger();

            #endregion

            #region Cors
            // Obtener la configuracion de CorsOptions desde appsettings.json
            var corsOptions = ORIGIN_CORS.Split(',');

            // Configurar la politica CORS con la configuracion cargada
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Cors_Tienda", builder =>
                {
                    builder.WithOrigins(corsOptions.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            #endregion

            // Add services to the container.
            builder.Services.AddScoped<ControllerImpl>();
            builder.Services.AddScoped<IService, ServiceImpl>();
            builder.Services.AddScoped<IRepository, RepositoryImpl>();
            builder.Services.AddHealthChecks();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = General.Nombre_Servicio + "-" + General.Tipo_Servicio, Version = "v1" });

            });

            // Reemplaza el logger por defecto con Serilog
            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", General.Nombre_Servicio + "-" + General.Tipo_Servicio + " v1");
                });
            }

            app.UseCors("Cors_Tienda");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseHealthChecks("/health");
            app.Run();
        }
    }
}
