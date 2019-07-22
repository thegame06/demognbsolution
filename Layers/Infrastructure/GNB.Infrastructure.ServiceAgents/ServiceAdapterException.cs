using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Exception;
using GNB.Infrastructure.Common;

namespace GNB.Infrastructure.ServiceAgents
{
    public class ServiceAdapterException : CurrentException
    {
        public override void CrashResult<T>(object sender, Result<T> e)
        {
            Capacity.LogWriterFacades.ExceptionError(e.Exception);
            Capacity.LogWriterFacades.LogError(e.StatusCode, e.StatusDescription);
        }
    }
}
