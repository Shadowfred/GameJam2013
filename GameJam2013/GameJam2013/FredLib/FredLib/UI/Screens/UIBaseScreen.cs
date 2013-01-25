using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FredLib.Input;

namespace FredLib.UI.Screens
{
    public class UIBaseScreen
    {
        #region Attributes
        protected string screenName;
        /// <summary>
        /// Name of the screen.
        /// </summary>
        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        protected bool isActive = true;
        /// <summary>
        /// If currently has focus.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        protected bool drawable = true;
        /// <summary>
        /// If not drawable, screen won't draw.
        /// </summary>
        public bool Drawable
        {
            get { return drawable; }
            set { drawable = value; }
        }

        public UIBaseScreen parent = null;
        protected UIScreenManager screenManager = null;

        protected bool paused = false;
        /// <summary>
        /// If paused, no updates will occur.
        /// </summary>
        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        protected float transitionProgress = 0.0f;
        protected TimeSpan transitionOn = TimeSpan.Zero;
        protected TimeSpan transitionOff = TimeSpan.Zero;
        public enum TransitionState
        {
            TransitionOn,
            TransitionOff,
            NoTransition
        }
        public TransitionState tState = TransitionState.TransitionOn;

        protected bool canClose = false;

        #endregion

        #region Initialisation
        /// <summary>
        /// Base creation of a screen.
        /// </summary>
        /// <param name="sM">The ScreenManager of the screen.</param>
        /// <param name="name">The identifier of the screen.</param>
        public UIBaseScreen(UIScreenManager sM, string name)
        {
            screenName = name;
            screenManager = sM;
        }

        /// <summary>
        /// Base creation of a screen, has a parent.
        /// </summary>
        /// <param name="p">Parent of the screen.</param>
        /// <param name="sM">The ScreenManager of the screen.</param>
        /// <param name="name">The identifier of the screen.</param>
        /// <param name="pause">True if parent shouldn't update while screen is active.</param>
        /// <param name="drawable">False if parent shouldn't draw while screen is active.</param>
        public UIBaseScreen(UIBaseScreen p, UIScreenManager sM, string name, bool pause, bool drawable)
        {
            parent = p;
            screenName = name;
            screenManager = sM;
            parent.IsActive = false;
            if (pause)
            {

                parent.Paused = true;
            }
            else
            {
                parent.Paused = false;
            }
            if (!drawable)
            {
                parent.Drawable = false;
            }
            else
            {
                parent.Drawable = true;
            }
        }

        #endregion

        public void StartClose()
        {
            tState = TransitionState.TransitionOff;
        }

        public void CloseScreen()
        {
            if (parent != null)
            {
                parent.IsActive = true;
                parent.Paused = false;
                parent.Drawable = true;
            }
            screenManager.RemoveScreen(this);
        }

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public virtual void Update(GameTime gameTime)
        {
            UpdateTransition(gameTime);
            if (canClose)
            {
                CloseScreen();
            }
        }
        
        /// <summary>
        /// Handles the input for the screen.
        /// </summary>
        /// <param name="input">The current input state for the game.</param>
        public virtual void HandleInput(Input.Input input) { }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        public virtual void Draw(GameTime gameTime) { }

        public virtual void RescaleElements(int width, int height) { }

        public void UpdateTransition(GameTime gameTime)
        {
            float transitionDelta = 0;
            if (tState == TransitionState.TransitionOn)
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                         transitionOn.TotalMilliseconds);
            }
            else if (tState == TransitionState.TransitionOff)
            {
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                         transitionOff.TotalMilliseconds);
            }

            if (tState != TransitionState.NoTransition)
            {
                transitionProgress += transitionDelta;
                if (transitionProgress >= 1)
                {
                    transitionProgress = 1;
                    if (tState == TransitionState.TransitionOff)
                    {
                        canClose = true;
                    }
                    tState = TransitionState.NoTransition;
                }
            }
            else
            {
                transitionProgress = 0;
            }
        }

    }
}
