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
            services.AddAuthorizationBuilder()
                .AddPolicy("default", new AuthorizationPolicyBuilder().
                RequireClaim(claimType: ClaimTypes.NameIdentifier).Build());

            //services.AddAuthorization(configure =>
            //{
            //    configure.AddPolicy("default", new AuthorizationPolicyBuilder().RequireClaim(claimType: ClaimTypes.NameIdentifier).Build());
            //});


            services.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {

                     // Configure the Authority to the expected value for your authentication provider
                     // This ensures the token is appropriately validated
                     //options.Authority = /* TODO: Insert Authority URL here */;

                     // We have to hook the OnMessageReceived event in order to
                     // allow the JWT authentication handler to read the access
                     // token from the query string when a WebSocket or 
                     // Server-Sent Events request comes in.

                     // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
                     // due to a limitation in Browser APIs. We restrict it to only calls to the
                     // SignalR hub in this code.
                     // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                     // for more information about security considerations when using
                     // the query string to transmit the access token.
                     //options.Events = new JwtBearerEvents
                     //{
                     //    OnMessageReceived = context =>
                     //    {
                     //        var accessToken = context.Request.Query["access_token"];

                     //        // If the request is for our hub...
                     //        var path = context.HttpContext.Request.Path;
                     //        if (!string.IsNullOrEmpty(accessToken) &&
                     //            (path.StartsWithSegments("/hubs/chat")))
                     //        {
                     //            // Read the token out of the query string
                     //            context.Token = accessToken;
                     //        }
                     //        return Task.CompletedTask;
                     //    }
                     //};
                 });
            services.ConfigureOptions<TokenValidationConfiguration>();

            services.AddScoped<ITokenHandler, JwtTokenHandler>();

            services.TryAddEnumerable(
    ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,
        ConfigureJwtBearerOptions>());
            return services;
        }
    }
}

