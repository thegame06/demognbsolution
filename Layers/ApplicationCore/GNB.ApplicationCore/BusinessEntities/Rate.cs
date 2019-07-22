using System;

namespace GNB.ApplicationCore.BusinessEntities
{
    [Serializable]
    public partial class Rate
    {
        public Rate(string pFrom, string pTo, decimal pRateValue)
        {
            From = pFrom;
            To = pTo;
            RateValue = pRateValue;
        }


        public string From { get; set; }

        public string To { get; set; }

        public decimal RateValue { get; set; }

        public decimal RateRound()
        {
            return Math.Round(RateValue, 2, MidpointRounding.AwayFromZero);
        }
    }
}
