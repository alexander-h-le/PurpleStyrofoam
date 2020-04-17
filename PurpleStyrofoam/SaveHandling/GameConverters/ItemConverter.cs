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
                if (type.Equals("PurpleStyrofoam.Items.ItemStack"))
                {
                    List<Item> items = new List<Item>();
                    for (int i = 0; i < item.Value<int>("count"); i++)
                    {
                        items.Add((Item)Activator.CreateInstance(Type.GetType(item.Value<string>("item"))));
                    }
                    ItemStack stack = new ItemStack(items);
                    return stack;
                }
                else return (Item)Activator.CreateInstance(Type.GetType(type));
            }
            catch
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject o = new JObject();
            if (value is ItemStack)
            {
                Item item = ((ItemStack)value).items[0];
                o.AddFirst(new JProperty("type", new JValue(value.GetType().Namespace + "." + value.GetType().Name)));
                o.Add(new JProperty("item", new JValue(item.GetType().Namespace + "." + item.GetType().Name)));
                o.Add(new JProperty("count", new JValue(((ItemStack)value).items.Count)));
            }
            else o.AddFirst(new JProperty("type", new JValue(value.GetType().Namespace + "." + value.GetType().Name)));
            o.WriteTo(writer);
        }
    }
}
