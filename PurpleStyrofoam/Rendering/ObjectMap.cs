using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public class ObjectMap
    {
        public int Key { get; set; }
        public List<MapObject> Bucket { get; set; }
        public Rectangle BucketBounds { get; set; }

        public ObjectMap()
        {
            Bucket = new List<MapObject>();
        }
    }
}
