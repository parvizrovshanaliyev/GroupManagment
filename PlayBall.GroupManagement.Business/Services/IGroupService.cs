using System.Collections.Generic;
using PlayBall.GroupManagement.Business.Model;
namespace PlayBall.GroupManagement.Business.Services
{
    public interface IGroupService
    {
        IReadOnlyCollection<Group> GetAll();

        Group GetById(long id);
        Group Update(Group group);
        Group Create(Group group);
    }

    
}
