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
        public static bool DetectCollisionMap(Rectangle rect)
        {
            foreach (MapObject map in RenderHandler.selectedMap.ActiveLayer)
            {
                if (rect.Intersects(map.MapRectangle))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool DetectCollisionSprites(AnimatedSprite SpriteSource)
        {
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                if (sprite != SpriteSource && sprite.SpriteRectangle.Intersects(SpriteSource.SpriteRectangle))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DetectCollisionSprites(AnimatedSprite SpriteSource, out AnimatedSprite connectedSprite)
        {
            connectedSprite = null;
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                if (sprite != SpriteSource && sprite.SpriteRectangle.Intersects(SpriteSource.SpriteRectangle))
                {
                    connectedSprite = sprite;
                    return true;
                }
            }
            return false;
        }
        public static bool DetectCollisionSprites(AnimatedSprite SpriteSource, Rectangle rect, out AnimatedSprite connectedSprite)
        {
            connectedSprite = null;
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                if (sprite != SpriteSource && sprite.SpriteRectangle.Intersects(rect))
                {
                    connectedSprite = sprite;
                    return true;
                }
            }
            return false;
        }

        //bool array = {NORTH,SOUTH,EAST,WEST}
        public static bool[] DetectCollisionArrayMap(Rectangle rect)
        {
            bool East = false;
            bool West = false;
            bool North = false;
            bool South = false;
            int CenterX = rect.Right - (rect.Width / 2);
            int CenterY = rect.Bottom - (rect.Height / 2);
            foreach (MapObject map in FindObjectMaps(rect))
            {
                if (rect.Intersects(map.MapRectangle))
                {
                    if ((East || West) && (North || South)) break;
                    //EAST & WEST
                    if (rect.Right > map.MapRectangle.Left && CenterX < map.MapRectangle.Left)
                    {
                        East = true;
                        continue;
                    }
                    else if (rect.Left < map.MapRectangle.Right && CenterX > map.MapRectangle.Right)
                    {
                        West = true;
                        continue;
                    }
                    else if (rect.Top < map.MapRectangle.Bottom && CenterY > map.MapRectangle.Top)
                    {
                        North = true;
                        continue;
                    }
                    else if (rect.Bottom > map.MapRectangle.Top && CenterY < map.MapRectangle.Bottom)
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
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                Rectangle SpriteRectangle = new Rectangle(sprite.X, sprite.Y, sprite.Texture.Width, sprite.Texture.Height);
                if (rect.Intersects(SpriteRectangle))
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
        public static List<MapObject> FindObjectMaps(Rectangle rect)
        {
            List<MapObject> mapObjects = new List<MapObject>();
            List<ObjectMap> maps = ObjectMapper.BucketMap.FindAll(x => x.BucketBounds.Intersects(rect));
            foreach (ObjectMap map in maps)
            {
                foreach (MapObject i in map.Bucket)
                {
                    if (!mapObjects.Contains(i)) mapObjects.Add(i);
                }
            }
            return mapObjects;
        }
    }
}
