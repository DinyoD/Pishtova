namespace Pishtova_ASP.NET_web_api
{
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using Pishtova.Data;
    using Pishtova.Data.Model;
    using Pishtova.Data.Seeding;
    using Pishtova.Services;
    using Pishtova.Services.Data;
	using Pishtova.Services.Messaging;

	public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PishtovaDbContext>(options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, Role>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<PishtovaDbContext>()
                .AddDefaultTokenProviders();

            var applicationSettingsConfiguration = this.configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(applicationSettingsConfiguration);

            var emailConfiguration = this.configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>(); 

            var appSettings = applicationSettingsConfiguration.Get<ApplicationSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);


            services
                .AddAuthentication(auth =>
                    {
                        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                ).AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pishtova_ASP.NET_web_api", Version = "v1" });
            });

            services.AddSingleton(emailConfiguration);
            services.AddScoped<IEmailSender, EmailSender>();

			services.AddTransient<IHellpers, Helpers>();
            services.AddTransient<ITownService, TownService>();
            services.AddTransient<IMunicipalityService, MunicipalityService>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IProblemService, ProblemService>();
            services.AddTransient<IScoreService, ScoreService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<PishtovaDbContext>();
                new PishtovaDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pishtova_ASP.NET_web_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
