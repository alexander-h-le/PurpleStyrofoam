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
        public static bool DetectCollisionSprites(Rectangle rect, AnimatedSprite SpriteSource)
        {
            foreach (AnimatedSprite sprite in RenderHandler.allCharacterSprites)
            {
                Rectangle spriteRect = new Rectangle(sprite.X, sprite.Y, sprite.Texture.Width, sprite.Texture.Height);
                if (sprite != SpriteSource && rect.Intersects(spriteRect))
                {
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
            foreach (ObjectMap i in FindObjectMaps(rect))
            {
                foreach (MapObject map in i.Bucket)
                {
                    if (rect.Intersects(map.MapRectangle))
                    {
                        //EAST & WEST
                        if (rect.Right >= map.MapRectangle.Left && CenterX < map.MapRectangle.Left)
                        {
                            East = true;
                        }
                        else if (rect.Left <= map.MapRectangle.Right && CenterX > map.MapRectangle.Right)
                        {
                            West = true;
                        }
                        else if (rect.Top <= map.MapRectangle.Bottom && CenterY > map.MapRectangle.Top)
                        {
                            North = true;
                        }
                        else if (rect.Bottom >= map.MapRectangle.Top && CenterY < map.MapRectangle.Bottom)
                        {
                            South = true;
                        }
                        if ((East || West) && (North || South)) break;
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
        public static List<ObjectMap> FindObjectMaps(Rectangle rect)
        {
            Debug.WriteLine("START ---------------------------");
            List<ObjectMap> maps = ObjectMapper.BucketMap.FindAll(x => rect.Intersects(x.BucketBounds));
            maps = new HashSet<ObjectMap>(maps).ToList();
            Debug.WriteLine($"maps: {maps.Count} + all: {RenderHandler.selectedMap.ActiveLayer.Length}");
            foreach (ObjectMap map in maps)
            {
                foreach (MapObject i in map.Bucket)
                {
                    Debug.WriteLine(i.name);
                }
            }
            Debug.WriteLine("END ---------------------------");
            return maps;
        }
    }
}
