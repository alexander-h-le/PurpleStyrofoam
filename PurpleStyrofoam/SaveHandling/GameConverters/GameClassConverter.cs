using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PurpleStyrofoam.Managers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.SaveHandling
{
    class GameClassConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(GameClass).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            var type = item.Value<string>("type");
            return (GameClass)Activator.CreateInstance(Type.GetType(type));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject o = new JObject();
            o.AddFirst(new JProperty("type", new JValue(value.GetType().Namespace + "." + value.GetType().Name)));
            o.WriteTo(writer);
        }
    }
}
