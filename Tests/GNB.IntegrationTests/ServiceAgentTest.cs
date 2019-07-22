using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;

namespace GNB.IntegrationTests
{
    [TestClass]
    public class ServiceAgentTest
    {
        public ServiceAgentTest()
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
        public void GetRates_ServiceAdapter()
        {
            var dataSource = new GNB.Infrastructure.ServiceAgents.ServiceAdapter();
            RatesData message = null;

            IResult<string> result =  dataSource.Execute(messageOut=> { message = (RatesData)messageOut;  }, Methods.ResourceService_GetRates);


            Assert.IsTrue(result.Successfully);
            Assert.IsNotNull(message);
            Assert.IsTrue(message.RateList.Count>0?true:false);

        }

        [TestMethod]
        public void GetTransactions_ServiceAdapter()
        {
            var service = new GNB.Infrastructure.ServiceAgents.ServiceAdapter();
            TransactionsData message = null;

            IResult<string> result = service.Execute(messageOut => { message = (TransactionsData)messageOut; }, Methods.ResourceService_GetTransactions);


            Assert.IsTrue(result.Successfully);
            Assert.IsNotNull(message);
            Assert.IsTrue(message.TransactionList.Count > 0 ? true : false);

        }
    }
}
