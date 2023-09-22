using System.Text;
using System.Text.Json;

namespace ResponsiBoss.BlazorServerApp//.Identity.Extensions
{
    public static class SessionStorageExtensions
    {
        public static void SaveItemEncrypted<T>(this Dictionary<Guid, string> cutomStorage, Guid key, T item)
        {
            var itemJson = JsonSerializer.Serialize(item);
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJsonBytes);
            cutomStorage.Add(key, base64Json);
        }

        public static T ReadEncryptedItem<T>(this Dictionary<Guid, string> cutomStorage, Guid key)
        {
            var base64Json = cutomStorage.TryGetValue(key, out string value) ? value : String.Empty;
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }
    }
}