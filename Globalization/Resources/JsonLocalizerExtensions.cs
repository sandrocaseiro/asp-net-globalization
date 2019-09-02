using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Reflection;

namespace Globalization.Resources
{
    public static class JsonLocalizerExtensions
    {
        public static void AddJsonLocalization(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
        }

        public static void UseStaticJsonLocalization(this IApplicationBuilder app)
        {
            var stringLocalizer = app.ApplicationServices.GetService<IStringLocalizer>();
            MemberInfo[] membersInfo = typeof(Resource).GetMember("stringLocalizer", MemberTypes.Field, 
                BindingFlags.NonPublic | BindingFlags.Static);
            if (!membersInfo.Any())
                return;

            ((FieldInfo)membersInfo.First()).SetValue(null, stringLocalizer);
        }
    }
}
