using System;
using GNB.ApplicationCore.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GNB.UnitTests.BusinessEntities
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void Error()
        {
            var result = new Result<string>(null, null, "90001","Error interno.");

            Assert.IsFalse(result.Successfully);
        }

        [TestMethod]
        public void OK()
        {
            var result = new Result<string>("OK");

            Assert.IsTrue(result.Successfully);
        }
    }
}
