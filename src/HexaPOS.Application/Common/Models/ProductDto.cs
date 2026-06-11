using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Models
{
    public class ProductDto : BaseSyncModel
    {
        public string Name { get; set; } = default!;
        public string Ubication { get; set; } = default!;
        public string? ImageFileName { get; set; } = default!;
        public string Sku { get; set; } = default!;
        public string Barcode { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int StockLimit { get; set; }
        public Guid CategoryId { get; set; }
    }
}
