using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Domain.Entities
{
    public partial class SubjectClassSchool : Common.AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid? SubjectGradeSchoolId { get; set; }
        public Guid? ClassRoomId { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
    }
}
