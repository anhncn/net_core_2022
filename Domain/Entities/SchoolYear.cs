using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Domain.Entities
{
    public partial class SchoolYear : Common.AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public int YearStudy { get; set; }
        public string SchoolYearName { get; set; }
    }
}
