using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PurpleStyrofoam.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.SaveHandling.GameConverters
{
    class ItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Item).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try 
            { 
                JObject item = JObject.Load(reader);
                var type = item.Value<string>("type");
                return (Item)Activator.CreateInstance(Type.GetType(type));
            }
            catch
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject o = new JObject();
            o.AddFirst(new JProperty("type", new JValue(value.GetType().Namespace + "." + value.GetType().Name)));
            o.WriteTo(writer);
        }
    }
}
