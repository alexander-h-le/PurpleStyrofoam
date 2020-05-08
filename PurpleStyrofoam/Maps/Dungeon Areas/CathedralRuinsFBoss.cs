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
using PurpleStyrofoam.Buffs.CommonBuffs;
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
            InteractableLayer = new MapInteractable[]
            {
                new MapInteractable(TextureHelper.Blank(Color.LightGreen), new Vector2(600, 2500), 50,50, false, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(600,2500,50,50), 100);
                    foreach (AnimatedSprite i in sp)
                        i.Buffs.AddBuff(new RegenerationBuff(GameMathHelper.TimeToFrames(2), 10, i));
                }),

                new MapInteractable(TextureHelper.Blank(Color.Orange), new Vector2(500, 2800), 50,50, false, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(500,2800,50,50), 100);
                    if (sp.Contains(Game.PlayerCharacter)){
                        Game.PlayerCharacter.Buffs.AddBuff(new AttackSpeedBuff(-1 ,5));
                        sp.Clear();
                    }
                }),

                new MapInteractable(TextureHelper.Blank(Color.LightBlue), new Vector2(600, 2800), 50,50, true, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(600,2800,50,50), 100);
                    if (sp.Contains(Game.PlayerCharacter))
                        Game.PlayerCharacter.Buffs.AddBuff(new SpeedBuff(GameMathHelper.TimeToFrames(10), 2, Game.PlayerCharacter));
                }),

                new MapInteractable(TextureHelper.Blank(Color.White), new Vector2(700, 2800), 50,50, true, () => {
                    AnimatedSprite temp;
                    temp = new AnimatedSprite(TextureHelper.Sprites.EnemySprite, 1,1, 700, 2800,
                            new BasicAI(Game.PlayerCharacter), new DefaultManager(10));
                    temp.AI.SupplyAI(temp);
                    temp.Load();
                    temp.SpriteRectangle.Width = 50;
                    temp.SpriteRectangle.Height = 50;
                    RenderHandler.allCharacterSprites.Add(temp);
                }),

                new MapInteractable(TextureHelper.Blank(Color.DeepSkyBlue), new Vector2(800, 2800), 50,50, true, () => {
                    List<AnimatedSprite> sp = CollisionDetection.GetNearby(new Rectangle(800,2800,50,50), 100);
                    foreach (AnimatedSprite i in sp)
                        i.Buffs.AddBuff(new WindyBuff(GameMathHelper.TimeToFrames(10), 10, i));
                })
            };
            ActiveLayer = new MapObject[]
                {
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 0), 1600, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 0), 100, 500),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 500), 800, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1200, 500), 500, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1600, 0), 100, 500),

                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(700, 600), 100, 1000),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1200, 600), 100, 1000),

                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 1600), 800, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1200, 1600), 600, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 1600), 100, 1500),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1700, 1600), 100, 900),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1700, 2800), 100, 300),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(0, 3100), 1800, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(300, 2925), 1400, 50),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(100, 2600), 700, 50),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1000, 2750), 400, 50),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(300, 2200), 1200, 50),

                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1800, 2800), 800, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(1800, 2400), 800, 100),

                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(2600, 2800), 100, 300),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(2600, 3100), 400, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(3200, 3100), 400, 100),
                    new MapObject(TextureHelper.Blank(Color.DarkGray), new Vector2(3600, 1200), 100, 2000),
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
