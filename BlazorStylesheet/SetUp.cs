using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

using StylesheetNET;

using System.Reflection;

namespace BlazorStylesheet
{
    public static class SetUp
    {
        /// <summary>
        /// Registers the Stylesheet service within the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddStylesheet(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddScoped<Stylesheet>();
        }
    }
}
