using Microsoft.AspNetCore.Localization;
using System.Text.RegularExpressions;

namespace  ExampleCRUDwhitAjax.Resources
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex PartLocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$", RegexOptions.IgnoreCase);
        private static readonly Regex FullLocalePattern = new Regex(@"^[a-z]{2}-[A-Z]{2}$", RegexOptions.IgnoreCase);

        private static readonly Dictionary<string, string> LanguageMap = new Dictionary<string, string>
        {
            { "en", "en-US" },
            { "ar", "ar-kw" }
        };

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var parts = httpContext.Request.Path.Value.Split('/');
            // Get culture from path
            var culture = parts[1];

            if (parts.Length < 3)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            // For full languages ar-kw or en-US pattern
            if (FullLocalePattern.IsMatch(culture))
            {
                return Task.FromResult(new ProviderCultureResult(culture));
            }

            // For part languages ar or en pattern
            if (PartLocalePattern.IsMatch(culture))
            {
                var fullCulture = LanguageMap[culture];
                return Task.FromResult(new ProviderCultureResult(fullCulture));
            }

            return Task.FromResult<ProviderCultureResult>(null);
        }
    }
}
