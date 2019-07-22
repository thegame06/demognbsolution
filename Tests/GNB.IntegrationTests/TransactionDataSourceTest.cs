using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;

namespace GNB.IntegrationTests
{
    [TestClass]
    public class TransactionDataSourceTest
    {
        public TransactionDataSourceTest()
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
        public void Transaction_DataPersistence()
        {
            var dataSource = new GNB.Infrastructure.Data.TransactionDataSource();
            var message = new TransactionsData(new List<Transaction> { new Transaction("TRN0001",10,"ISO") });

            dataSource.Add(message);

            message = null;

            IResult<string> result = dataSource.Get(messageOut => {
                message = messageOut;
            });

            Assert.IsTrue(result.Successfully);
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.TransactionList.Count);
            Assert.AreEqual("TRN0001", message.TransactionList[0].SKU);

        }
    }
}
