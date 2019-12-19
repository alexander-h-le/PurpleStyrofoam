using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    /// <summary>
    /// The storage for the <c>MapObject</c>s within a certain range
    /// </summary>
    public class ObjectMap
    {
        /// <summary>
        /// Index of the object
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// The actual container of the <c>MapObject</c>s
        /// </summary>
        public List<MapObject> Bucket { get; set; }

        /// <summary>
        /// The area the the <c>ObjectMap</c> encompasses
        /// </summary>
        public Rectangle BucketBounds { get; set; }

        public ObjectMap()
        {
            Bucket = new List<MapObject>();
        }
    }
}
