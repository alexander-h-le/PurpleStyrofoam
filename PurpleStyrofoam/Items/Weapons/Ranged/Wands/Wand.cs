using Microsoft.Xna.Framework;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Weapons.Ranged.Wands
{
    public abstract class Wand : Ranged
    {
        public Wand(string nameIn, int damage, Color levelIn, ItemSprite imageIn) : 
            base(nameIn, damage, ATTACKSPEED.FAST, levelIn, imageIn, new Vector2(40,40), typeof(Caster))
        {
        }

        public override void OnLeftClickStart()
        {
            _ = new CasterProjectile(Game.PlayerCharacter, 
                new Point(Game.PlayerCharacter.SpriteRectangle.Right, Game.PlayerCharacter.SpriteRectangle.Y), 
                Damage, 
                Projectile.GenerateVelocityVector(Game.PlayerCharacter.SpriteRectangle.Location.ToVector2(), MouseHandler.mousePos));
            
        }
    }
}
