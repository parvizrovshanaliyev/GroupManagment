using System;
using System.Collections.Generic;
using Autofac;
using PlayBall.GroupManagement.Business.Implement.Services;
using PlayBall.GroupManagement.Business.Model;
using PlayBall.GroupManagement.Business.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AutoFacModule : Module

    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGroupService>()
                .Named<IGroupService>("groupService")
                .SingleInstance();
            builder.RegisterDecorator<IGroupService>((context,service)=>
                new GroupServiceDecorator(service),"groupService" );
        }


        private class GroupServiceDecorator : IGroupService
        {
            private readonly IGroupService _inner;

            public GroupServiceDecorator(IGroupService inner)
            {
                _inner = inner;
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                Console.WriteLine($"######## Hello from {nameof(GetAll)} ######");
                return _inner.GetAll();
            }

            public Group GetById(long id)
            {
                Console.WriteLine($"######## Hello from {nameof(GetById)} ######");

                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                Console.WriteLine($"######## Hello from {nameof(Update)} ######");

                return _inner.Update(group);
            }

            public Group Add(Group group)
            {
                Console.WriteLine($"######## Hello from {nameof(Add)} ######");

                return _inner.Add(group);
            }
        }
    }
}
