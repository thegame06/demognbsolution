using System.ComponentModel.DataAnnotations;

namespace GNB.ApplicationCore.DTOs
{
    public class TransactionDto
    {
        public string SKU { get; set; }

        [Display(Name = "Monto")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Amount { get; set; }

        [Display(Name = "Moneda")]
        public string Currency { get; set; }
    }
}
