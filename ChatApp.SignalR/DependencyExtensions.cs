using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces;
using ChatApp.SignalR.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.SignalR
{
    public static class DependencyExtensions
    {
        public static void RegisterSignalR(this IServiceCollection services)
        {
            services.AddScoped<IMessageHubService, MessageHubService>();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            services.AddSingleton<IConnectionCache, ConnectionCache>();
        }

    }
}
