using GNB.ApplicationCore.Interfaces;
using GNB.ApplicationCore.BusinessEntities;
using System;
using GNB.Infrastructure.ServiceAgents.Services;
using GNB.Infrastructure.ServiceAgents.Webs.WcfGNB;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace GNB.Infrastructure.ServiceAgents
{
    public class ServiceAdapter : IServiceAgent
    {
        public IResult<string> Execute(Action<BaseMessage> pMessageOut, Methods pMethod)
        {
            IResult<string> result = null;

            switch (pMethod)
            {
                case Methods.ResourceService_GetRates:

                    var rateResult = GetRateList();

                    if (rateResult.Successfully)
                    {
                        pMessageOut(rateResult.Resource);
                        result = new Result<string>("OK");
                    }
                    else
                    {
                        result = new Result<string>(null,null, rateResult.StatusCode, rateResult.StatusDescription);
                    } 

                    break;

                case Methods.ResourceService_GetTransactions:

                    var tranResult = GetTransactionList();

                    if (tranResult.Successfully)
                    {
                        pMessageOut(tranResult.Resource);
                        result = new Result<string>("OK");
                    }
                    else
                    {
                        result = new Result<string>(null, null, tranResult.StatusCode, tranResult.StatusDescription);
                    }

                    break;

                default:

                    result = new Result<string>(null, null, "Method not implement.");

                    break;
            }

            return result;

        }

        public virtual IResult<RatesData> GetRateList()
        {
            IResult<rates> clientResult = null;
            RatesData ratesData = new RatesData(new List<ApplicationCore.BusinessEntities.Rate>(), ConfigurationManager.AppSettings["BaseCurrency"]);

            using (var client = new GNBService())
            {
                clientResult = client.GetRateList();
            }

            if (clientResult.Successfully)
            {
                ratesData.RateList.AddRange((
                    from data in clientResult.Resource.AsEnumerable()
                    select new ApplicationCore.BusinessEntities.Rate(data.@from, data.to, decimal.Parse(data.rateMember))).ToList());
            }
            else
                return new Result<RatesData>(null, null, clientResult.StatusCode, clientResult.StatusDescription);

            return new Result<RatesData>(ratesData);
        }

        public virtual IResult<TransactionsData> GetTransactionList()
        {
            IResult<transactions> clientResult = null;
            TransactionsData transactionsData = new TransactionsData(new List<ApplicationCore.BusinessEntities.Transaction>());

            using (var client = new GNBService())
            {
                clientResult = client.GetTransactionList();
            }

            if (clientResult.Successfully)
            {
                transactionsData.TransactionList.AddRange((
                    from data in clientResult.Resource.AsEnumerable()
                    select new ApplicationCore.BusinessEntities.Transaction(data.sku, decimal.Parse(data.amount), data.currency)).ToList());
            }
            else
                return new Result<TransactionsData>(null, null, clientResult.StatusCode, clientResult.StatusDescription);

            return new Result<TransactionsData>(transactionsData);
        }
    }
}
