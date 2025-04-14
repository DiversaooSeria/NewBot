using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameApi.Models
{
    public class Button
    {
        public string Id { get; set; }
        public string ButtonName { get; set; }
        public DateTime ClickTime { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}
