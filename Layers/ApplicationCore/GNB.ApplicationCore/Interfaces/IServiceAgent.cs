using GNB.ApplicationCore.BusinessEntities;
using System;

namespace GNB.ApplicationCore.Interfaces
{
    public interface IServiceAgent
    {
        IResult<string> Execute(Action<BaseMessage> pMessageOut, Methods pMethod);
    }
}
