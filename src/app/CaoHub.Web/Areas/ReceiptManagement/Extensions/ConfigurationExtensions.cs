using CaoHub.Web.Areas.ReceiptManagement.Services;

namespace CaoHub.Web.Areas.ReceiptManagement.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddReceiptManagementServices(this IServiceCollection services)
        {
            services.AddScoped<StoreCategoryService>();
            services.AddScoped<StoreService>();
            services.AddScoped<TaxService>();
            services.AddScoped<ProductService>();
            services.AddScoped<PersonService>();
            services.AddScoped<ReceiptService>();
            services.AddScoped<ReceiptItemService>();

            return services;
        }
    }
}
