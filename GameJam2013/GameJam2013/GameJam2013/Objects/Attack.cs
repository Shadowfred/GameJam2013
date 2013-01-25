using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DigestionDuel.Utilities;

namespace DigestionDuel.Objects
{
    class Attack : Collideable
    {
        SpriteSheet spriteSheet;
        Vector2 velocity;
        float roation = 0.0f;
        bool edible = false;
        int points = 1;
        const float friction = 30;
        Player attackOwner;
        Color tint;

        public Attack(Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
        }

        public Attack(Texture2D spriteSheet, Vector2 velocity, Player attackOwner, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            this.spriteSheet = new SpriteSheet(spriteSheet, width, height);
            this.velocity = velocity;
            this.attackOwner = attackOwner;
            tint = Color.White;
        }

        public Attack(Texture2D spriteSheet, Vector2 velocity, Player attackOwner, Color tint, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            this.spriteSheet = new SpriteSheet(spriteSheet, width, height);
            this.velocity = velocity;
            this.attackOwner = attackOwner;
            this.tint = tint;
            this.spriteSheet.SetTint(this.tint);
        }

        public override void Update(GameTime gameTime, Map map)
        {
            velocity.Y += gravity;
            if (velocity.X == 0)
            {
                edible = true;
            }
            Move(map, owner.movers, velocity.X, velocity.Y);
        }

        public override void onCollHitting(Collideable c, float x, float y)
        {
            if (c is Wall)
            {
                if (x != 0)
                {
                    velocity.X *= -1;
                    if (velocity.X > 0)
                    {
                        velocity.X /= 2;
                    }
                    else
                    {
                        velocity.X /= 2;
                    }
                }
                if (y != 0)
                {
                    velocity.Y = 0;
                    if (velocity.X > friction)
                    {
                        velocity.X -= friction;
                    }
                    else if (velocity.X < -friction)
                    {
                        velocity.X += friction;
                    }
                    else
                    {
                        velocity.X = 0;
                    }
                }
            }
        }

        public override void onCollHit(Collideable c, float x, float y)
        {
            if (c is Player)
            {
                Player p = (Player)c;
                if (edible)
                {
                    if (p.Eating && alive)
                    {
                        p.points += points;
                        alive = false;
                    }
                }
                else if (p != attackOwner && !p.isHit)
                {
                    p.Hit();
                }
                
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            spriteSheet.Update(gameTime);
            spriteSheet.Draw(sBatch, rect, roation);
        }
    }
}
