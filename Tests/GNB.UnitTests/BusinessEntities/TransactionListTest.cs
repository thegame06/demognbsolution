using System;
using System.Collections.Generic;
using GNB.ApplicationCore.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GNB.UnitTests.BusinessEntities
{
    [TestClass]
    public class TransactionListTest
    {
        [TestMethod]
        public void GetTransactionsInBaseCurrency()
        {
            List<Rate> rateList = new List<Rate>
            {
                new Rate ( "EUR", "USD",  1.359M ),
                new Rate ( "CAD", "EUR",  0.732M ),
                new Rate ( "USD", "EUR",  0.736M ),
                new Rate ( "EUR", "CAD",  1.366M ),
            };

            List<Transaction> transList = new List<Transaction>
            {
                new Transaction ( "T2006",  10.00M, "USD"  )
            };

            var rates = new RatesData(rateList, "EUR");
            var trans = new TransactionsData(transList);
            var transactionList = new TransactionList(trans, rates);

            trans = transactionList.GetTransactionsInBaseCurrency();


            Assert.IsNotNull(trans.TransactionList);
            Assert.AreEqual(trans.TransactionList.Count, 1);
            Assert.AreEqual(trans.TransactionList[0].Currency, "EUR");
            Assert.AreEqual(trans.TransactionList[0].Amount, 7.36M);
        }
    }
}
