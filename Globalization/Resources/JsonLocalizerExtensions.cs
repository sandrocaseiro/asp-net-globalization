using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Globalization.Resources
{
    public static class JsonLocalizerExtensions
    {
        public static void AddJsonLocalization(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
        }
    }
}
