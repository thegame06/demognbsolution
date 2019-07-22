using System.Collections.Generic;
using System.Linq;

namespace GNB.ApplicationCore.BusinessEntities
{
    public class TransactionList
    {
        public string BaseCurrency { get; set; }


        private TransactionsData _transactions;
        private RatesData _rates;
        private TransactionsData _transactionsNew;

        public TransactionList(TransactionsData pTransactions, RatesData pRates)
        {
            _transactions = pTransactions;
            _rates = pRates;
            _transactionsNew = new TransactionsData(new List<Transaction>());
        }

        public TransactionsData GetTransactionsInBaseCurrency()
        {
            if (_transactions != null && _transactions.TransactionList.Any())
            {
                _transactions.TransactionList.ForEach(t =>
                {

                    Transaction trans = null;


                    if (_rates != null && _rates.RateList.Any())
                    {
                        if (t.Currency != _rates.BaseCurrency)
                        {
                            decimal rateValue = _rates.GetExchangeRate(t.Currency, _rates.BaseCurrency);

                            t.Amount *= rateValue;
                            t.Currency = _rates.BaseCurrency;

                            trans = t;
                        }
                        else
                            trans = t;
                    }
                    else
                        trans = t;


                    if (trans != null)
                        _transactionsNew.TransactionList.Add(trans);

                });

                BaseCurrency = _rates.BaseCurrency;

            }

            return _transactionsNew;
        }
    }
}
