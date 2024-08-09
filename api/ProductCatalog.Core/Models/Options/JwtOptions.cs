namespace ProductCatalog.Core.Models.Options
{
    public class JwtOptions
    {
        public static readonly string JwtSettings = "JwtSettings";

        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecretKey { get; set; }
    }
}
