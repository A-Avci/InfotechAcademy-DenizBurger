using Newtonsoft.Json;

namespace İnfotechProje.Extensions
{
    public static class SessionExtensions
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
