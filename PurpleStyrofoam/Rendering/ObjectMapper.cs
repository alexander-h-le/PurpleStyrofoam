﻿using Microsoft.Xna.Framework;
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
        public static Dictionary<int, List<MapObject>> BucketMap { get; private set; }

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
            BucketMap = new Dictionary<int, List<MapObject>>();

            Columns = map.maxBounds.Right / bucketlength;
            Rows = map.maxBounds.Bottom / bucketlength;

            int i = 0;
            for (int y = map.maxBounds.Top; y <= map.maxBounds.Bottom; y+=bucketlength)
            {
                for (int x = map.maxBounds.Left; x <= map.maxBounds.Right; x+=bucketlength)
                {
                    BucketMap.Add(i++, new List<MapObject>());
                }
            }

            foreach(MapObject mapO in map.ActiveLayer)
            {
                foreach(int index in GetObjectHashId(mapO.MapRectangle))
                {
                    BucketMap[index].Add(mapO);
                }
            }

            TotalRC = Columns * Rows;
        }

        public static List<int> GetObjectHashId(Rectangle rect)
        {
            List<int> items = new List<int>();

            //TopLeft ~~> TopRight
            for (int y = rect.Top; y < rect.Bottom; y+=bucketlength)
            {
                for (int x = rect.Left; x < rect.Right; x+= bucketlength)
                {
                    int num = GetColumn(x) + ((GetRow(y) > 0 ? GetRow(y) - 1: 0) * Columns);
                    Debug.WriteLine(y);
                    items.Add(num);
                }
            }
            //TopRight
            int TR = GetColumn(rect.Right) + ((GetRow(rect.Top) > 0 ? GetRow(rect.Top) - 1 : 0) * Columns);
            if (!items.Contains(TR)) items.Add(TR);

            //BottomLeft
            int BL = GetColumn(rect.Left) + ((GetRow(rect.Bottom) > 0 ? GetRow(rect.Bottom) - 1 : 0) * Columns);
            if (!items.Contains(BL)) items.Add(BL);

            //BottomRight
            int BR = GetColumn(rect.Right) + ((GetRow(rect.Bottom) > 0 ? GetRow(rect.Bottom) - 1 : 0) * Columns);
            if (!items.Contains(BR)) items.Add(BR);

            return items;
        }

        private static int GetColumn(int x)
        {
            return (int) Math.Ceiling((double) (x / bucketlength));
        }

        private static int GetRow(int y)
        {
            return (int) Math.Ceiling((double) (y / bucketlength));
        }

        /// <summary>
        /// Adds a new object into the <c>ObjectMap</c> table.
        /// </summary>
        /// <param name="input">The <c>MapObject</c> to be added</param>
        public static void AddMapObject(MapObject input)
        {
            foreach (int index in GetObjectHashId(input.MapRectangle))
            {
                BucketMap[index].Add(input);
            }
        }

        /// <summary>
        /// Removes an object from the <c>ObjectMap</c> table
        /// </summary>
        /// <param name="input">The <c>MapObject</c> to be removed.</param>
        public static void DeleteMapObject(MapObject input)
        {
            foreach (int index in GetObjectHashId(input.MapRectangle))
            {
                BucketMap[index].Remove(input);
            }
        }
    }
}
