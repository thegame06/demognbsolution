using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GNB.WcfService.DataContract
{
    [CollectionDataContract(Name = "rates")]
    public class Rates : List<Rate>
    {

    }
}