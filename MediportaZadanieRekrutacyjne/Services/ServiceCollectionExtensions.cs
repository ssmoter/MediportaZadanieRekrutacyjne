using MediportaZadanieRekrutacyjne.Data;
using MediportaZadanieRekrutacyjne.Models;

namespace MediportaZadanieRekrutacyjne.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyService(this IServiceCollection services)
        {
            services.AddScoped<ITagRequest, TagRequest>();
            services.AddScoped<ITagCache, TagCache>();
            services.AddScoped<ITagService, TagService>();

            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, TagSerializerContext.Default);
            });



            return services;
        }
    }
}
