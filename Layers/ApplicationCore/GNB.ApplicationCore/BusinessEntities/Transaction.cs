using System;

namespace GNB.ApplicationCore.BusinessEntities
{
    [Serializable]
    public class Transaction
    {
        public Transaction(String pSKU, decimal pAmount, string pCurrency)
        {
            SKU = pSKU;
            Amount = pAmount;
            Currency = pCurrency;
        }


        public string SKU { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal AmountTruncate()
        {
            return Math.Truncate(100 * Amount) / 100; ;
        }
    }
}
