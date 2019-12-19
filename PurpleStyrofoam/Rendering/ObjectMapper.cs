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
    /// <summary>
    /// Maps objects to a readable matrix. Decreases method calls, increasing game speed
    /// </summary>
    public static class ObjectMapper
    {
        /// <summary>
        /// List of buckets which contain all <c>MapObjects</c> in a certain range
        /// </summary>
        public static List<ObjectMap> BucketMap { get; private set; }

        /// <summary>
        /// The number of rows of <c>ObjectMap</c>s there are.
        /// </summary>
        public static int Rows { get; set; }

        /// <summary>
        /// The number of columns of <c>ObjectMap</c>s there are.
        /// </summary>
        public static int Columns { get; set; }

        /// <summary>
        /// The total number of cells there are in this table of <c>ObjectMap</c>s
        /// </summary>
        public static int TotalRC { get; set; }
        private const int bucketlength = 100;

        /// <summary>
        /// Maps all the objects in a given map to the readable table.
        /// </summary>
        /// <param name="map">The <c>BaseMap</c> to be mapped</param>
        public static void MapObjects(BaseMap map)
        {
            BucketMap = new List<ObjectMap>();
            int BucketMapKeys = 0;

            int minX = map.maxBounds.Left;
            int maxX = map.maxBounds.Right;
            int minY = map.maxBounds.Top;
            int maxY = map.maxBounds.Bottom;
            Columns = (maxX - minX) / bucketlength;
            Rows = (maxY - minY) / bucketlength;
            minX -= bucketlength;
            maxX += bucketlength;
            minY -= bucketlength;
            maxY += bucketlength;

            for (int y = minY; y < maxY; y += bucketlength)
            {
                for (int x = minX; x < maxX; x += bucketlength)
                {
                    ObjectMap objectMap = new ObjectMap();
                    objectMap.Key = BucketMapKeys++;
                    objectMap.BucketBounds = new Rectangle(x, y,bucketlength, bucketlength);
                    foreach (MapObject i in map.ActiveLayer)
                    {
                        if (i.MapRectangle.Intersects(objectMap.BucketBounds))
                        {
                            objectMap.Bucket.Add(i);
                        }
                    }
                    BucketMap.Add(objectMap);
                }
            }
            TotalRC = Columns * Rows;
        }

        /// <summary>
        /// Given a list of <c>ObjectMap</c>s, it finds the one with the lowest index
        /// </summary>
        /// <param name="maps">The list of <c>ObjectMap</c>s to be iterated through</param>
        /// <returns>The index of the <c>ObjectMap</c> with the lowest index</returns>
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

        /// <summary>
        /// Given a list of <c>ObjectMap</c>s, it finds the one with the highest index
        /// </summary>
        /// <param name="maps">The list of <c>ObjectMap</c>s to be iterated through</param>
        /// <returns>The index of the <c>ObjectMap</c> with the highest index</returns>
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

        /// <summary>
        /// Adds a new object into the <c>ObjectMap</c> table.
        /// </summary>
        /// <param name="input">The <c>MapObject</c> to be added</param>
        public static void AddMapObject(MapObject input)
        {
            foreach (ObjectMap i in BucketMap.FindAll(x => x.BucketBounds.Intersects(input.MapRectangle)))
            {
                i.Bucket.Add(input);
            }
            RenderHandler.extras.Add(input);
        }

        /// <summary>
        /// Removes an object from the <c>ObjectMap</c> table
        /// </summary>
        /// <param name="input">The <c>MapObject</c> to be removed.</param>
        public static void DeleteMapObject(MapObject input)
        {
            foreach (ObjectMap i in BucketMap)
            {
                i.Bucket.Remove(input);
            }
            RenderHandler.extras.Remove(input);
        }
    }
}
