using GNB.ApplicationCore.BusinessEntities;

namespace GNB.ApplicationCore.Exception
{
    public partial class CurrentException : ExceptionDelegateBase
    {
        public override void CrashResult<T>(object sender, Result<T> e)
        {
            
        }
    }
}
