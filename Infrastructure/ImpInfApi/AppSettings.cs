namespace ImpInfApi
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; }
        public string StaticToken { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public uint Lifetime { get; set; }
    }
}
