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

        protected Weapon(string nameIn, int damage, float atkspd, Color levelIn, ItemSprite imageIn, Vector2 ItemSize, params Type[] EquippableBy) : base(nameIn, levelIn, imageIn)
        {
            Damage = damage;
            AttackSpeed = atkspd;
            EquippableClass = EquippableBy;
            Size = ItemSize;
        }

        public int Damage { get; set; } // Get the item's damage.

        public Vector2 Size { get; set; }
        public float KnockBack = 1f;

        /// <summary>
        /// Called when player first left clicks
        /// </summary>
        public virtual void OnLeftClickStart() { }

        protected AnimatedSprite[] oldSprites = new AnimatedSprite[0];
        public virtual void DuringLeftClick()
        {
            AnimatedSprite[] newSprites;
            CollisionDetection.DetectCollisionSprites(Game.PlayerCharacter, Sprite.ItemRectangle, out newSprites);
            float charDir = Game.PlayerCharacter.velocity.X / Game.PlayerCharacter.velocity.X;
            float LaunchVelocity = 500f * KnockBack;
            foreach (AnimatedSprite i in newSprites)
            {
                if (oldSprites.Contains(i)) continue;

                DamageHelper.DamageTarget(-Damage, i);
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

        /// <summary>
        /// Called when the swing animation is over. If you are overriding this method, make sure to empty the <code>oldSprites</code> array.
        /// </summary>
        public virtual void OnLeftClickEnd() {
            oldSprites = new AnimatedSprite[0];
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
