
//using AlShamil.Business;
//using AlShamil.Business.Interface;
using AlShamil.BusinessEF;
using AlShamil.BusinessEF.Interface;
using AlShamil.Data;
using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using AlShamilEntityData;
using AlShamilEntityData.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AlShamil
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                 c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meeting Room Booking API", Version = "v1" });

                     // Configure Swagger to use JWT Bearer authentication
                     var securityScheme = new OpenApiSecurityScheme
                     {
                         Name = "Authorization",
                         BearerFormat = "JWT",
                         Description = "JWT Authorization header using the Bearer scheme",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.Http,
                         Scheme = "bearer",
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     };

                     c.AddSecurityDefinition("Bearer", securityScheme);
                     c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                         { securityScheme, new string[] { } }
                     });
                 }
                 );

            builder.Services.AddDbContext<AlShamilDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("AlShamilDBConnection"))
            );


            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

            //builder.Services.AddScoped<IUserData<UserDto>, UserData<UserDto>>();
            //builder.Services.AddScoped<IUserBusiness<UserDto>, UserBusiness<UserDto>>();
            //builder.Services.AddScoped<ILoginData<LoginDto>, LoginData<LoginDto>>();
            //builder.Services.AddScoped<ILoginBusiness<LoginDto>, LoginBusiness<LoginDto>>();

            //builder.Services.AddScoped<IForgotPasswordData<UserDto>, ForgotPasswordData<UserDto>>();
            //builder.Services.AddScoped<IForgotPasswordBusiness<UserDto>, ForgotPasswordBusiness<UserDto>>();
            //builder.Services.AddScoped<IEmailSenderData<string>, EmailSenderData<string>>();
            //builder.Services.AddScoped<IEmailSenderBusiness<string>, EmailSenderBusiness<string>>();

            builder.Services.AddScoped<IUserData, UserData>();
            builder.Services.AddScoped<IUserBusiness, UserBusiness>();
            builder.Services.AddScoped<IRoleData, RoleData>();
            builder.Services.AddScoped<IRoleBusiness, RoleBusiness>();
            builder.Services.AddScoped<ILoginData, LoginData>();
            builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
            builder.Services.AddScoped<IForgotPasswordData, ForgotPasswordData>();
            builder.Services.AddScoped<IForgotPasswordBusiness, ForgotPasswordBusiness>();
            builder.Services.AddScoped<ISmtpEmailSenderData, SmtpEmailSenderData>();
            builder.Services.AddScoped<ISmtpEmailSenderBusiness, SmtpEmailSenderBusiness>();


            

            builder.Services.Configure<SmtpSettingsData>(builder.Configuration.GetSection("SmtpSettings"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();
            // Add configuration from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
