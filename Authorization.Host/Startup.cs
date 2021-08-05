using System;
using System.Linq;
using System.Reflection;
using Authorization.Application.Extensions;
using Authorization.Application.Handlers;
using Authorization.Application.Models.Responses;
using Authorization.Application.Validators;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Domain.Services;
using Authorization.Domain.Services.Abstraction;
using Authorization.Host.Middleware;
using Authorization.Host.Policies;
using Authorization.Infrastructure.Logging.ConfigurationClasses;
using Authorization.Infrastructure.Logging.Middleware;
using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using Authorization.Infrastructure.Persistence.Contexts;
using Authorization.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorization.Host
{
    public class Startup
    {
        private const string PersistenceSectionName = "Persistence";
        private const string LoggingSectionName = "Logging";
        private const string TokensSectionName = "Tokens";
        private const string JwtSectionName = "Jwt";

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private IWebHostEnvironment HostingEnvironment { get; }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance)
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>())
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<GetUserByEmailAndPasswordValidator>())
                .ConfigureApiBehaviorOptions(ConfigureFluentValidationResponse);

            RegisterConfiguration(Configuration, services);
            RegisterRepositories(services);
            RegisterServices(services);

            services.AddMediatR(typeof(RegisterUserHandler).Assembly);
            services.AddMediatR(typeof(GetUserHandler).Assembly);
            services.AddDbContext<AuthContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IPersistenceConfiguration>();
                var connectionString = configuration.GetConnectionString();
                options.UseNpgsql(connectionString);
            });
        }

#pragma warning disable CA1822

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

#pragma warning restore CA1822

        private static void RegisterConfiguration(IConfiguration configuration, IServiceCollection services)
        {
            var persistenceConfiguration = new PersistenceConfiguration();
            configuration.GetSection(PersistenceSectionName).Bind(persistenceConfiguration);

            var loggingConfiguration = new LoggingConfiguration();
            configuration.GetSection(LoggingSectionName).Bind(loggingConfiguration);

            var tokensConfiguration = new TokensConfiguration();
            configuration.GetSection(TokensSectionName).Bind(tokensConfiguration);
            configuration.GetSection(TokensSectionName).GetSection(JwtSectionName).Bind(tokensConfiguration.JwtConfiguration);

            services
                .AddSingleton<IPersistenceConfiguration>(persistenceConfiguration)
                .AddSingleton<ILoggingConfiguration>(loggingConfiguration)
                .AddSingleton<ITokensConfiguration>(tokensConfiguration)
                .AddSingleton<IJwtConfiguration>(tokensConfiguration.JwtConfiguration);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services
                .AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IClientService, ClientService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<IHashGenerator, Pbkdf2HashGenerator>()
                .AddScoped<ITimeProvider, TimeProvider>();
        }

        private static void ConfigureFluentValidationResponse(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = c =>
            {
                var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage);

                var response = new ErrorResponse
                {
                    Code = ErrorCode.ValidationFailed.GetDisplayName(),
                    Message = errors.First(),
                };

                return new BadRequestObjectResult(response);
            };
        }
    }
}
