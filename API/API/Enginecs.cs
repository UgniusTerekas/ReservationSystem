using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using ServiceLayer.AuthServices;
using ServiceLayer.Interfaces;

namespace API
{
    public static class Enginecs
    {
        public static void SetupDependancies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthServices, AuthServices>();
            serviceCollection.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
