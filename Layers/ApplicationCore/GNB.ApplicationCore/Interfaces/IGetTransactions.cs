using GNB.ApplicationCore.DTOs;
using System;

namespace GNB.ApplicationCore.Interfaces
{
    public interface IGetTransactions
    {
        IResult<string> Execute(Action<GetTransactionListDto> pMessageOut);
    }
}
