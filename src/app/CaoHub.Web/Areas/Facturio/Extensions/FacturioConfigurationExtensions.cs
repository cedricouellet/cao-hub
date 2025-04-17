using CaoHub.Web.Areas.Facturio.Services;

namespace CaoHub.Web.Areas.Facturio.Extensions
{
    public static class FacturioConfigurationExtensions
    {
        public static IServiceCollection AddFacturioServices(this IServiceCollection services) 
        {
            services.AddSingleton<CalculationMethodService>();
            
            services.AddScoped<StoreCategoryService>();
            services.AddScoped<StoreService>();
            services.AddScoped<PersonService>();
            services.AddScoped<TaxService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ReceiptService>();

            return services;
        }
    }
}
