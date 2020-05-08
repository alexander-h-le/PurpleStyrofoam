using Microsoft.Xna.Framework;
using PurpleStyrofoam.Buffs;
using PurpleStyrofoam.Buffs.CommonBuffs;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Items.Potions.BuffPotions
{
    public class RegularSpeedPotion : Potion
    {
        public RegularSpeedPotion() : base("Speed Potion", RARITY.UNCOMMON, new ItemSprite("Potions/BuffPotions/RegularSpeed"))
        {
        }

        public override string EffectDescription => "speed";

        public override int ID => 420;

        public override string Description => "you go fast";

        public override int Duration => GameMathHelper.TimeToFrames(10);

        public override void OnInventoryUse()
        {
            float original = Game.PlayerCharacter.terminalVelocity.X;
            if (Game.PlayerCharacter.Buffs.AddBuff(new SpeedBuff(1, GameMathHelper.TimeToFrames(10), Game.PlayerCharacter))) 
                Game.PlayerManager.Inventory.DeleteItem(this);
        }

        public override void Update()
        {

        }
    }
}
