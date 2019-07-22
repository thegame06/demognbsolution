using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Interfaces;
using System;

namespace GNB.ApplicationCore.Exception
{
    public class CustomException<T> : System.Exception where T : class
    {

        public event EventHandler<Result<T>> _eventHandler;

        public IResult<T> Result { get; private set; }

        public CustomException()
        {

        }

        private void Set(object pSender, Result<T> pResult)
        {
            Result = pResult;
        }

        void LoadDelegate(Type pOverrideCustomerExcepcion = null)
        {
            var cDelegate = new CurrentException();

            if (pOverrideCustomerExcepcion != null)
                cDelegate = (CurrentException)Activator.CreateInstance(pOverrideCustomerExcepcion);

            _eventHandler += cDelegate.CrashResult<T>;
        }

        public CustomException(T pResource, int pCode, string pMessage, System.Exception pException = null, Type pOverrideCustomerExcepcion = null)
        {

            this._eventHandler += Set;

            LoadDelegate(pOverrideCustomerExcepcion);

            InExceptionResult(pResource, pCode, pMessage, pException);
        }

        protected virtual void InExceptionResult(T pResource, int pCode, string pMessage, System.Exception pException)
        {
            var del = _eventHandler as EventHandler<Result<T>>;
            if (del != null)
            {
                del(this, new Result<T>(pResource, pException, pCode, pMessage));
            }
        }

    }
}
