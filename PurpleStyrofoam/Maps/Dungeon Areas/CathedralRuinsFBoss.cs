using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PurpleStyrofoam.AiController;
using PurpleStyrofoam.AiController.AIs;
using PurpleStyrofoam.Buffs;
using PurpleStyrofoam.Helpers;
using PurpleStyrofoam.Managers;
using PurpleStyrofoam.Rendering;
using PurpleStyrofoam.Rendering.Sprites;

namespace PurpleStyrofoam.Maps.Dungeon_Areas
{
    public class CathedralRuinsFBoss : DungeonArea
    {
        public CathedralRuinsFBoss()
        {
            maxBounds = new Rectangle(0,0,10000,10000);
            ContentManager Content = Game.GameContent;
            BackgroundLayer = new MapObject[]
                {
                };
            float atksped = 0.0f;
            float spd = 0.0f;
            float jheight = 0.0f;
            TimerHelper t = null;
            TimerHelper t2 = null;
            InteractableLayer = new MapInteractable[]
            {
                new MapInteractable(TextureHelper.Blank(Color.DarkRed), new Vector2(200, 2500), 50,50, false, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(200,2500,50,50), 150);
                    foreach (AnimatedSprite i in sp)
                        i.Buffs.AddBuff(new Buff(
                            "On Fire!",
                            2,
                            start: () => {if (t == null) t = new TimerHelper(GameMathHelper.TimeToFrames(0.3f), () => { i.AddHealth(-1); });},
                            end: () => { t?.Delete(); t = null; },
                            texture: TextureHelper.Blank(Color.Red)
                        ));
                }),

                new MapInteractable(TextureHelper.Blank(Color.LightGreen), new Vector2(700, 2500), 50,50, false, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(700,2500,50,50), 150);
                    foreach (AnimatedSprite i in sp)
                        i.Buffs.AddBuff(new Buff(
                            "Regeneration",
                            2,
                            start: () => {if (t2 == null) t2 = new TimerHelper(GameMathHelper.TimeToFrames(0.3), () => { i.AddHealth(2); }); },
                            end: () => { t2?.Delete(); t2 = null; }
                        ));
                }),

                new MapInteractable(TextureHelper.Blank(Color.Orange), new Vector2(500, 2800), 50,50, false, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(500,2800,50,50), 100);
                    if (sp.Contains(Game.PlayerCharacter)){
                        Game.PlayerCharacter.Buffs.AddBuff(new Buffs.Buff(
                            "Attack Speed",
                            -1,
                            start: () => {
                                atksped = Game.PlayerManager.EquippedWeapon.AttackSpeed;
                                Game.PlayerManager.EquippedWeapon.AttackSpeed += 10.0f;
                            },
                            end: () => {
                                Game.PlayerManager.EquippedWeapon.AttackSpeed = atksped;
                            },
                            texture: TextureHelper.Blank(Color.Orange)
                        ));
                        sp.Clear();
                    }
                }),

                new MapInteractable(TextureHelper.Blank(Color.LightBlue), new Vector2(600, 2800), 50,50, true, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(600,2800,50,50), 100);
                    if (sp.Contains(Game.PlayerCharacter))
                    {
                        Game.PlayerCharacter.Buffs.AddBuff(new Buffs.Buff(
                        "Super speed",
                        GameMathHelper.TimeToFrames(8),
                        start: () => {
                            spd = Game.PlayerCharacter.terminalVelocity.X;
                            Game.PlayerCharacter.terminalVelocity.X = 1000;
                        },
                        end: () => {
                            Game.PlayerCharacter.terminalVelocity.X = spd;
                        },
                        texture: TextureHelper.Blank(Color.LightBlue)
                        ));
                    }
                }),

                new MapInteractable(TextureHelper.Blank(Color.White), new Vector2(700, 2800), 50,50, true, () => {
                    AnimatedSprite temp;
                    temp = new AnimatedSprite(TextureHelper.Sprites.EnemySprite, 1,1, 700, 2800,
                            new BasicAI(Game.PlayerCharacter), new DefaultManager());
                    temp.AI.SupplyAI(temp);
                    temp.Load();
                    temp.SpriteRectangle.Width = 50;
                    temp.SpriteRectangle.Height = 50;
                    RenderHandler.allCharacterSprites.Add(temp);
                }),

                new MapInteractable(TextureHelper.Blank(Color.DeepSkyBlue), new Vector2(800, 2800), 50,50, true, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(800,2800,50,50), 100);
                    if (sp.Contains(Game.PlayerCharacter))
                    {
                        Game.PlayerCharacter.Buffs.AddBuff(new Buffs.Buff(
                        "Super Jump",
                        GameMathHelper.TimeToFrames(8),
                        start: () => {
                            jheight = Game.PlayerCharacter.terminalVelocity.Y;
                            Game.PlayerCharacter.terminalVelocity.Y = 2000;
                        },
                        end: () => {
                            Game.PlayerCharacter.terminalVelocity.Y = jheight;
                        },
                        texture: TextureHelper.Blank(Color.DeepSkyBlue)
                        ));
                    }
                })
            };
            ActiveLayer = new MapObject[]
                {
                    new MapObject("testIMG", new Vector2(0, 0), 1600, 100),
                    new MapObject("testIMG", new Vector2(0, 0), 100, 500),
                    new MapObject("testIMG", new Vector2(0, 500), 800, 100),
                    new MapObject("testIMG", new Vector2(1200, 500), 500, 100),
                    new MapObject("testIMG", new Vector2(1600, 0), 100, 500),

                    new MapObject("testIMG", new Vector2(700, 600), 100, 1000),
                    new MapObject("testIMG", new Vector2(1200, 600), 100, 1000),

                    new MapObject("testIMG", new Vector2(0, 1600), 800, 100),
                    new MapObject("testIMG", new Vector2(1200, 1600), 600, 100),
                    new MapObject("testIMG", new Vector2(0, 1600), 100, 1500),
                    new MapObject("testIMG", new Vector2(1700, 1600), 100, 900),
                    new MapObject("testIMG", new Vector2(1700, 2800), 100, 300),
                    new MapObject("testIMG", new Vector2(0, 3100), 1800, 100),
                    new MapObject("testIMG", new Vector2(300, 2925), 1400, 50),
                    new MapObject("testIMG", new Vector2(100, 2600), 700, 50),
                    new MapObject("testIMG", new Vector2(1000, 2750), 400, 50),
                    new MapObject("testIMG", new Vector2(300, 2200), 1200, 50),

                    new MapObject("testIMG", new Vector2(1800, 2800), 800, 100),
                    new MapObject("testIMG", new Vector2(1800, 2400), 800, 100),

                    new MapObject("testIMG", new Vector2(2600, 2800), 100, 300),
                    new MapObject("testIMG", new Vector2(2600, 3100), 400, 100),
                    new MapObject("testIMG", new Vector2(3200, 3100), 400, 100),
                    new MapObject("testIMG", new Vector2(3600, 1200), 100, 2000),
                };
            ForegroundLayer = new MapObject[]
                {
                };
        }
        public override MapObject[] BackgroundLayer { get; set; }
        public override MapObject[] ActiveLayer { get; set; }
        public override MapObject[] ForegroundLayer { get; set; }
        public override List<AnimatedSprite> sprites { get; set; }
        public override MapInteractable[] InteractableLayer { get; set; }
    }
}
