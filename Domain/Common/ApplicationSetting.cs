
namespace Domain.Common
{
    public class ApplicationSetting
    {
        public string MyProperty { get; set; }
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
