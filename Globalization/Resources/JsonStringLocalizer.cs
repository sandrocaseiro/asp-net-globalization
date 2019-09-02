using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Globalization.Resources
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _resource =
            new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        private readonly HashSet<string> _languages;

        public JsonStringLocalizer()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources/Resource.json");
            using (StreamReader reader = File.OpenText(path))
            {
                var jObject = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                _resource = JsonConvert.DeserializeObject<ConcurrentDictionary<string, ConcurrentDictionary<string, string>>>(jObject.ToString());
                _languages = new HashSet<string>(_resource.SelectMany(r => r.Value).Select(r => r.Key).Distinct());
            }
        }

        public LocalizedString this[string name] => GetStringResource(name);

        public LocalizedString this[string name, params object[] arguments] => GetStringResource(name, arguments);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _resource.Keys.Select(r => new LocalizedString(r, r));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }

        public IEnumerable<CultureInfo> GetAllCultures()
        {
            return _languages.Select(c => new CultureInfo(c));
        }

        private LocalizedString GetStringResource(string name, params object[] arguments)
        {
            if (string.IsNullOrWhiteSpace(name)
                || !_resource.TryGetValue(name, out ConcurrentDictionary<string, string> stringByCulture)
                || !stringByCulture.TryGetValue(CultureInfo.CurrentCulture.Name, out string value))
                return new LocalizedString(name, name, true);

            return new LocalizedString(name, string.Format(value, arguments), false);
        }
    }
}
