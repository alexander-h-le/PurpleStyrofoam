using Microsoft.Xna.Framework;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Melee
{
    public abstract class Melee : Weapon
    {
        protected Melee(string nameIn, int damage, float atkspd, Color levelIn, ItemSprite imageIn, Vector2 ItemSize, params Type[] EquippableBy) : 
            base(nameIn, damage, atkspd, levelIn, imageIn, ItemSize, EquippableBy)
        {
        }

        public override void DuringLeftClick()
        {
            AnimatedSprite[] newSprites;
            CollisionDetection.DetectCollisionSprites(Game.PlayerCharacter, Sprite.ItemRectangle, out newSprites);
            float charDir = Game.PlayerCharacter.velocity.X / Game.PlayerCharacter.velocity.X;
            float LaunchVelocity = 500f * KnockBack;
            foreach (AnimatedSprite i in newSprites)
            {
                if (oldSprites.Contains(i)) continue;

                i.AddHealth(-Damage);
                i.SpriteRectangle.Y -= 10;
                if (Game.PlayerCharacter.velocity.X != 0)
                {
                    i.velocity += new Vector2(Game.PlayerCharacter.velocity.X + (LaunchVelocity * charDir), -350);
                }
                else
                {
                    i.velocity += new Vector2(Game.PlayerCharacter.animate.Flipped ? -LaunchVelocity : LaunchVelocity, -350);
                }
            }
            oldSprites = newSprites;
        }
    }
}
