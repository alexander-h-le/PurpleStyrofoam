using System;
using Newtonsoft.Json;
using PurpleStyrofoam.Items;
using PurpleStyrofoam.Items.Armors;
using PurpleStyrofoam.Items.Materials;
using PurpleStyrofoam.Items.Miscellaneous_Items.Companions;
using PurpleStyrofoam.Items.Potions;
using PurpleStyrofoam.Items.Weapons;

namespace PurpleStyrofoam.SaveHandling
{
    public class ItemConverter : JsonConverter
    {
        public ItemConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Item) ||
                objectType == typeof(Weapon) ||
                objectType == typeof(Potion) ||
                objectType == typeof(Companion) ||
                objectType == typeof(Material) ||
                objectType == typeof(Armor);
        }

        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(Weapon)) return serializer.Deserialize(reader, typeof(Weapon));
            else if (objectType == typeof(Potion)) return serializer.Deserialize(reader, typeof(Potion));
            else if (objectType == typeof(Companion)) return serializer.Deserialize(reader, typeof(Companion));
            else if (objectType == typeof(Material)) return serializer.Deserialize(reader, typeof(Material));
            else if (objectType == typeof(Armor)) return serializer.Deserialize(reader, typeof(Armor));
            else return serializer.Deserialize(reader, typeof(Item));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
