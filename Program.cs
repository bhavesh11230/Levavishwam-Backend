
//using Levavishwam_Backend.RepositoryLayer;
//using Levavishwam_Backend.ServiceLayer;
//using Levavishwam_Backend.Services;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

//namespace Levavishwam_Backend
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);


//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            builder.Services.AddSingleton<JwtService>();       
//            builder.Services.AddScoped<IAuthRL, AuthRL>();
//            builder.Services.AddScoped<IAuthSL, AuthSL>();

//            builder.Services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
//        )
//    };
//});


//            var app = builder.Build();

//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}


using Levavishwam_Backend.Data;
using Levavishwam_Backend.RepositoryLayer.ImplementationRL;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.ImplementationSL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Levavishwam_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Levavishwam_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ------------------------- CORS CONFIG -------------------------
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins(
                        "http://localhost:5173",
                         "https://localhost:5173"
                         )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();    
                    });
            });
            // ----------------------------------------------------------------

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // --------------------- DATABASE CONTEXT (MANDATORY) ---------------------
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSettingConnection")));
            // ------------------------------------------------------------------------

            //// --------------------- AUTH MODULE DI ---------------------
            //builder.Services.AddSingleton<JwtService>();
            //builder.Services.AddScoped<IAuthRL, AuthRL>();
            //builder.Services.AddScoped<IAuthSL, AuthSL>();
            // ----------------------------------------------------------

            // --------------------- HOME MODULE DI ---------------------
            builder.Services.AddScoped<IHomeRepository, HomeRepository>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            // ----------------------------------------------------------

            // --------------------- JWT AUTH ---------------------------
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                    )
                };
            });
            // ----------------------------------------------------------

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // CORS must come before Auth
            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
