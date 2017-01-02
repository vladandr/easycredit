using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyCredit.Models;

namespace EasyCredit.Tests.Models
{
    [TestClass]
    public class ChildernTest
    {
        [TestMethod]
        public void ChildrenCanHaveName()
        {
            Children child = new Children();
            child.Name = "Dick";
            Assert.IsNotNull(child.Name);
        }
    }
}
