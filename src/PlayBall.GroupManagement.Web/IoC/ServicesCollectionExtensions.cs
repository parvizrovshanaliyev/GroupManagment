using PlayBall.GroupManagement.Business.Implement.Services;
using PlayBall.GroupManagement.Business.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IGroupService, InMemoryGroupService>();
            // more business services ... 
            return services;
        }
    }
}
