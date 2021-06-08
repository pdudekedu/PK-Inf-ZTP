namespace WorkManager.Infrastructure.Authorization
{
    public class JwtConfig
    {
        public static readonly string SectionKey = "Jwt";
        public static readonly string UserItem = "User";

        public string Secret { get; set; }
    }
}
