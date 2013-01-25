using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FredLib.Graphics;

namespace GameJam2013.Objects
{
    class RockWall : Wall
    {
        SpriteSheet spriteSheet;
        Dictionary<Player,Point> eatingPos;
        List<Player> eating;
        int points = 1;
        int life = 1;
        float lastHit = 0;
        const int decayTime = 2;

        public RockWall(Texture2D image, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            CommonInit(image, width, height);
        }

        public RockWall(Texture2D image, int life, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            this.life = life;
            points = life;
            CommonInit(image, width, height);
        }

        void CommonInit(Texture2D image, int width, int height)
        {
            eatingPos = new Dictionary<Player, Point>();
            eating = new List<Player>();
            spriteSheet = new SpriteSheet(image, width, height);

            List<Point> animation = new List<Point>();
            animation.Add(new Point(0, 0));
            spriteSheet.AddNewAnimation("default", animation);
            animation = new List<Point>();
            animation.Add(new Point(0, 0));
            animation.Add(new Point(1, 0));
            animation.Add(new Point(2, 0));
            animation.Add(new Point(3, 0));
            animation.Add(new Point(4, 0));
            spriteSheet.AddNewAnimation("eating", animation);
            spriteSheet.SetAnim("default", 0.0f);
        }

        public override void Update(GameTime gameTime)
        {
            List<Player> remove = new List<Player>();
            foreach (Player p in eating)
            {
                if (!p.Eating)
                {
                    eatingPos.Remove(p);
                    remove.Add(p);
                }
                else if (p.rect.Location != eatingPos[p])
                {
                    eatingPos.Remove(p);
                    remove.Add(p);
                }
            }
            foreach (Player p in remove)
            {
                p.realEating = false;
                eating.Remove(p);
            }

            if (eating.Count > 0 && lastHit == 0)
            {
                lastHit = decayTime;
                spriteSheet.SetAnim("eating", decayTime);
            }
            if (eating.Count > 0)
            {
                lastHit -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (lastHit <= 0)
                {
                    life--;
                }
                if (life == 0)
                {
                    alive = false;
                    eating[0].points += points;
                    eating[0].realEating = false;
                }
            }
            else
            {
                life = points;
                lastHit = 0;
                if (spriteSheet.CurrentAnim != "default")
                {
                    spriteSheet.SetAnim("default", 0.0f);
                }
            }
        }

        public override void onCollHit(Collideable c, float x, float y)
        {
            base.onCollHit(c, x, y);
            if (c is Player)
            {
                Player p = (Player)c;
                if (x != 0)
                {
                    if (p.Eating)
                    {
                        if (!eating.Contains(p))
                        {
                            eating.Add(p);
                            p.realEating = true;
                            eatingPos.Add(p, p.rect.Location);
                        }
                    }
                    else
                    {
                        if (eating.Contains(p))
                        {
                            eating.Remove(p);
                            p.realEating = false;
                            eatingPos.Remove(p);
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            spriteSheet.Update(gameTime);
            spriteSheet.Draw(sBatch, rect);
        }
    }
}
