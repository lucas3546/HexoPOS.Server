using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Models
{
    public class CategoryDto : BaseSyncModel
    {
        public string Name { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

    }
}
