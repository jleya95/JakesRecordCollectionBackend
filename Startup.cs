using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Capstone.DAO;
using Capstone.Security;
using Microsoft.OpenApi.Models;
using Capstone.Services;
using RecordCollection.Services;
using RecordCollection.DAO;

namespace Capstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

/*            string connectionString = Configuration.GetConnectionString("Project");
*/            string connectionString = "Server=tcp:jakesrecordcollection-dbserver.database.windows.net,1433;Initial Catalog=record_collection;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";";


            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSecret"]);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    NameClaimType = "name"
                };
            });

            // Dependency Injection configuration
            services.AddSingleton<ITokenGenerator>(tk => new JwtGenerator(Configuration["JwtSecret"]));
            services.AddSingleton<IPasswordHasher>(ph => new PasswordHasher());
            services.AddTransient<IUserDao>(m => new UserSqlDao(connectionString));
            services.AddTransient<IFileService>(m => new FileService());
            services.AddTransient<IRecordDao>(m => new RecordSqlDao(connectionString));
            services.AddTransient<IRecordService>(m=> new RecordService());
            services.AddTransient<ISingleDao>(m => new SingleSqlDao(connectionString));

            // Swagger set up
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = Configuration["APIVersion"],
                    Title = "Record Collection API",
                    Description = "A digital record database"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "13.1026");
                c.RoutePrefix = string.Empty;
                c.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[] { });
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
