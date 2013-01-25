using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FredLib.Input;

namespace FredLib.UI.Elements
{
    /// <summary>
    /// The base class for all UIElements. Should only be used when using more than one type of element in a collection.
    /// </summary>
    public class UIElement
    {
        /// <summary>
        /// The state of a UIElement.
        /// </summary>
        public enum UIState
        {
            Default,
            MouseOver,
            Clicked,
            Held,
            Disabled
        }

        #region Attributes
       
        protected bool active = true;
        /// <summary>
        /// Non-active elements will not update, handle input or draw.
        /// </summary>
        public bool Active
        {
            get {return active;}
            set {active = value;}
        }

        protected UIElement parent = null;
        /// <summary>
        /// The element's parent.
        /// </summary>
        public UIElement Parent
        {
            get {return parent;}
            set {parent = value;}
        }
        protected List<UIElement> children;
        /// <summary>
        /// All children of the element.
        /// </summary>
        public List<UIElement> Children
        {
            get {return children;}
            set {children = value;}
        }

        protected Vector2 relativePosition = Vector2.Zero;
        /// <summary>
        /// The element's position relative to its parent.
        /// </summary>
        public Vector2 RelativePosition
        {
            get {return relativePosition;}
            set {relativePosition = value;}
        }

        protected Vector2 position = Vector2.Zero;
        /// <summary>
        /// The element's actual position.
        /// </summary>
        public Vector2 Position
        {
            get {return position;}
            set {position = value;}
        }

        protected int width = 0;
        protected int height = 0;


        /// <summary>
        /// The current state of the element.
        /// </summary>
        protected UIState state = UIState.Default;

        protected Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }

        protected Color tint = Color.White;
        public Color Tint
        {
            get { return tint; }
            set { tint = value; }
        }

        protected float alpha = 1.0f;
        public float Alpha
        {
            get { return alpha; }
        }

        #endregion
        
        #region Events
        /// <summary>
        /// When the mouse starts to click on the element.
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// When the mouse button is released over the element.
        /// </summary>
        public event EventHandler Released;
        /// <summary>
        /// When the mouse moves over the element.
        /// </summary>
        public event EventHandler MouseOver;

        protected virtual void OnClick()
        {
            if (Clicked != null)
            {
                Clicked(this, new EventArgs());
            }
        }

        protected virtual void OnRelease()
        {
            if (Released != null)
            {
                Released(this, new EventArgs());
            }
        }

        protected virtual void OnMouseOver()
        {
            if (MouseOver != null)
            {
                MouseOver(this, new EventArgs());
            }
        }
        #endregion

        #region Initialisation

        /// <summary>
        /// Create a new UIElement with default values.
        /// </summary>
        public UIElement()
        {
            children = new List<UIElement>();
        }

        /// <summary>
        /// Creates a new UIElement with default values.
        /// </summary>
        /// <param name="p">The parent of the element.</param>
        public UIElement(UIElement p)
        {
            parent = p;
            parent.children.Add(this);
            children = new List<UIElement>();
        }

        #endregion

        #region Update, Handle Input, Draw
        /// <summary>
        /// Updates the element.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Handles the input for the element.
        /// </summary>
        /// <param name="input">The current input state for the game.</param>
        public virtual void HandleInput(Input.Input input) { }

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun.</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch) { }

        /// <summary>
        /// Draws all active children.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        /// <param name="sBatch">The spritebatch to draw to, should already be begun.</param>
        protected void DrawChildren(GameTime gameTime, SpriteBatch sBatch)
        {
            if (children != null && children.Count() > 0)
            {
                foreach (UIElement e in children)
                {
                    if (e.Active)
                    {
                        e.Draw(gameTime, sBatch);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the input of all active children.
        /// </summary>
        /// <param name="input">The current input state for the game.</param>
        protected void HandleInputChildren(Input.Input input)
        {
            List<UIElement> toHandle = new List<UIElement>();
            if (children != null && children.Count() > 0)
            {
                foreach (UIElement e in children)
                {
                    if (e.Active)
                    {
                        toHandle.Add(e);
                    }
                }
                foreach (UIElement e in toHandle)
                {
                    if (e.Active)
                    {
                        e.HandleInput(input);
                    }
                }
            }
        }

        /// <summary>
        /// Updates all children.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        protected void UpdateChildren(GameTime gameTime)
        {
            if (children != null && children.Count() > 0)
            {
                foreach (UIElement e in children)
                {
                    if (e.Active)
                    {
                        e.Update(gameTime);
                    }
                }
            }
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Disables the element.
        /// </summary>
        public void Disable()
        {
            state = UIState.Disabled;
        }

        /// <summary>
        /// Enables the element.
        /// </summary>
        public void Enable()
        {
            state = UIState.Default;
        }

        public void ChangePos(Vector2 newPos)
        {
            relativePosition = newPos;
            UpdatePosition();
            UpdateChildPosition();
        }

        protected void UpdateChildPosition()
        {
            foreach (UIElement e in children)
            {
                e.UpdatePosition();
            }
        }

        protected void UpdateChildPosition(Vector2 offset)
        {
            foreach (UIElement e in children)
            {
                e.UpdatePosition(offset);
            }
        }

        public virtual void UpdatePosition()
        {
            if (parent != null)
            {
                position = parent.position + relativePosition;
            }
            else
            {
                position = relativePosition;
            }
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
            UpdateChildPosition();
        }

        public virtual void UpdatePosition(Vector2 offset)
        {
            if (parent != null)
            {
                position = offset + parent.position + relativePosition;
            }
            else
            {
                position = offset + relativePosition;
            }
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
            UpdateChildPosition();
        }

        public void SetAlpha(float alpha)
        {
            this.alpha = alpha;
            SetChildAlpha(alpha);
        }

        public virtual void RescaleElement(int width, int height)
        {
            foreach (UIElement e in children)
            {
                e.RescaleElement(width, height);
            }
        }

        protected void SetChildAlpha(float alpha)
        {
            foreach (UIElement e in children)
            {
                e.SetAlpha(alpha);
            }
        }
        #endregion
    }
}
