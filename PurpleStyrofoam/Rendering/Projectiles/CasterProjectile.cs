using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PurpleStyrofoam.Buffs.CommonBuffs;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Managers.Classes;
using PurpleStyrofoam.Rendering.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Rendering.Projectiles
{
    public class CasterProjectile : Projectile
    {
        int Damage;
        AnimatedSprite source;
        Rectangle ProjectileRectangle;
        Vector2 Speed;
        Texture2D t;
        Caster.SpellType spellType;

        public CasterProjectile(AnimatedSprite src, Point location, int dmg, Vector2 spd)
        {
            source = src;
            Damage = dmg;
            ProjectileRectangle = new Rectangle(location, new Point(25,25));
            Speed = spd;
            spellType = ((Caster)Game.PlayerManager.Class).CurrentSpellType;

            switch (spellType)
            {
                case Caster.SpellType.FIRE:
                    t = TextureHelper.Blank(Color.Red);
                    break;
                case Caster.SpellType.ICE:
                    t = TextureHelper.Blank(Color.SkyBlue);
                    break;
                case Caster.SpellType.WIND:
                    t = TextureHelper.Blank(Color.Yellow);
                    break;
                case Caster.SpellType.EARTH:
                    t = TextureHelper.Blank(Color.DarkGreen);
                    break;
                default:
                    t = TextureHelper.Blank(Color.White);
                    break;
            }

            RenderHandler.allProjectiles.Add(this);
        }

        public override void DetectCollision()
        {
            MapObject tempMap;
            if (CollisionDetection.DetectCollisionMap(ProjectileRectangle, out tempMap))
                if (!(tempMap is MapInteractable)) Delete();

            AnimatedSprite temp;
            if (CollisionDetection.DetectCollisionSprite(source, ProjectileRectangle, out temp))
                if (temp != null) ProjectileAction(source, temp);
        }

        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(t, ProjectileRectangle.Location.ToVector2(), new Rectangle(0,0,ProjectileRectangle.Width,ProjectileRectangle.Height),
                Color.White, 0f, new Vector2(), 1f, SpriteEffects.None, 1.0f);
        }

        public override void ProjectileAction(AnimatedSprite source, AnimatedSprite target)
        {
            switch (spellType)
            {
                // Fire Attack
                case Caster.SpellType.FIRE:
                    target.AddHealth(-Damage);

                    // Fire + Ice
                    if (target.Buffs.HasBuff(typeof(SlowBuff))) {
                        target.Buffs.RemoveBuff(typeof(SlowBuff));
                        FireIce(target);
                    }

                    // Fire + Wind
                    else if (target.Buffs.HasBuff(typeof(DamageReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DamageReductionBuff));
                        FireWind(target);
                    }

                    // Fire + Earth
                    else if (target.Buffs.HasBuff(typeof(DefenseReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DefenseReductionBuff));
                        FireEarth(target);
                    }
                    else target.Buffs.AddBuff(new BurningBuff(GameMathHelper.TimeToFrames(3), 1, target));
                    break;
                
                // Ice Attack
                case Caster.SpellType.ICE:
                    target.AddHealth(-Damage);

                    // Fire + Ice
                    if (target.Buffs.HasBuff(typeof(BurningBuff))) {
                        target.Buffs.RemoveBuff(typeof(BurningBuff));
                        FireIce(target);
                    }

                    // Ice + Wind
                    else if (target.Buffs.HasBuff(typeof(DamageReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DamageReductionBuff));
                        IceWind(target);
                    }

                    // Ice + Earth
                    else if (target.Buffs.HasBuff(typeof(DefenseReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DefenseReductionBuff));
                        IceEarth(target);
                    }
                    else target.Buffs.AddBuff(new SlowBuff(GameMathHelper.TimeToFrames(3), 1, target));
                    break;

                // Wind Attack
                case Caster.SpellType.WIND:
                    target.AddHealth(-Damage);

                    // Fire + Wind
                    if (target.Buffs.HasBuff(typeof(BurningBuff))) {
                        target.Buffs.RemoveBuff(typeof(BurningBuff));
                        FireWind(target);
                    }

                    // Ice + Wind
                    else if (target.Buffs.HasBuff(typeof(SlowBuff))) {
                        target.Buffs.RemoveBuff(typeof(SlowBuff));
                        IceWind(target);
                    }

                    // Wind + Earth
                    else if (target.Buffs.HasBuff(typeof(DefenseReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DefenseReductionBuff));
                        WindEarth(target);
                    }
                    else target.Buffs.AddBuff(new DamageReductionBuff(GameMathHelper.TimeToFrames(3), 1, target));
                    break;

                // Earth Attack
                case Caster.SpellType.EARTH:
                    target.AddHealth(-Damage);

                    // Fire + Earth
                    if (target.Buffs.HasBuff(typeof(BurningBuff))) {
                        target.Buffs.RemoveBuff(typeof(BurningBuff));
                        FireEarth(target);
                    }

                    // Ice + Earth
                    else if (target.Buffs.HasBuff(typeof(SlowBuff))) {
                        target.Buffs.RemoveBuff(typeof(SlowBuff));
                        IceEarth(target);
                    }

                    // Wind + Earth
                    else if (target.Buffs.HasBuff(typeof(DamageReductionBuff))) {
                        target.Buffs.RemoveBuff(typeof(DamageReductionBuff));
                        WindEarth(target);
                    }
                    else target.Buffs.AddBuff(new DefenseReductionBuff(GameMathHelper.TimeToFrames(3), 1, target));
                    break;
                default:
                    target.AddHealth(-Damage);
                    break;
            }

            Delete();
        }

        private void FireIce(AnimatedSprite target)
        {
            List<AnimatedSprite> sp = CollisionDetection.GetNearby(target.SpriteRectangle, 75);
            foreach (AnimatedSprite i in sp)
            {
                if (i != Game.PlayerCharacter) i.AddHealth((int)(-(Damage * 1.5)));
            }
        }

        private void FireWind(AnimatedSprite target)
        {
            List<AnimatedSprite> sp = CollisionDetection.GetNearby(target.SpriteRectangle, 75);
            foreach (AnimatedSprite i in sp)
            {
                if (i != Game.PlayerCharacter) i.Buffs.AddBuff(new BurningBuff(6, 3, i));
            }
        }

        private void FireEarth(AnimatedSprite target)
        {
            target.Buffs.AddBuff(new BurningBuff(6, 5, target));
        }

        private void IceWind(AnimatedSprite target)
        {
            List<AnimatedSprite> sp = CollisionDetection.GetNearby(target.SpriteRectangle, 75);
            foreach (AnimatedSprite i in sp)
            {
                if (i != Game.PlayerCharacter) i.Buffs.AddBuff(new FrostbittenBuff(6, 1, i));
            }
        }

        private void IceEarth(AnimatedSprite target)
        {
            List<AnimatedSprite> sp = CollisionDetection.GetNearby(target.SpriteRectangle, 75);
            foreach (AnimatedSprite i in sp)
            {
                if (i != Game.PlayerCharacter) i.Buffs.AddBuff(new FrozenBuff(10, i));
            }
        }

        private void WindEarth(AnimatedSprite target)
        {
            List<AnimatedSprite> sp = CollisionDetection.GetNearby(target.SpriteRectangle, 75);
            foreach (AnimatedSprite i in sp)
            {
                if (i != Game.PlayerCharacter) i.Buffs.AddBuff(new WindyBuff(5, 2 ,i));
            }
        }

        public override void ProjectileAction(AnimatedSprite source, MapObject target)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            ProjectileRectangle.Location += Speed.ToPoint();
            DetectCollision();
        }
    }
}
