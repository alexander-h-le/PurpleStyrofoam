using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.SaveHandling.GameConverters
{
    class AnimatedSpriteConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AnimatedSprite);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            return new AnimatedSprite(item["texture"].Value<string>(), 1, 1, item["X"].Value<int>(), item["Y"].Value<int>(), 
                (AIBase) Activator.CreateInstance(Type.GetType(item["ai"].Value<string>())), item["manager"].ToObject<IManager>());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            AnimatedSprite sp = (AnimatedSprite) value;
            JObject newObj = new JObject();
            JObject manager = JObject.FromObject(sp.Manager);
            newObj.Add(new JProperty("texture", new JValue(sp.textureName)));
            newObj.Add(new JProperty("X", new JValue(sp.SpriteRectangle.X)));
            newObj.Add(new JProperty("Y", new JValue(sp.SpriteRectangle.Y)));
            newObj.Add(new JProperty("manager", manager));
            newObj.Add(new JProperty("ai", sp.AI.GetType().Namespace + "." + sp.AI.GetType().Name));
        }
    }
}
