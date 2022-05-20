
namespace API.Contracts
{
    public static class V1
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class G
        {
            public const string GetAll = $"{Base}/GetAll";
            public const string Get = $"{Base}/Get";
        }
    }
}
