using Autofac;
using PlayBall.GroupManagement.Business.Implement.Services;
using PlayBall.GroupManagement.Business.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AutoFacModule : Module

    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGroupService>().As<IGroupService>().SingleInstance();
        }
    }
}
