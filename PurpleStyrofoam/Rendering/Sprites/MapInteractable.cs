using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Sprites
{
    public class MapInteractable : MapObject
    {
        public MapInteractable(string textureName, Vector2 position, int w, int h, bool Click, Action mapAction) : base(textureName, position, w, h) 
        {
            if (Click) ClickAction = mapAction;
            else MapAction = mapAction;
        }

        public MapInteractable(Texture2D text, Vector2 position, int w, int h, bool Click, Action mapAction) : base(text, position, w, h)
        {
            if (Click) ClickAction = mapAction;
            else MapAction = mapAction;
        }

        public List<AnimatedSprite> Nearby(int range)
        {
            return CollisionDetection.GetNearby(MapRectangle, range);
        }

        public Action ClickAction;
        public Action MapAction;
    }
}
