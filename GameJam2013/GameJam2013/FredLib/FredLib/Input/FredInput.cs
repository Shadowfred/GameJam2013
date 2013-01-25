using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FredLib.Input
{
    /// <summary>
    /// States for both keys and buttons.
    /// </summary>    
    public enum InputState
    {
        Default,
        Pressed,
        Held,
        Released
    }

    /// <summary>
    /// All useable mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        Left,
        Right,
        Middle,
        Mouse4,
        Mouse5
    }

    /// <summary>
    /// A class that handles the mouse state and the keyboard state.
    /// </summary>
    public class Input
    {
        
        #region Attributes
        /// <summary>
        /// The old mouse state.
        /// </summary>
        MouseState oldMouseState;
        /// <summary>
        /// The old keyboard state.
        /// </summary>
        KeyboardState oldKeyState;

        /// <summary>
        /// The current mouse state.
        /// </summary>
        MouseState mouseState;
        /// <summary>
        /// The current keyboard state.
        /// </summary>
        KeyboardState keyState;
        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new Input class.
        /// </summary>
        public Input()
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates the states of the keyboard and mouse.
        /// </summary>
        public void Update()
        {
            oldKeyState = keyState;
            oldMouseState = mouseState;

            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        #endregion

        #region MainFunctions

        //KEYBOARD

        /// <summary>
        /// Returns an array of all currently pressed keys.
        /// </summary>
        /// <returns></returns>
        public Keys[] GetPressedKeys()
        {
            return keyState.GetPressedKeys();
        }

        /// <summary>
        /// Returns if the key is currently down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key)
        {
            return (keyState.IsKeyDown(key));
        }

        /// <summary>
        /// Returns if the key is currently up.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public bool IsKeyUp(Keys key)
        {
            return (keyState.IsKeyUp(key));
        }

        /// <summary>
        /// Returns the state of k.
        /// </summary>
        /// <param name="k">The key to check.</param>
        public InputState GetKeyState(Keys k)
        {
            if (IsKeyPressed(k))
            {
                return InputState.Pressed;
            }
            else if (IsKeyHeld(k))
            {
                return InputState.Held;
            }
            else if (IsKeyReleased(k))
            {
                return InputState.Released;
            }
            else
            {
                return InputState.Default;
            }
        }

        //MOUSE

        /// <summary>
        /// Sets the mouse position using a Vector2.
        /// </summary>
        public void SetMousePosV(Vector2 pos)
        {
            Mouse.SetPosition((int)pos.X, (int)pos.Y);
            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Sets the mouse position using a Point.
        /// </summary>
        public void SetMousePosP(Point pos)
        {
            Mouse.SetPosition(pos.X, pos.Y);
            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Gets an array of the currently pressed mouse buttons.
        /// </summary>
        /// <returns></returns>
        public MouseButtons[] GetPressedButtons()
        {
            List<MouseButtons> pressed = new List<MouseButtons>();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                pressed.Add(MouseButtons.Left);
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                pressed.Add(MouseButtons.Right);
            }
            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                pressed.Add(MouseButtons.Middle);
            }
            if (mouseState.XButton1 == ButtonState.Pressed)
            {
                pressed.Add(MouseButtons.Mouse4);
            }
            if (mouseState.XButton2 == ButtonState.Pressed)
            {
                pressed.Add(MouseButtons.Mouse5);
            }

            return pressed.ToArray();
        }

        /// <summary>
        /// Returns the input state of the button requested.
        /// </summary>
        /// <param name="button">The mouse button to use.</param>
        public InputState GetMouseState(MouseButtons button)
        {
            if (button == MouseButtons.Left)
            {
                if (LeftClick())
                {
                    return InputState.Pressed;
                }
                else if (LeftHeld())
                {
                    return InputState.Held;
                }
                else if (LeftReleased())
                {
                    return InputState.Released;
                }
                else
                {
                    return InputState.Default;
                }
            }
            else if (button == MouseButtons.Right)
            {
                if (RightClick())
                {
                    return InputState.Pressed;
                }
                else if (RightHeld())
                {
                    return InputState.Held;
                }
                else if (RightReleased())
                {
                    return InputState.Released;
                }
                else
                {
                    return InputState.Default;
                }
            }
            else if (button == MouseButtons.Middle)
            {
                if (MiddleClick())
                {
                    return InputState.Pressed;
                }
                else if (MiddleHeld())
                {
                    return InputState.Held;
                }
                else if (MiddleReleased())
                {
                    return InputState.Released;
                }
                else
                {
                    return InputState.Default;
                }
            }
            else if (button == MouseButtons.Mouse4)
            {
                if (Mouse4Click())
                {
                    return InputState.Pressed;
                }
                else if (Mouse4Held())
                {
                    return InputState.Held;
                }
                else if (Mouse4Released())
                {
                    return InputState.Released;
                }
                else
                {
                    return InputState.Default;
                }
            }
            else
            {
                if (Mouse5Click())
                {
                    return InputState.Pressed;
                }
                else if (Mouse5Held())
                {
                    return InputState.Held;
                }
                else if (Mouse5Released())
                {
                    return InputState.Released;
                }
                else
                {
                    return InputState.Default;
                }
            }
        }

        //MOUSE-POSITION

        /// <summary>
        /// Returns the mouse position as a Vector2.
        /// </summary>
        public Vector2 MousePosV()
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Returns the mouse position as a Point.
        /// </summary>
        public Point MousePosP()
        {
            return new Point(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Returns only the x position of the mouse.
        /// </summary>
        public int MouseX()
        {
            return mouseState.X;
        }

        /// <summary>
        /// Returns only the y position of the mouse.
        /// </summary>
        public int MouseY()
        {
            return mouseState.Y;
        }

        /// <summary>
        /// Returns the difference in X value over the last update. The value is negative if moving left, positive if right.
        /// </summary>
        public int MouseXDiff()
        {
            return mouseState.X - oldMouseState.X;
        }

        /// <summary>
        /// Returns the difference in Y value over the last update. The value is negative if moving up, positive if down.
        /// </summary>
        public int MouseYDiff()
        {
            return mouseState.Y - oldMouseState.Y;
        }

        //MOUSE-SCROLL

        /// <summary>
        /// Returns a positive value if the wheel has been scrolled up, a negative for down and zero for no change.
        /// </summary>
        public int ScrollVal()
        {
            return (oldMouseState.ScrollWheelValue - mouseState.ScrollWheelValue);
        }

        #endregion

        #region Private Mouse Funtions

        //Left

        /// <summary>
        /// Checks to see if the left button has been clicked this update cycle.
        /// </summary>
        bool LeftClick()
        {
            return (mouseState.LeftButton == ButtonState.Pressed &&
                oldMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Checks to see if the left button has remained held this update cycle.
        /// </summary>
        bool LeftHeld()
        {
            return (mouseState.LeftButton == ButtonState.Pressed &&
                oldMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the left button has been clicked this update cycle.
        /// </summary>
        bool LeftReleased()
        {
            return (mouseState.LeftButton == ButtonState.Released &&
                oldMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the left button has remained untouched this update cycle.
        /// </summary>
        bool LeftDefault()
        {
            return (mouseState.LeftButton == ButtonState.Released &&
                oldMouseState.LeftButton == ButtonState.Released);
        }

        //Right

        /// <summary>
        /// Checks to see if the right button has been pressed this update cycle.
        /// </summary>
        bool RightClick()
        {
            return (mouseState.RightButton == ButtonState.Pressed &&
                oldMouseState.RightButton == ButtonState.Released);
        }

        /// <summary>
        /// Checks to see if the right button has remained held this update cycle.
        /// </summary>
        bool RightHeld()
        {
            return (mouseState.RightButton == ButtonState.Pressed &&
                oldMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the right button has been released this update cycle.
        /// </summary>
        bool RightReleased()
        {
            return (mouseState.RightButton == ButtonState.Released &&
                oldMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the right button has remained untouched this update cycle.
        /// </summary>
        bool RightDefault()
        {
            return (mouseState.RightButton == ButtonState.Released &&
                oldMouseState.RightButton == ButtonState.Released);
        }

        //Middle

        /// <summary>
        /// Checks to see if the middle button has been clicked this update cycle.
        /// </summary>
        bool MiddleClick()
        {
            return (mouseState.MiddleButton == ButtonState.Pressed &&
                oldMouseState.MiddleButton == ButtonState.Released);
        }

        /// <summary>
        /// Checks to see if the middle button has remained held this update cycle.
        /// </summary>
        bool MiddleHeld()
        {
            return (mouseState.MiddleButton == ButtonState.Pressed &&
                oldMouseState.MiddleButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the middle button has been clicked this update cycle.
        /// </summary>
        bool MiddleReleased()
        {
            return (mouseState.MiddleButton == ButtonState.Released &&
                oldMouseState.MiddleButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if the middle button has remained untouched this update cycle.
        /// </summary>
        bool MiddleDefault()
        {
            return (mouseState.MiddleButton == ButtonState.Released &&
                oldMouseState.MiddleButton == ButtonState.Released);
        }

        //Mouse4

        /// <summary>
        /// Checks to see if mouse 4 has been pressed this update cycle.
        /// </summary>
        bool Mouse4Click()
        {
            return (mouseState.XButton1 == ButtonState.Pressed &&
                oldMouseState.XButton1 == ButtonState.Released);
        }

        /// <summary>
        /// Checks to see if mouse 4 has remained held this update cycle.
        /// </summary>
        bool Mouse4Held()
        {
            return (mouseState.XButton1 == ButtonState.Pressed &&
                oldMouseState.XButton1 == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if mouse 4 has been released this update cycle.
        /// </summary>
        bool Mouse4Released()
        {
            return (mouseState.XButton1 == ButtonState.Released &&
                oldMouseState.XButton1 == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if mouse 4 has remained untouched this update cycle.
        /// </summary>
        bool Mouse4Default()
        {
            return (mouseState.XButton1 == ButtonState.Released &&
                oldMouseState.XButton1 == ButtonState.Released);
        }

        //Mouse5

        /// <summary>
        /// Checks to see if mouse 5 has been clicked this update cycle.
        /// </summary>
        bool Mouse5Click()
        {
            return (mouseState.XButton2 == ButtonState.Pressed &&
                oldMouseState.XButton2 == ButtonState.Released);
        }

        /// <summary>
        /// Checks to see if mouse 5 has remained held this update cycle.
        /// </summary>
        bool Mouse5Held()
        {
            return (mouseState.XButton2 == ButtonState.Pressed &&
                oldMouseState.XButton2 == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if mouse 5 has been clicked this update cycle.
        /// </summary>
        bool Mouse5Released()
        {
            return (mouseState.XButton2 == ButtonState.Released &&
                oldMouseState.XButton2 == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks to see if mouse 5 has remained untouched this update cycle.
        /// </summary>
        bool Mouse5Default()
        {
            return (mouseState.XButton2 == ButtonState.Released &&
                oldMouseState.XButton2 == ButtonState.Released);
        }
        #endregion

        #region Private Keyboard Functions

        /// <summary>
        /// Checks to see if the key has been pressed this update cycle.
        /// </summary>
        /// <param name="key">The key to check.</param>
        bool IsKeyPressed(Keys key)
        {
            return (oldKeyState.IsKeyUp(key) && keyState.IsKeyDown(key));
        }

        /// <summary>
        /// Checks to see if the key has been released this update cycle.
        /// </summary>
        /// <param name="key">The key to check.</param>
        bool IsKeyReleased(Keys key)
        {
            return (oldKeyState.IsKeyDown(key) && keyState.IsKeyUp(key));
        }

        /// <summary>
        /// Checks to see if the key has remained held this update cycle.
        /// </summary>
        /// <param name="key">The key to check.</param>
        bool IsKeyHeld(Keys key)
        {
            return (oldKeyState.IsKeyDown(key) && keyState.IsKeyDown(key));
        }

        /// <summary>
        /// Checks to see if the key has remained untouched this update cycle.
        /// </summary>
        /// <param name="key">The key to check.</param>
        bool IsKeyDefault(Keys key)
        {
            return (oldKeyState.IsKeyUp(key) && keyState.IsKeyUp(key));
        }
        #endregion
    }
}
