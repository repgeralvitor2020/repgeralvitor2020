using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CopaFilmes.Common.WebApi.Extensions
{
    public static class ContextExtension
    {
        private static string _sessionKey = "sessionid";

        public static string GetSessionID(this HttpContext context, bool isNew = false)
        {
            if (isNew)
                return context.Session.Id;
            else
                return context.Request.Headers.Keys.Contains(_sessionKey) ?
                    context.Request.Headers.First(h => h.Key.Equals(_sessionKey)).Value.ToString() : string.Empty;
        }
    }
}
