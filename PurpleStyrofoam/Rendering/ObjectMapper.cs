using Microsoft.Xna.Framework;
using PurpleStyrofoam.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public static class ObjectMapper
    {
        public static List<ObjectMap> BucketMap { get; private set; }
        public static int Rows { get; set; }
        public static int Columns { get; set; }
        private const int bucketlength = 100;
        public static void MapObjects(BaseMap map)
        {
            BucketMap = new List<ObjectMap>();
            int BucketMapKeys = 0;

            int minX = (int) map.ActiveLayer[0].location.X;
            int maxX = 0;
            int minY = (int) map.ActiveLayer[0].location.Y;
            int maxY = 0;
            foreach (MapObject i in map.ActiveLayer)
            {
                if (i.MapRectangle.X < minX)
                {
                    minX = i.MapRectangle.X;
                } else if (i.MapRectangle.X > maxX)
                {
                    maxX = i.MapRectangle.X;
                }
                if (i.MapRectangle.Y < minY)
                {
                    minY = i.MapRectangle.Y;
                } else if (i.MapRectangle.Y > maxY)
                {
                    maxY = i.MapRectangle.Y;
                }
            }
            Columns = (maxX - minX) / bucketlength;
            Rows = (maxY - minY) / bucketlength;
            Debug.WriteLine($"C: {Columns} R: {Rows}");

            for (int y = minY; y <= maxY; y += bucketlength)
            {
                for (int x = minX; x <= maxX; x += bucketlength)
                {
                    BucketMap.Add(new ObjectMap());
                    BucketMap.Last().Key = BucketMapKeys++;
                    BucketMap.Last().BucketBounds = new Rectangle(x, y, x + bucketlength, y + bucketlength);
                    foreach (MapObject i in map.ActiveLayer)
                    {
                        if (i.MapRectangle.Intersects(BucketMap.Last().BucketBounds))
                        {
                            BucketMap.Last().Bucket.Add(i);
                        }
                    }
                }
            }
        }

        public static int FindLowestKey(List<ObjectMap> maps)
        {
            int lowest = maps[0].Key;
            foreach(ObjectMap key in maps)
            {
                if (lowest > key.Key)
                {
                    lowest = key.Key;
                }
            }
            return lowest;
        }
        public static int FindHighestKey(List<ObjectMap> maps)
        {
            int lowest = maps[0].Key;
            foreach (ObjectMap key in maps)
            {
                if (lowest > key.Key)
                {
                    lowest = key.Key;
                }
            }
            return lowest;
        }
    }
}
