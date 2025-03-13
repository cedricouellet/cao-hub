using CaoHub.Web.Areas.ReceiptManagement.Services;

namespace CaoHub.Web.Areas.ReceiptManagement.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddReceiptManagementServices(this IServiceCollection services)
        {
            services.AddTransient<StoreCategoryService>();
            services.AddTransient<StoreService>();
            services.AddTransient<TaxService>();
            services.AddTransient<ProductService>();
            services.AddTransient<PersonService>();
            services.AddTransient<ReceiptService>();
            services.AddTransient<ReceiptItemService>();

            return services;
        }

        public static ControllerActionEndpointConventionBuilder MapReceiptManagementRoutes(this IEndpointRouteBuilder builder)
        {
            return builder.MapControllerRoute(
                name: "ReceiptItems",
                pattern: "ReceiptManagement/Receipts/{receiptId}/ReceiptItems/{id?}",
                defaults: new
                {
                    action = "Index",
                    controller = "ReceiptItems",
                    area = "ReceiptManagement"
                });
        }
    }
}
