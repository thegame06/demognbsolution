using GNB.ApplicationCore.BusinessEntities;

namespace GNB.ApplicationCore.Exception
{
    public abstract class  ExceptionDelegateBase
    {
        public abstract void CrashResult<T>(object sender, Result<T> e) where T : class;
    }
}
