
namespace Domain.Enumerations
{
    public enum RoleCode : int
    {
        Guest = 0,
        Student = 1,
        Teacher = 2,
        HeadTeacher = 4,
        TeacherSubject = 8,
        HeadMaster = 16,
        SupportHeadMaster = 32,
        Administrator = 99,
    }
}
