using GNB.ApplicationCore.Interfaces;

namespace GNB.ApplicationCore.BusinessEntities
{
    public class Result<T> : Result, IResult<T> where T : class
    {
        public T Resource { get; private set; }

        public Result(T resource)
            : this(resource, null, null)
        {

        }

        public Result(object resource, System.Exception exception, params object[] args)
            : base(exception, args)
        {

            if (resource != null)
            {
                Resource = (T)resource;
            }
        }
    }
}
