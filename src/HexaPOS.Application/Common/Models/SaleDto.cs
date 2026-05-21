using HexaPOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Models
{
    public class SaleDto : BaseSyncModel
    {
        public long SaleNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
