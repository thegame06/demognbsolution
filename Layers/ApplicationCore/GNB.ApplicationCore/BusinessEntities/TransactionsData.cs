using System;
using System.Collections.Generic;
using System.Linq;

namespace GNB.ApplicationCore.BusinessEntities
{
    [Serializable]
    public class TransactionsData : BaseMessage
    {
        public TransactionsData(List<Transaction> pTransactionList)
        {
            TransactionList = pTransactionList;
        }

        public List<Transaction> TransactionList { get; set; }

    }
}
