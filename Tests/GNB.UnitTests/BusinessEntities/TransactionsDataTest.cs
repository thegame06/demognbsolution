using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.ApplicationCore.BusinessEntities;

namespace GNB.UnitTests.BusinessEntities
{
    /// <summary>
    /// Summary description for RatesDataTest
    /// </summary>
    [TestClass]
    public class TransactionsDataTest
    {


        [TestMethod]
        public void ValidateTransaction()
        {
            List<Transaction> transList = new List<Transaction>
            {
                new Transaction ( "T2006",  10.00M, "USD"  )                                                    
            };

            var trans = new TransactionsData(transList);

            Assert.AreEqual(1, transList.Count);
        }
    }
}
