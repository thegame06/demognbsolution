using System;

namespace GNB.ApplicationCore.Interfaces
{
    public interface IDataSourceBase<T> where T : class
    {
        IResult<string> Get(Action<T> pMessageOut);


        IResult<string> Add(T pMessageIn);

    }
}
