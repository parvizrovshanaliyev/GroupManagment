using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayBall.GroupManagement.Web.Demo
{
    public class SomeRootConfiguration
    {
        public SomeSubRootConfiguration SomeSubRoot { get; set; }
    }

    public class SomeSubRootConfiguration
    {
        public int SomeKey { get; set; }
        public string AnotherKey { get; set; }
        public int CmdLineKey { get; set; }
    }
}
