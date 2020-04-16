using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering;

namespace PurpleStyrofoam.Items.Weapons
{
    public abstract class Weapon : Item
    {

        private Type[] EquippableClass;

        protected Weapon(string nameIn, int damage, float atkspd, RARITY levelIn, ItemSprite imageIn, params Type[] EquippableBy) : base(nameIn, levelIn, imageIn)
        {
            Damage = damage;
            AttackSpeed = atkspd;
            EquippableClass = EquippableBy;
        }

        public int Damage { get; } // Get the item's damage.

        /// <summary>
        /// Called when player first left clicks
        /// </summary>
        public void OnLeftClickStart()
        {

        }

        public void DuringLeftClick()
        {
            AnimatedSprite temp;
            CollisionDetection.DetectCollisionSprites(Game.PlayerCharacter, Sprite.ItemRectangle, out temp);
            DamageHelper.DamageTarget(-Damage, temp);
            float charDir = Game.PlayerCharacter.velocity.X / Game.PlayerCharacter.velocity.X;
            if (temp != null)
            {
                temp.SpriteRectangle.Y -= 10;
                if (Game.PlayerCharacter.velocity.X != 0)
                {
                    temp.velocity += new Vector2(Game.PlayerCharacter.velocity.X + (100 * charDir), -350);
                }
                else
                {
                    temp.velocity += new Vector2(Game.PlayerCharacter.animate.Flipped ? -400 : 400, -350);
                }
            }
        }

        /// <summary>
        /// called when the swing animation is over.
        /// </summary>
        public void OnLeftClickEnd()
        {

        }
        public abstract void OnQAbility(); // Will be called when player presses Q
        public float AttackSpeed { get; set; }
        public override void OnInventoryUse()
        {
            foreach (Type t in EquippableClass)
            {
                if (t == Game.PlayerManager.Class.GetType()) Game.PlayerManager.Inventory.SwitchItems(this, Game.PlayerManager.Inventory.Items[0]);
            }
        }

        public void LeftClick()
        {
            Sprite.OnStart = OnLeftClickStart;
            Sprite.DuringSwing = DuringLeftClick;
            Sprite.OnEnd = OnLeftClickEnd;

            Sprite.StartAnimation(AttackSpeed);
        }
    }

    public class ATTACKSPEED
    {
        public static float 
            INSANELY_SLOW = 0.0375f, 
            VERY_SLOW = 0.075f, 
            SLOW = 0.1025f, 
            MODERATE = 0.15f, 
            FAST = 0.1875f, 
            VERY_FAST = 0.2250f, 
            INSANELY_FAST = 0.2625f;
    }
}
