using EasyWebApp.API.Services.AuthSrv;
using EasyWebApp.API.Services.CustomerDbConnStrManagerSrv;
using EasyWebApp.API.Services.CustomerDbProccessSrv;
using EasyWebApp.API.Services.CustomerDbStatisticsSrv;
using EasyWebApp.API.Services.UserSrv;
using EasyWebApp.API.Settings;
using EasyWebApp.Data.DbContext;
using EasyWebApp.Data.DbContextProvider;
using EasyWebApp.Data.Entities.AuthenticationEnties;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EasyWebAppAPI
{
    public class Startup
    {
        protected readonly string FrontEnd = "_frontEnd";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettingsSection = Configuration.GetSection(nameof(JwtSetting));
            services.Configure<JwtSetting>(jwtSettingsSection);

            services.AddDbContext<CustomerDbContext>(options =>
            {
                options
                .UseSqlServer(
                    Configuration.GetConnectionString("MSSQLDbConnection"),
                                    optionsBuilder => optionsBuilder.MigrationsAssembly("EasyWebApp.Data"));
            });

            services.AddDbContext<EasyWebDbContext>(options =>
            {
                options
                .UseSqlServer(
                    Configuration.GetConnectionString("WebAppDbConnection"),
                                    optionsBuilder => optionsBuilder.MigrationsAssembly("EasyWebApp.Data"));
            });

            //services.AddDbContext<CustomerDbContext>(options =>
            //{
            //    options
            //    .UseMySql(
            //        Configuration.GetConnectionString("MySQLDbConnection"),
            //        new MySqlServerVersion(new Version(8, 0, 21)),
            //                                mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend));
            //});

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyWebAppAPI", Version = "v1" });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            })
               .AddEntityFrameworkStores<EasyWebDbContext>()
               .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var jwtSetting = jwtSettingsSection.Get<JwtSetting>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    SaveSigninToken = false,
                    ValidateActor = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: FrontEnd, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddAuthorization();

            services.AddScoped<IUserSrv, UserSrv>();
            services.AddScoped<IAuthSrv, AuthSrv>();
            services.AddScoped<ICustomerDbConnStrManagerSrv, CustomerDbConnStrManagerSrv>();
            services.AddScoped<IDbContextProvider, DbContextProvider>();
            services.AddScoped<ICustomerDbProccessSrv, CustomerDbProccessSrv>();
            services.AddScoped<ICustomerDbStatisticsSrv, CustomerDbStatisticsSrv>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyWebAppAPI v1"));

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors(FrontEnd);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
