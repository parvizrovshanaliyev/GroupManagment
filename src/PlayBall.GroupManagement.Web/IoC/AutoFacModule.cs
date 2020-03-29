using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using PlayBall.GroupManagement.Business.Implement.Services;
using PlayBall.GroupManagement.Business.Model;
using PlayBall.GroupManagement.Business.Services;

namespace PlayBall.GroupManagement.Web.IoC
{
    public class AutoFacModule : Module

    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGroupService>().Named<IGroupService>("groupService").SingleInstance();
            builder.RegisterDecorator<IGroupService>(
                (context, service) =>
                    new GroupServiceDecorator(service, context.Resolve<ILogger<GroupServiceDecorator>>()),
                "groupService");
        }


        private class GroupServiceDecorator : IGroupService
        {
            private readonly IGroupService _inner;
            private ILogger<GroupServiceDecorator> _logger;

            public GroupServiceDecorator(IGroupService inner,
                ILogger<GroupServiceDecorator> logger)
            {
                _inner = inner;
                _logger = logger;
            }

            public IReadOnlyCollection<Group> GetAll()
            {
                using (var scope =
                    _logger.BeginScope("Decorator scope: {decorator}",
                        nameof(GroupServiceDecorator)))
                {
                    _logger.LogTrace("######## Hello from {decoratedMethod} ######",
                        nameof(GetAll));

                    var result =
                        _inner.GetAll();

                    _logger.LogTrace("######## Goodbye from {decoratedMethod} ######",
                        nameof(GetAll));

                    return result;
                }
            }

            public Group GetById(long id)
            {
                _logger.LogTrace("######## Hello from {decoratedMethod} ######", nameof(GetById));

                return _inner.GetById(id);
            }

            public Group Update(Group group)
            {
                _logger.LogTrace("######## Hello from {decoratedMethod} ######", nameof(Update));

                return _inner.Update(group);
            }

            public Group Add(Group group)
            {
                _logger.LogTrace("######## Hello from {decoratedMethod} ######", nameof(Add));

                return _inner.Add(group);  
            }
        }
    }
}
