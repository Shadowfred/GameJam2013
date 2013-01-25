using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FredLib.Graphics;
using FredLib.Input;

namespace GameJam2013.Objects
{
    class Player : Collideable
    {
        public static Texture2D attackImage;

        const int xvel = 200;
        const int jumpPower = 400;
        const int terminalVelocity = 500;
        const float hitFlash = 0.1f;
        const float shotCooldown = 0.5f;

        SpriteSheet spriteSheet;
        Vector2 velocity;
        Dictionary<string, Keys> controls;
        public int points = 0;
        public int health = 5; 

        bool eating = false;
        public bool realEating = false;
        public bool Eating
        {
            get { return eating; }
        }
        bool flipped = false;
        bool attacking = false;
        public bool isHit = false;
        float lasthit = 0.0f;
        float shotTime = 0.0f;
        bool shot = false;
        bool jumpPossible = false;
        Color tint;

        public Player(Texture2D image, Vector2 pos, int width, int height)
            : base(pos, width, height)
        {
            spriteSheet = new SpriteSheet(image, width,height);
            List<Point> animation = new List<Point>();
            //Idle
            animation.Add(new Point(4, 2));
            animation.Add(new Point(5, 2));
            animation.Add(new Point(6, 2));
            animation.Add(new Point(7, 2));
            spriteSheet.AddNewAnimation("idle", animation);
            //Walking
            animation = new List<Point>();
            animation.Add(new Point(0, 0));
            animation.Add(new Point(1, 0));
            animation.Add(new Point(2, 0));
            animation.Add(new Point(3, 0));
            animation.Add(new Point(4, 0));
            animation.Add(new Point(5, 0));
            animation.Add(new Point(6, 0));
            animation.Add(new Point(7, 0));
            spriteSheet.AddNewAnimation("walking", animation);
            //Jumping
            spriteSheet.AddNewAnimation("jump", animation);
            //Falling
            animation = new List<Point>();
            animation.Add(new Point(0, 2));
            animation.Add(new Point(1, 2));
            animation.Add(new Point(2, 2));
            animation.Add(new Point(3, 2));
            spriteSheet.AddNewAnimation("fall", animation);
            //Eating
            animation = new List<Point>();
            animation.Add(new Point(0, 1));
            animation.Add(new Point(1, 1));
            animation.Add(new Point(2, 1));
            animation.Add(new Point(3, 1));
            animation.Add(new Point(4, 1));
            animation.Add(new Point(5, 1));
            animation.Add(new Point(6, 1));
            animation.Add(new Point(7, 1));
            spriteSheet.AddNewAnimation("eat", animation);
            //Attacking
            spriteSheet.AddNewAnimation("attack", animation);
            spriteSheet.SetAnim("idle", 0.6f, flipped);
            velocity = Vector2.Zero;
            tint = Color.White;
            spriteSheet.SetTint(tint);
        }

        public void AddControls(Dictionary<string, Keys> controls)
        {
            this.controls = controls;
        }

        public void SetColour(Color colour)
        {
            tint = colour;
            spriteSheet.SetTint(colour);
        }

        public override void Update(GameTime gameTime, Map map)
        {
            velocity.Y += gravity;
            if (velocity.Y > terminalVelocity)
            {
                velocity.Y = terminalVelocity;
            }
            if (realEating)
            {
                if (spriteSheet.CurrentAnim != "eat")
                {
                    spriteSheet.SetAnim("eat", 0.7f, flipped);
                }
                if (spriteSheet.flipped != flipped)
                {
                    spriteSheet.SetAnim("eat", 0.7f, flipped);
                }
            }
            else if (velocity.Y > 5*gravity || velocity.Y < 0)
            {
                if (spriteSheet.CurrentAnim != "fall")
                {
                    spriteSheet.SetAnim("fall", 0.3f, flipped);
                }
                if (spriteSheet.flipped != flipped)
                {
                    spriteSheet.SetAnim("fall", 0.3f, flipped);
                }
            }
            else if (velocity.X != 0)
            {
                if (spriteSheet.CurrentAnim != "walking")
                {
                    spriteSheet.SetAnim("walking", 0.9f, flipped);
                }
                if (spriteSheet.flipped != flipped)
                {
                    spriteSheet.SetAnim("walking", 0.9f, flipped);
                }
            }
            else
            {
                if (spriteSheet.CurrentAnim != "idle")
                {

                    spriteSheet.SetAnim("idle", 0.6f, flipped);
                }
                if (spriteSheet.flipped != flipped)
                {
                    spriteSheet.SetAnim("idle", 0.6f, flipped);
                }
            }
            if (shot)
            {
                shotTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shotTime >= shotCooldown)
                {
                    shot = false;
                }
            }
            if (attacking)
            {
                attacking = false;
                if (!shot)
                {
                    if (owner != null && points > 0)
                    {
                        Vector2 attkDirection = new Vector2(200, -200);
                        if (flipped)
                        {
                            attkDirection.X *= -1;
                        }
                        attkDirection.X += velocity.X;

                        owner.Add(new Attack(attackImage, attkDirection, this, tint, new Vector2(rect.Center.X - 8, rect.Center.Y - 8), 16, 16), true);
                        points--;
                        shot = true;
                        shotTime = 0.0f;
                    }
                }
            }
            if (isHit)
            {
                lasthit -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (lasthit <= 0)
                {
                    lasthit = 0;
                    isHit = false;
                }
            }
            if (MainGame.fps != 0)
            {
                jumpPossible = false;
                Move(map, owner.movers ,velocity.X, velocity.Y);
            }
        }

        public void Hit()
        {
            isHit = true;
            health--;
            lasthit = 2.0f;
            attacking = false;
            eating = false;
        }

        public override void HandleInput(Input input)
        {
            velocity.X = 0;
            if (input.GetKeyState(controls["left"]) == InputState.Pressed ||
                input.GetKeyState(controls["left"]) == InputState.Held)
            {
                velocity.X -= xvel;
            }
            if (input.GetKeyState(controls["right"]) == InputState.Pressed ||
                input.GetKeyState(controls["right"]) == InputState.Held)
            {
                velocity.X += xvel;
            }
            if (velocity.X > 0)
            {
                flipped = false;
            }
            else if (velocity.X < 0)
            {
                flipped = true;
            }
            if (input.GetKeyState(controls["jump"]) == InputState.Pressed ||
                input.GetKeyState(controls["jump"]) == InputState.Held)
            {
                if (jumpPossible)
                {
                    velocity.Y = -jumpPower;
                }
            }
            if (!isHit)
            {
                if (input.GetKeyState(controls["eat"]) == InputState.Pressed ||
                    input.GetKeyState(controls["eat"]) == InputState.Held)
                {
                    eating = true;
                }
                else
                {
                    eating = false;
                }
                if (input.GetKeyState(controls["attack"]) == InputState.Pressed)
                {
                    attacking = true;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            spriteSheet.Update(gameTime);
            if (!isHit)
            {
                spriteSheet.Draw(sBatch, rect);
            }
            else
            {
                int flashtest = (int)(lasthit / hitFlash);
                if (flashtest % 2 == 0)
                {
                    spriteSheet.Draw(sBatch, rect);
                }
            }
        }

        public override void onCollHitting(Collideable c, float x, float y)
        {
            if (c is Wall)
            {
                if (y != 0)
                {
                    velocity.Y = 0;
                    if (y > 0)
                    {
                        jumpPossible = true;
                    }
                }
                if (x != 0)
                {
                    velocity.X = 0;
                }
            }
        } 
    }
}
