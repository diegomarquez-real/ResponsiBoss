namespace ResponsiBoss.BlazorServerApp.Identity
{
    public class CustomStorage
    {
        public Dictionary<Guid, string> UserSession { get; } = new Dictionary<Guid, string>();
    }
}