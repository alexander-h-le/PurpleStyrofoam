using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering
{
    public static class CollisionDetection
    {
        public static bool DetectCollisionMap(Rectangle rect, out MapObject ConnectedObject)
        {
            foreach (MapObject map in FindObjectBuckets(rect))
            {
                if (rect.Intersects(map.MapRectangle))
                {
                    ConnectedObject = map;
                    return true;
                }
            }
            ConnectedObject = null;
            return false;
        }
        public static List<MapObject> DetectCollisionMaps(Rectangle rect)
        {
            List<MapObject> objects = new List<MapObject>();
            foreach (MapObject map in FindObjectBuckets(rect))
            {
                if (rect.Intersects(map.MapRectangle) && !objects.Contains(map)) objects.Add(map);
            }
            return objects;
        }
        public static bool DetectCollisionSprite(AnimatedSprite SpriteSource, Rectangle rect, out AnimatedSprite connectedSprite)
        {
            foreach (AnimatedSprite sprite in FindSprites(rect))
            {
                if (sprite != SpriteSource && sprite.SpriteRectangle.Intersects(rect))
                {
                    connectedSprite = sprite;
                    return true;
                }
            }
            connectedSprite = null;
            return false;
        }

        public static bool DetectCollisionSprites(AnimatedSprite SpriteSource, Rectangle rect, out AnimatedSprite[] connectedSprite)
        {
            List<AnimatedSprite> connected = new List<AnimatedSprite>();
            foreach (AnimatedSprite sprite in FindSprites(rect))
            {
                if (sprite != SpriteSource && sprite.SpriteRectangle.Intersects(rect))
                {
                    connected.Add(sprite);
                }
            }
            connectedSprite = connected.ToArray();
            if (connectedSprite.Length > 0) return true;
            else return false;
        }

        /// <summary>
        /// {North South East West}
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool[] DetectCollisionArrayMap(Rectangle rect)
        {
            bool East = false;
            bool West = false;
            bool North = false;
            bool South = false;
            int CenterX = rect.Right - (rect.Width / 2);
            int CenterY = rect.Bottom - (rect.Height / 2);
            foreach (MapObject map in FindObjectBuckets(rect))
            {
                if (RenderHandler.selectedMap.InteractableLayer.Contains(map)) continue;

                if (rect.Intersects(map.MapRectangle))
                {
                    if ((East || West) && (North || South)) break;
                    if (!East && rect.Right > map.MapRectangle.Left && CenterX < map.MapRectangle.Left)
                    {
                        East = true;
                        continue;
                    }
                    else if (!West && rect.Left < map.MapRectangle.Right && CenterX > map.MapRectangle.Right)
                    {
                        West = true;
                        continue;
                    }
                    else if (!North && rect.Top < map.MapRectangle.Bottom && CenterY > map.MapRectangle.Top)
                    {
                        North = true;
                        continue;
                    }
                    else if (!South && rect.Bottom > map.MapRectangle.Top && CenterY < map.MapRectangle.Bottom)
                    {
                        South = true;
                        continue;
                    }
                }
            }
            return new bool[] { North, South, East, West };
        }
        public static bool[] DetectCollisionArraySprites(Rectangle rect, AnimatedSprite SpriteSource)
        {
            bool East = false;
            bool West = false;
            bool North = false;
            bool South = false;
            int CenterX = rect.Right - (rect.Width / 2);
            int CenterY = rect.Bottom - (rect.Height / 2);
            foreach (AnimatedSprite sprite in FindSprites(rect))
            {
                Rectangle SpriteRectangle = sprite.SpriteRectangle;
                if (rect.Intersects(SpriteRectangle) && sprite != SpriteSource)
                {
                    //EAST & WEST
                    if (rect.Right >= SpriteRectangle.Left && CenterX < SpriteRectangle.Left)
                    {
                        East = true;
                    }
                    else if (rect.Left <= SpriteRectangle.Right && CenterX > SpriteRectangle.Right)
                    {
                        West = true;
                    }
                    else if (rect.Top <= SpriteRectangle.Bottom && CenterY > SpriteRectangle.Top)
                    {
                        North = true;
                    }
                    else if (rect.Bottom >= SpriteRectangle.Top && CenterY < SpriteRectangle.Bottom)
                    {
                        South = true;
                    }
                    if ((East || West) && (North || South)) break;
                }
            }
            return new bool[] { North, South, East, West };
        }

        public static List<AnimatedSprite> GetNearby(Rectangle source, int range)
        {
            Rectangle Check = new Rectangle(source.X-range, source.Y-range, 
                source.Width + (2*range), source.Height + (2*range));

            List<AnimatedSprite> nearby = new List<AnimatedSprite>();
            foreach (AnimatedSprite i in FindSprites(Check)) 
                if (!nearby.Contains(i) && i.SpriteRectangle.Intersects(Check)) 
                    nearby.Add(i);

            return nearby;
        }

        public static List<MapObject> FindObjectBuckets(Rectangle rect)
        {
            List<MapObject> mapObjects = new List<MapObject>();
            foreach (int i in ObjectMapper.GetObjectHashId(rect))
            {
                foreach (MapObject map in ObjectMapper.BucketMap[i])
                    if (!mapObjects.Contains(map)) mapObjects.Add(map);
            }
            return mapObjects;
        }

        /// <summary>
        /// Finds all the sprites that are in the same buckets as specified rectangle
        /// </summary>
        /// <param name="rect">Rectangle to check</param>
        /// <returns>List of sprites</returns>
        public static List<AnimatedSprite> FindSprites(Rectangle rect)
        {
            List<AnimatedSprite> sprites = new List<AnimatedSprite>();
            foreach (int i in ObjectMapper.GetObjectHashId(rect))
            {
                foreach (AnimatedSprite s in ObjectMapper.BucketSprite[i])
                    if (!sprites.Contains(s)) sprites.Add(s);
            }
            return sprites;
        }
    }
}
