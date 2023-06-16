using AccountService.Services;

namespace AccountService.Infrastructure.Extentions.ServiceCollection
{
    public static class ServiceCollectionExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddHostedService<MessageReceiver>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
