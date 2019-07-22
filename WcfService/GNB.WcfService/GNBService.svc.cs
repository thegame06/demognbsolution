using System;
using GNB.WcfService.DataContract;

namespace GNB.WcfService
{
    public class GNBService : IGNBService
    {
        public Rates GetRateList()
        {
            Rates response = new Rates();

            response.Add(new Rate { From = "EUR", To = "USD", _rate = "1.359" });
            response.Add(new Rate { From = "CAD", To = "EUR", _rate = "0.732" });
            response.Add(new Rate { From = "USD", To = "EUR", _rate = "0.736" });
            response.Add(new Rate { From = "EUR", To = "CAD", _rate = "1.366" });

            return response;
        }

        public Transactions GetTransactionList()
        {
            Transactions response = new Transactions();

            response.Add(new Transaction { SKU = "T2006", Amount = "10.00", Currency = "USD" });
            response.Add(new Transaction { SKU = "M2007", Amount = "34.57", Currency = "CAD" });
            response.Add(new Transaction { SKU = "R2008", Amount = "17.95", Currency = "USD" });
            response.Add(new Transaction { SKU = "T2006", Amount = "7.63", Currency = "EUR" });
            response.Add(new Transaction { SKU = "B2009", Amount = "21.23", Currency = "USD" });

            return response;
        }
    }
}
