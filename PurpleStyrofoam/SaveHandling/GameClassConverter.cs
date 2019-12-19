using Newtonsoft.Json;
using PurpleStyrofoam.Managers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.SaveHandling
{
    /// <summary>
    /// A converter for the originally undeserializable GameClass
    /// </summary>
    public class GameClassConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether or not the object is a GameClass
        /// </summary>
        /// <param name="objectType">The target object's type</param>
        /// <returns>Returns if object is a GameClass</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GameClass);
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
            if (objectType == typeof(Knight)) return serializer.Deserialize(reader, typeof(Knight));
            else return serializer.Deserialize(reader, typeof(Knight));
        }

        /// <summary>
        /// Serializes the class data
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,value);
        }
    }
}
