using System;
using System.Collections.Generic;
using System.Linq;

namespace GNB.ApplicationCore.BusinessEntities
{
    [Serializable]
    public class RatesData : BaseMessage
    {
        public RatesData(List<Rate> pRateList, string pBaseCurrency)
        {
            RateList = pRateList;
            BaseCurrency = pBaseCurrency;
        }


        public List<Rate> RateList { get; set; }

        public string BaseCurrency { get; set; }

        public decimal GetExchangeRate(string pFromCurrency, string pToCurrency)
        {
            decimal rate = 0;

            if (RateList.Exists(e => e.From == pFromCurrency && e.To == pToCurrency))
            {
                rate = RateList.Single(e => e.From == pFromCurrency && e.To == pToCurrency).RateValue;
            }
            else
            {
                var rate1 = RateList.Single(e => e.From == pFromCurrency && e.To == BaseCurrency).RateValue;
                var rate2 = RateList.Single(e => e.From == BaseCurrency && e.To == pToCurrency).RateValue;

                var rateNew = rate1 / rate2;

                RateList.Add(new Rate(pFromCurrency,pToCurrency, rateNew));

                rate = GetExchangeRate(pFromCurrency, pToCurrency);
            }


            return rate;
        }
    }
}
