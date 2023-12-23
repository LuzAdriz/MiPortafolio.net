using Colegios.Api.Interfaces;
using Colegios.Api.Models;
using Colegios.Api.Services;
using Colegios.Api.Services.Repositorios;
using Microsoft.Extensions.DependencyInjection;


namespace Colegios.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Regitra las inyecciones de dependencias al startup de la aplicacion
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddTransient<IDocente, DocenteService>();

            services.AddTransient<IRepositorio<DocenteModel>, DocenteRepositorio>();


            return services;
        }
    }
}
