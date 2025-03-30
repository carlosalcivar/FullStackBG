
using es_producto.Controllers.Constans;
using es_producto.Controllers.Impl;
using es_producto.Repository.Contract;
using es_producto.Repository.Db;
using es_producto.Repository.Impl;
using es_producto.Service.Contract;
using es_producto.Service.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace es_producto
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

            #region Autenticacion

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //Valida el Issuer
                        ValidIssuer = ISSUER,

                        ValidateAudience = true, //Valida el Audience
                        ValidAudience = AUDIENCE,

                        ValidateIssuerSigningKey = true, //Valida el key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEYSECERT)),

                        ValidateLifetime = true, //Valida la expiracion del token
                        ClockSkew = TimeSpan.Zero //Quita los 5 min de tolerancia
                    };
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

                // Configura la autenticación
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Token de autorización en el formato 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
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
