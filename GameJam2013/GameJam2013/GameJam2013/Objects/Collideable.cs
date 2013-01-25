using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DigestionDuel.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace DigestionDuel.Objects
{
    class Collideable
    {
        protected const int gravity = 20;

        public Rectangle rect;
        public CollideableManager owner;
        public Vector2 pos;

        protected bool alive = true;
        public bool Alive { get { return alive; } } 

        public Collideable()
        {
            this.pos = Vector2.Zero;
            this.rect = new Rectangle();
        }

        public Collideable(Vector2 pos, int width, int height)
        {
            this.pos = pos;
            this.rect = new Rectangle((int)pos.X,(int)pos.Y,width,height);
        }

        protected bool CheckColl(Collideable col)
        {
            return (rect.Intersects(col.rect));
        }

        public void CheckColl(List<Collideable> cols, float x, float y)
        {
            foreach (Collideable c in cols)
            {
                if (this != c && CheckColl(c))
                {
                    c.onCollHit(this, x, y);
                    onCollHitting(c, x, y);
                }
            }
        }

        protected void Move(Map map, List<Collideable> cols, float x, float y)
        {
            List<Collideable> allCols = new List<Collideable>();
            if (y != 0)
            {
                pos.Y += (y / MainGame.fps);
                rect.Y = (int)pos.Y;
                allCols.AddRange(map.ClosestTo(pos));
                allCols.AddRange(cols);
                CheckColl(allCols, 0, y);
            }
            if (x != 0)
            {
                pos.X += (x / MainGame.fps);
                rect.X = (int)pos.X;
                allCols.Clear();
                allCols.AddRange(map.ClosestTo(pos));
                allCols.AddRange(cols);
                CheckColl(allCols, x, 0);
            }
        }

        public virtual void onCollHit(Collideable c, float x, float y) { }
        public virtual void onCollHitting(Collideable c, float x, float y) { }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Update(GameTime gameTime, Map map) { }
        public virtual void HandleInput(Input input) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch) { }

    }

    class CollideableManager
    {
        public List<Collideable> movers;
        public List<Collideable> nonMovers; 
        List<Collideable> remove;
        List<Collideable> toAddMovers;
        List<Collideable> toAddNonMovers;

        public CollideableManager()
        {
            movers = new List<Collideable>();
            nonMovers = new List<Collideable>();
            remove = new List<Collideable>();
            toAddMovers = new List<Collideable>();
            toAddNonMovers = new List<Collideable>();
        }

        public void Update(GameTime gameTime)
        {
            remove.Clear();
            foreach (Collideable c in toAddNonMovers)
            {
                nonMovers.Add(c);
            }
            toAddNonMovers.Clear();
            foreach (Collideable c in nonMovers)
            {
                c.Update(gameTime);
                if (!c.Alive)
                {
                    remove.Add(c);
                }
            }
            foreach (Collideable c in remove)
            {
                nonMovers.Remove(c);
            }
        }

        /*public void Update(GameTime gameTime, Map map)
        {
            List<Collideable> remove = new List<Collideable>();
            foreach (Collideable c in movers)
            {
                c.Update(gameTime, cols);
                if (!c.Alive)
                {
                    remove.Add(c);
                }
            }
            foreach (Collideable c in remove)
            {
                movers.Remove(c);
            }
        }*/

        public void Update(GameTime gameTime, Map map)
        {
            remove.Clear();
            foreach (Collideable c in toAddMovers)
            {
                movers.Add(c);
            }
            toAddMovers.Clear();
            foreach (Collideable c in movers)
            {
                c.Update(gameTime, map);
                if (!c.Alive)
                {
                    remove.Add(c);
                }
            }
            foreach (Collideable c in remove)
            {
                movers.Remove(c);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            foreach (Collideable c in movers)
            {
                c.Draw(gameTime, sBatch);
            }
            foreach (Collideable c in nonMovers)
            {
                c.Draw(gameTime, sBatch);
            }
        }

        public void HandleInput(Input input)
        {
            foreach (Collideable c in movers)
            {
                c.HandleInput(input);
            }
        }

        public void Add(Collideable c, bool movers)
        {
            c.owner = this;
            if (movers)
            {
                toAddMovers.Add(c);
            }
            else
            {
                toAddNonMovers.Add(c);
            }
        }
    }
}
