using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.CityRepository.cs;
using DataLayer.Repositories.Entertainment;
using ServiceLayer.AuthServices;
using ServiceLayer.CategoryService;
using ServiceLayer.CityService;
using ServiceLayer.EntertainmentService;
using ServiceLayer.GalleryServices;
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
            serviceCollection.AddScoped<IEntertainmentServices, EntertainmentServices>();
            serviceCollection.AddScoped<IEntertainmentRepository, EntertainmentRepository>();
        }
    }
}
