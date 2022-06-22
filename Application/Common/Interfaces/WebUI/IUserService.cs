namespace Application.Common.Interfaces
{
    /// <summary>
    /// Lấy thông tin cơ bản của user trong request
    /// </summary>
    public interface IUserService
    {
        string UserId { get; }

        string UserName { get; }
    }
}
