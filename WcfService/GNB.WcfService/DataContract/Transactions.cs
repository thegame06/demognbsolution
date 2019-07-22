using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GNB.WcfService.DataContract
{
    [CollectionDataContract(Name = "transactions")]
    public class Transactions : List<Transaction>
    {

    }
}