using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.CityRepository.cs;
using ServiceLayer.AuthServices;
using ServiceLayer.CategoryService;
using ServiceLayer.CityService;
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
            serviceCollection.AddScoped<ICitiesServices, CitiesServices>();
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
        }
    }
}
