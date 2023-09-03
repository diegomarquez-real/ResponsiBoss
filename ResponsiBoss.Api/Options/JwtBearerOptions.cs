namespace ResponsiBoss.Api.Options
{
    public sealed class JwtBearerOptions
    {
        public string Authority { get; set; }
        public string[] ValidAudiences { get; set; }
        public string ValidIssuer { get; set; }
    }
}