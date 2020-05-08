using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Buffs.CommonBuffs
{
    public class AttackSpeedBuff : Buff
    {

        float OriginalSpeed;

        /// <summary>
        /// Increases the attack speed of target weapon
        /// Note: Only applies to player character+
        /// </summary>
        /// <param name="dur">Duration of Buff</param>
        /// <param name="lvl">Buff Strength</param>
        public AttackSpeedBuff(int dur, int lvl) : 
            base("Attack Speed", dur, lvl, "Your weapon feels lighter", texture: TextureHelper.Blank(Color.Orange))
        {
            OnStart = () =>
            {
                OriginalSpeed = Game.PlayerManager.EquippedWeapon.AttackSpeed;
                Game.PlayerManager.EquippedWeapon.AttackSpeed = OriginalSpeed + (lvl * 0.0375f);
            };

            OnEnd = () => { Game.PlayerManager.EquippedWeapon.AttackSpeed = OriginalSpeed; };
        }
    }
}
