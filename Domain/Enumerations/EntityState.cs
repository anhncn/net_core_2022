using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumerations
{
    public enum EntityState : int
    {
        None = 1,
        Created = 2,
        Updated = 4,
        Deleted = 8,
    }
}
