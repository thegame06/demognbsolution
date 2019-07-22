using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GNB.ApplicationCore.DTOs
{
    public class GetTransactionListDto
    {
        public GetTransactionListDto()
        {
            TransactionList = new List<TransactionDto>();
        }

        public List<TransactionDto> TransactionList { get; set; }

        [Display(Name = "Monto total")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public Decimal TotalAmount { get; set; }

        public string BaseCurrency { get; set; }
    }
}
