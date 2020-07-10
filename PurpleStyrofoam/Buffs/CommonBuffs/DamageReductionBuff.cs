using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs.CommonBuffs
{
    public class DamageReductionBuff : Buff
    {
        int OriginalDamage;
        public DamageReductionBuff(int dur, int lvl, AnimatedSprite target) : 
            base("Damage Reduction", dur, lvl, "You feel weak", texture: TextureHelper.Blank(Color.Gray))
        {
            OnStart = () => { 
                if (target == Game.PlayerCharacter)
                    Game.PlayerManager.EquippedWeapon.Damage = (int)(Game.PlayerManager.EquippedWeapon.Damage * (1 - (0.1 * lvl)));
                else
                {
                    OriginalDamage = target.Manager.Damage;
                    target.Manager.Damage = (int) (target.Manager.Damage * (1 - (0.1 * lvl)));
                }
            };

            OnEnd = () =>
            {
                if (target == Game.PlayerCharacter) 
                    Game.PlayerManager.EquippedWeapon.Damage = ((Weapon) Activator.CreateInstance(Game.PlayerManager.EquippedWeapon.GetType())).Damage;
                else
                {
                    target.Manager.Damage = OriginalDamage;
                }
            };
        }

        public override void UpdateFrom(Buff b)
        {
            OriginalDamage = ((DamageReductionBuff)b).OriginalDamage;
        }
    }
}
