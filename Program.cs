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
// AUTH LAYERS
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.ImplementationSL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Levavishwam_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Levavishwam_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ------------------- CORS -------------------
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")

                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
            // -------------------------------------------------

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // ------------------- DATABASE -------------------
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBSettingConnection"));
            });
            // -------------------------------------------------

            // ------------------- AUTH DI -------------------
            builder.Services.AddSingleton<JwtTokenGenerator>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();


            // ------------------- MENU MANAGEMENT DI -------------------
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            // -------------------------------------------------

            // ------------------- HOME PAGE DI (FIX FOR YOUR 500 ERROR) -------------------
            builder.Services.AddScoped<IHomeRepository, HomeRepository>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            // -------------------------------------------------

            // --------------------- PROFILE MODULE DI (ADDED) ---------------------
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();


            // --------------------- FileUpload MODULE DI (ADDED) ---------------------
            builder.Services.AddScoped<IUploadService, UploadService>();
            builder.Services.AddScoped<IFileService, FileService>();


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


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");   
            app.UseAuthentication();
            app.UseAuthorization();

            var uploadsRoot = Path.Combine(builder.Environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsRoot))
            {
                Directory.CreateDirectory(uploadsRoot);
            }

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/Uploads"
            });

            app.MapControllers();

            app.Run();


        }
    }
}