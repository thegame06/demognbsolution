using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;

namespace GNB.IntegrationTests
{
    [TestClass]
    public class RateDataSourceTest
    {
        public RateDataSourceTest()
        {
        }

        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        #region Additional test attributes

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestInitialize]
        public void SetupTest()
        {
            Console.WriteLine(
                "TestContext.TestName='{0}'  static _testContext.TestName='{1}'",
                TestContext.TestName,
                _testContext.TestName);
        }


        #endregion

        [TestMethod]
        public void Rate_DataPersistence()
        {
            var dataSource = new GNB.Infrastructure.Data.RateDataSource();
            var message = new RatesData(new List<Rate> { new Rate("EUR","USD",1.12M) },"EUR");

            dataSource.Add(message);

            message = null;

            IResult<string> result = dataSource.Get(messageOut => {
                message = messageOut;
            });

            Assert.IsTrue(result.Successfully);
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.RateList.Count);
            Assert.AreEqual(16.80M, message.RateList[0].RateValue * 15);

        }
    }
}
