using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Text
{
    public class DamageIndicator
    {
        List<DamageIndication> damages;
        List<DamageIndication> toRemove;
        Rectangle location;
        SpriteFont font;

        public DamageIndicator()
        {
            damages = new List<DamageIndication>();
            toRemove = new List<DamageIndication>();
            font = Game.GameContent.Load<SpriteFont>(TextureHelper.Fonts.Default);
        }

        public void Update(Rectangle pos)
        {
            location = pos;
            foreach (DamageIndication i in damages)
            {
                i.visiblity -= 0.02f;
                if (i.visiblity <= 0f) toRemove.Add(i);
            }
            foreach (DamageIndication i in toRemove) damages.Remove(i);
            toRemove.Clear();
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (DamageIndication i in damages)
            {
                sp.DrawString(font, i.value.ToString(),
                    location.Location.ToVector2() + i.pos, i.color * i.visiblity,
                    0f, new Vector2(), i.scale, SpriteEffects.None, 1f);
                i.pos.Y--;
            }
        }

        public void NewDamage(int amt, Color c)
        {
            Random rand = new Random();
            Vector2 vec = new Vector2(
                rand.Next(location.Left, location.Right), 
                rand.Next(location.Top, location.Bottom - (int)font.MeasureString(amt.ToString()).Y));
            vec -= location.Location.ToVector2();
            damages.Add(new DamageIndication(amt, vec, 0.9f, c));
        }
    }

    class DamageIndication
    {
        public int value;
        public float visiblity = 1.0f;
        public Vector2 pos;
        public float scale;
        public Color color;

        public DamageIndication(int value, Vector2 pos, float scale, Color c)
        {
            this.value = value;
            this.pos = pos;
            this.scale = scale;
            this.color = c;
        }
    }
}
