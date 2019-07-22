using GNB.ApplicationCore.BusinessEntities;
using GNB.ApplicationCore.Exception;
using GNB.ApplicationCore.Interfaces;
using GNB.Infrastructure.ServiceAgents.Common;
using GNB.Infrastructure.ServiceAgents.Webs.WcfGNB;
using System.ServiceModel;

namespace GNB.Infrastructure.ServiceAgents.Services
{
    [CurrentException(Class = "ServiceAdapterException", NameSpace = "GNB.Infrastructure.ServiceAgents", Assembly = "GNB.Infrastructure.ServiceAgents")]
    public class GNBService : ServiceClientWrapper<IGNBService>
    {
        public GNBService()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }

        public virtual IResult<rates> GetRateList()
        {
            rates response = new rates();

            IResult<string> result = this.Using(u =>
             {
                 using (new OperationContextScope(InnerChannel))
                 {
                     response = Client.GetRateList();
                 }
             });

            if (!result.Successfully)
            {
                return new Result<rates>(null, null, result.StatusCode, result.StatusDescription);
            }


            return new Result<rates>(response);
        }

        public virtual IResult<transactions> GetTransactionList()
        {
            transactions response = new transactions();

            IResult<string> result = this.Using(u =>
            {
                using (new OperationContextScope(InnerChannel))
                {
                    response = Client.GetTransactionList();
                }
            });

            if (!result.Successfully)
            {
                return new Result<transactions>(null, null, result.StatusCode, result.StatusDescription);
            }


            return new Result<transactions>(response);
        }
    }
}
