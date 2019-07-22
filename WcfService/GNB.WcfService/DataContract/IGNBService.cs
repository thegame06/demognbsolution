using GNB.WcfService.DataContract;
using System.ServiceModel;

namespace GNB.WcfService
{
    [ServiceContract]
    public interface IGNBService
    {
        [OperationContract]
        Rates GetRateList();


        [OperationContract]
        Transactions GetTransactionList();
    }
}
