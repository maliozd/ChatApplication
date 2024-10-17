using ChatApp.Application.Common.Interfaces;
using ChatApp.Application.Common.Interfaces.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Infrastructure
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"), (options) =>
                {
                    options.EnableRetryOnFailure(5);
                    options.CommandTimeout(30);
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                });
                opt.EnableDetailedErrors(true);
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddHttpContextAccessor();
            return services;

        }


        public static IServiceCollection AddAuthentincationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));
            services.AddScoped<ITokenHandler, JwtTokenHandler>();

            services.AddAuthorizationBuilder()
                .AddPolicy("default", new AuthorizationPolicyBuilder().
                RequireClaim(claimType: ClaimTypes.NameIdentifier).Build());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            services.TryAddEnumerable(
    ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,
        ConfigureJwtBearerOptions>());
            return services;
        }
    }
}

