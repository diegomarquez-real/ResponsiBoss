namespace ResponsiBoss.Api.Options
{
    public sealed class JwtBearerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessExpirationMinutes { get; set; }
    }
}