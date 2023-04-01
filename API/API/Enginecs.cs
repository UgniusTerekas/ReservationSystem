using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using DataLayer.Repositories.Category;
using ServiceLayer.AuthServices;
using ServiceLayer.CategoryService;
using ServiceLayer.Interfaces;

namespace API
{
    public static class Enginecs
    {
        public static void SetupDependancies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthServices, AuthServices>();
            serviceCollection.AddScoped<IAuthRepository, AuthRepository>();
            serviceCollection.AddScoped<ICategoriesServices, CategoriesServices>();
            serviceCollection.AddScoped<ICategoriesRepository, CategoriesRepository>();
        }
    }
}
