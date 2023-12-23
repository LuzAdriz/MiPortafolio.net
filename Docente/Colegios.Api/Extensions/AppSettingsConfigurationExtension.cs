using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Colegios.Api.Extensions
{
    public static class AppSettingsConfigurationExtension
    {
        /// <summary>
        /// Agrega el archivo appsetting al startup de la aplicacion
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder AddAppSettings(this IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((builderContex, config) =>
            {
                var env = builderContex.HostingEnvironment;

                if (env.IsDevelopment())
                {
                    var folder = Path.Combine(env.ContentRootPath, "appsettings.json");
                    config.AddJsonFile(folder);
                }
                else 
                {
                    config.AddJsonFile("appsettings.json");
                }
            });

            return builder;
        }
    }
}
