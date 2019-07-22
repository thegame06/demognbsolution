using System;
using System.ServiceModel;

namespace GNB.Infrastructure.ServiceAgents.Common
{
    public class ServiceClientWrapper<TChannel> : ClientBase<TChannel>, IDisposable
                         where TChannel : class
    {
        public ServiceClientWrapper()
        {

        }

        public ServiceClientWrapper(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        public TChannel Client
        {
            get { return Channel; }
        }

        public new void Close()
        {
            ((IDisposable)this).Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (State != CommunicationState.Closed)
                    base.Close();
            }
            catch (System.Exception ex)
            {
                base.Abort();
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
