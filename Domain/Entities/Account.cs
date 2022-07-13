using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
