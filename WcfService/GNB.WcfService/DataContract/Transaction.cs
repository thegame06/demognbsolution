using System.Runtime.Serialization;

namespace GNB.WcfService.DataContract
{
    [DataContract(Name = "transaction")]
    public class Transaction
    {
        [DataMember(Name = "sku", IsRequired = true, Order = 0)]
        public string SKU { get; set; }


        [DataMember(Name = "amount", IsRequired = true, Order = 1)]
        public string Amount { get; set; }


        [DataMember(Name = "currency", IsRequired = true, Order = 2)]
        public string Currency { get; set; }
    }
}