using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayBall.GroupManagement.Business.Model;
using PlayBall.GroupManagement.Business.Services;

namespace PlayBall.GroupManagement.Business.Implement.Services
{
    public class InMemoryGroupService : IGroupService
    {
        #region fields
        private readonly List<Group> _groups =
            new List<Group>();

        private long _currentId = 0;
        #endregion

        public IReadOnlyCollection<Group> GetAll()
        {
            return _groups.AsReadOnly();
        }

        public Group GetById(long id)
        {
            return _groups.SingleOrDefault(x => x.Id == id);
        }

        public Group Update(Group group)
        {
            var toUpdate = _groups.SingleOrDefault(x => x.Id == group.Id);
            if (toUpdate is null) return null;

            toUpdate.Name = group.Name;
            return toUpdate;
        }

        public Group Add(Group group)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return group;
        }
    }
}
