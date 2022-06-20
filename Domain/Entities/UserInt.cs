using Domain.Common;
using System;

namespace Domain.Entities
{
    [Dapper.Contrib.Extensions.Table("dbo.[User]")]
    public class User : AuditableEntity
    {
        [Dapper.Contrib.Extensions.Key]
        [System.ComponentModel.DataAnnotations.Key]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }
    }

    [Dapper.Contrib.Extensions.Table("dbo.[UserInt]")]
    public class UserInt : AuditableEntity
    {
        [Dapper.Contrib.Extensions.Key]
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string UserName { get; set; }
    }
}
