using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayBall.GroupManagement.Web.Demo
{
    public interface IGroupGenerator
    {
        long Next();
    }

    public class GroupGenerator : IGroupGenerator
    {
        private long _currentId = 1;
        public long Next()
        {
            throw new NotImplementedException();
        }
    }
}
