using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Globalization.Resources
{
    public class RouteRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string pattern = @"^\/([a-z]{2}-[A-Z]{2})";
            Match match = Regex.Match(httpContext.Request.Path, pattern);
            if (!match.Success || match.Groups.Count < 2)
                return NullProviderCultureResult;

            string culture = match.Groups[1].Value;

            return Task.FromResult(new ProviderCultureResult(new StringSegment(culture)));
        }
    }
}
