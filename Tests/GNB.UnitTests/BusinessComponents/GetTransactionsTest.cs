using System;
using GNB.ApplicationCore.BusinessComponents;
using GNB.ApplicationCore.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GNB.UnitTests.BusinessComponents
{
    [TestClass]
    public class GetTransactionsTest
    {
        [TestMethod]
        public void ExecuteMethod_Test()
        {
            var dto = new GetTransactionListDto();

            var serviceAgent = new GNB.Infrastructure.ServiceAgents.ServiceAdapter();
            var rateDataSource = new GNB.Infrastructure.Data.RateDataSource();
            var transDataSource = new GNB.Infrastructure.Data.TransactionDataSource();

            var component = new GetTransactions(rateDataSource, transDataSource, serviceAgent);

            var result = component.Execute(messageOut => {

                dto = messageOut;

            });


            Assert.IsTrue(result.Successfully);
            Assert.IsNotNull(dto);
            Assert.IsTrue(dto.TransactionList.Count > 0 ? true : false);

        }
    }
}
