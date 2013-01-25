using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FredLib.Input;

namespace FredLib.UI.Screens
{
    public class UIScreenManager
    {
        #region Attributes
        /// <summary>
        /// A list containing all the screens that are currently loaded.
        /// </summary>
        List<UIBaseScreen> screens;

        /// <summary>
        /// A list containing all the screens to add to the screenManager.
        /// </summary>
        List<UIBaseScreen> toAdd;

        /// <summary>
        /// A list containing all the screens to remove from the screenManager
        /// </summary>
        List<UIBaseScreen> toRemove;

        /// <summary>
        /// The spritebatch used to draw 2D elements.
        /// </summary>
        public SpriteBatch sBatch;
        
        /// <summary>
        /// A rasterizerState to allow for scissoring when drawing some UI elements.
        /// </summary>
        public RasterizerState rState;

        /// <summary>
        /// The image used for the cursor.
        /// </summary>
        public Texture2D cursor;

        /// <summary>
        /// Where the cursor is currently.
        /// </summary>
        Vector2 cursorPos;

        /// <summary>
        /// Says wether to draw the cursor or not.
        /// </summary>
        public bool cursorActive = true;

        public bool gameFocus = true;

        #endregion

        #region Initialisation
        /// <summary>
        /// A manager that contains every screen.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sB"></param>
        public UIScreenManager(SpriteBatch sB)
        {
            screens = new List<UIBaseScreen>();
            toAdd = new List<UIBaseScreen>();
            toRemove = new List<UIBaseScreen>();
            sBatch = sB;
            rState = new RasterizerState();
            rState.ScissorTestEnable = true;
        }
        #endregion

        #region Update, Handle Input, Draw
        public void Update(GameTime gameTime)
        {
            foreach (UIBaseScreen s in screens)
            {
                if (!s.Paused)
                {
                    s.Update(gameTime);
                }
            }
            foreach (UIBaseScreen s in toAdd)
            {
                screens.Add(s);
            }
            foreach (UIBaseScreen s in toRemove)
            {
                if (s.tState == UIBaseScreen.TransitionState.NoTransition)
                {
                    screens.Remove(s);
                }
            }
            toAdd.Clear();
            toRemove.Clear();

            
        }

        public void HandleInput(Input.Input input)
        {
            if (gameFocus)
            {
                cursorActive = true;
                foreach (UIBaseScreen s in screens)
                {
                    if (s.IsActive)
                    {
                        if (s.ScreenName == "game" || s.ScreenName == "loading")
                        {
                            cursorActive = false;
                        }
                        s.HandleInput(input);
                    }
                }
                cursorPos = input.MousePosV();
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (UIBaseScreen s in screens)
            {
                if (s.Drawable)
                {
                    s.Draw(gameTime);
                }
            }
            if (cursorActive)
            {
                sBatch.Begin();
                sBatch.Draw(cursor, cursorPos, Color.White);
                sBatch.End();
            }
        }
        #endregion

        #region Public Functions
        public void AddScreen(UIBaseScreen screen)
        {
            toAdd.Add(screen);
        }

        public void RemoveScreen(UIBaseScreen screen)
        {
            toRemove.Add(screen);
        }

        public UIBaseScreen GetScreen(string name)
        {
            UIBaseScreen screen = null;
            foreach (UIBaseScreen s in screens)
            {
                if (name == s.ScreenName)
                {
                    screen = s;
                }
            }
            return screen;
        }

        public void RescaleElements(int width, int height)
        {
            foreach (UIBaseScreen s in screens)
            {
                s.RescaleElements(width, height);
            }
        }
        #endregion
    }
}
