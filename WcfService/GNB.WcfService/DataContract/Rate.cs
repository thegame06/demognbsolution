using System.Runtime.Serialization;

namespace GNB.WcfService.DataContract
{
    [DataContract(Name = "rate")]
    public class Rate
    {
        [DataMember(Name = "from", IsRequired = true, Order = 0)]
        public string From { get; set; }


        [DataMember(Name = "to", IsRequired = true, Order = 1)]
        public string To { get; set; }


        [DataMember(Name = "rate", IsRequired = true, Order = 2)]
        public string _rate { get; set; }
    }
}