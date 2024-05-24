using ChatApp.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Application
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyExtensions).Assembly);
            });
            services.AddScoped<IEventPublisher, EventPublisher>();
            return services;
        }
    }
}
