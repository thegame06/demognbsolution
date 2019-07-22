using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;
using GNB.ApplicationCore.Exception;
using GNB.Infrastructure.Data.Model;
using System;
using System.Linq;

namespace GNB.Infrastructure.Data
{
    public class TransactionDataSource : ITransactionDataSource
    {
        public IResult<string> Add(TransactionsData pMessageIn)
        {
            return this.UsingCatch(c =>
            {
                using (var context = new GNBDataDataContext())
                {

                    context.InsTransactions(Common.Serializer.SerializeBase64(pMessageIn));

                }

            });
        }


        public IResult<string> Get(Action<TransactionsData> pMessageOut)
        {
            return this.UsingCatch(c =>
             {
                 using (var context = new GNBDataDataContext())
                 {
                     var resultRates = context.GetTransactions().FirstOrDefault();

                     if (resultRates != null)
                     {
                         if (!string.IsNullOrEmpty(resultRates.Transactions))
                         {
                             pMessageOut((TransactionsData)Common.Serializer.DeserializeBase64(resultRates.Transactions));
                         }
                     }
                 }

             });
        }
    }
}
