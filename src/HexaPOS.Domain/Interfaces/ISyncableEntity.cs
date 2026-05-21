using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Domain.Interfaces
{
    public interface ISyncableEntity
    {
        Guid Id { get; set; }

        DateTimeOffset UpdatedAt { get; set; }

        DateTimeOffset? DeletedAt { get; set; }
    }
}
