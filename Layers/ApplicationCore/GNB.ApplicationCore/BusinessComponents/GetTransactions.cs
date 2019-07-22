using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.DTOs;
using GNB.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GNB.ApplicationCore.BusinessComponents
{
    public class GetTransactions : IGetTransactions
    {
        IRateDataSource _rateData;
        ITransactionDataSource _transactionData;
        IServiceAgent _serviceAgent;

        public GetTransactions(IRateDataSource pRateData,
            ITransactionDataSource pTransactionData,
            IServiceAgent pServiceAgent)
        {
            _rateData = pRateData;
            _transactionData = pTransactionData;
            _serviceAgent = pServiceAgent;
        }

        #region Private Methods

        private IResult<TransactionsData> GetTransactionsData()
        {
            IResult<string> result = null;
            TransactionsData transactions = new TransactionsData(new List<Transaction>());

            //1. Obtenemos los datos con el servicio wcf
            result = _serviceAgent.Execute(messageout =>
            {

                //1.1 Plan B
                if (messageout != null)
                {
                    var messageOut = (TransactionsData)messageout;

                    if (messageOut.TransactionList.Count > 0)
                    {
                        _transactionData.Add(messageOut);
                    }

                    transactions = messageOut;
                }

            }, Methods.ResourceService_GetTransactions);



            //2. Verificamos si es necesario iniciar el Plan B
            if (!result.Successfully && transactions.TransactionList.Count == 0)
            {
                result = _transactionData.Get(messageout =>
                {

                    transactions = messageout;

                });
            }

            if (result.Successfully)
                return new Result<TransactionsData>(transactions);
            else
                return new Result<TransactionsData>(transactions, null, result.StatusCode, result.StatusDescription);
        }

        private IResult<RatesData> GetRatesData()
        {
            IResult<string> result = null;
            RatesData rates = new RatesData(new List<Rate>(), string.Empty);

            //1. Obtenemos las tasas con el servicio wcf
            result = _serviceAgent.Execute(messageout =>
            {

                //1.1 Plan B
                if (messageout != null)
                {
                    var messageOut = (RatesData)messageout;

                    if (messageOut.RateList.Count > 0)
                    {
                        _rateData.Add(messageOut);
                    }

                    rates = messageOut;
                }

            }, Methods.ResourceService_GetRates);

            //2. Verificamos si es necesario iniciar el Plan B
            if (!result.Successfully && rates.RateList.Count == 0)
            {
                result = _rateData.Get(messageout =>
                {

                    rates = messageout;

                });
            }

            if (result.Successfully)
                return new Result<RatesData>(rates);
            else
                return new Result<RatesData>(rates, null, result.StatusCode, result.StatusDescription);
        }

        #endregion Private Methods


        private IResult<GetTransactionListDto> GetTransactionsDto()
        {
            IResult<GetTransactionListDto> result = new Result<GetTransactionListDto>(null, null, "10001", "Search data error.");

            RatesData rates = GetRatesData().Resource;
            TransactionsData transactions = GetTransactionsData().Resource;
            TransactionList transactionList = new TransactionList(transactions, rates);
            GetTransactionListDto OutDto = new GetTransactionListDto() { TransactionList = new List<TransactionDto>() };

            transactions = transactionList.GetTransactionsInBaseCurrency();


            if (transactions != null && transactions.TransactionList.Any())
            {
                transactions.TransactionList.ForEach(t =>
                {
                    OutDto.TransactionList.Add(new TransactionDto { Amount = t.Amount, Currency = t.Currency, SKU = t.SKU });
                });

                OutDto.BaseCurrency = rates.BaseCurrency;

                result = new Result<GetTransactionListDto>(OutDto);
            }

            return result;
        }

        public IResult<string> Execute(Action<GetTransactionListDto> pMessageOut)
        {
            var result = GetTransactionsDto();

            pMessageOut(result.Resource);

            if (result.Successfully)
            {
                return new Result<string>("OK");
            }
            else
                return new Result<string>(null, null, result.StatusCode, result.StatusDescription);
        }
    }
}
