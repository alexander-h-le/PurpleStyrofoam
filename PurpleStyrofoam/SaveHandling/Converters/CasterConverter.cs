using System;
using Newtonsoft.Json;
using PurpleStyrofoam.Managers.Classes;

namespace PurpleStyrofoam.SaveHandling.Converters
{
    public class CasterConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether or not the object is a GameClass
        /// </summary>
        /// <param name="objectType">The target object's type</param>
        /// <returns>Returns if object is a GameClass</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Caster);
        }

        /// <summary>
        /// Deserializes the JSON data
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>Returns serialized object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new Caster(Game.PlayerCharacter);
        }

        /// <summary>
        /// Serializes the class data
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
