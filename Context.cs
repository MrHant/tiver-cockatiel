using System.Net.Http;

namespace tiver_cockatiel
{
    public static class Context
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        public static string LogDatetime;
        public static string LogFilepath;
    }
}