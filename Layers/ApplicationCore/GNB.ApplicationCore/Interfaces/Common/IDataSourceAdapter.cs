using System;

namespace GNB.ApplicationCore.Interfaces
{
    public interface IDataSourceAdapter<T> where T : class
    {
        IResult<string> Get(Action<T> pMessageOut);
    }
}
