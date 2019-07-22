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
    public class RatesDataTest
    {


        [TestMethod]
        public void GenerateRateWithRates()
        {
            List<Rate> rateList = new List<Rate>
            {
                new Rate ( "EUR", "USD",  1.359M ),
                new Rate ( "CAD", "EUR",  0.732M ),
                new Rate ( "USD", "EUR",  0.736M ),
                new Rate ( "EUR", "CAD",  1.366M ),                                                      
            };

            var rates = new RatesData(rateList,"EUR");

            var rate = rates.GetExchangeRate("USD", "CAD");

            Assert.AreEqual(rate, (0.736M / 1.366M));
        }
    }
}
