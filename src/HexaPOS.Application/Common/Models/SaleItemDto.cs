using HexaPOS.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Models
{
    public class SaleItemDto : BaseSyncModel
    {
        public string SkuSnapshot { get; set; }
        public string ProductNameSnapshot { get; set; }
        public string AttributeSnapshot { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
    }
}
