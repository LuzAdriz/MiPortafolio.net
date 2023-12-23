using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Colegios.Api.Utilities
{
    public class BaseApiController : Controller
    {
        protected string GetCnx(IConfiguration configuration)
        {
            string initialCnx = configuration["InitialConnectionStrings"];
            return configuration.GetConnectionString(initialCnx);
        }
    }
}
