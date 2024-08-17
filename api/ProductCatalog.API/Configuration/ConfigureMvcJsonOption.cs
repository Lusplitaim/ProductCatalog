using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductCatalog.API.Formatters;

namespace ProductCatalog.API.Configuration
{
    internal class ConfigureMvcJsonOption : IConfigureOptions<MvcOptions>
    {
        private readonly IOptionsMonitor<JsonOptions> m_JsonOptions;
        public ConfigureMvcJsonOption(IOptionsMonitor<JsonOptions> jsonOptions)
        {
            m_JsonOptions = jsonOptions;
        }

        public void Configure(MvcOptions options)
        {
            var jsonOptions = m_JsonOptions.Get(string.Empty);

            options.OutputFormatters.Insert(0, new AllowedPropertiesJsonOutputFormatter(jsonOptions.JsonSerializerOptions));
        }
    }
}
