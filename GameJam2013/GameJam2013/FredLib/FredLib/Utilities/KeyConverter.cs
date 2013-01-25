using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FredLib.Utilities
{
    /// <summary>
    /// Converts from Keys enumeration
    /// to ASCII value.
    /// </summary>
    public static class KeyConverter
    {
        public static int ConvertToAscii(Keys key)
        {
            switch (key)
            {
                case Keys.OemBackslash:
                    return (int)'\\';

                case Keys.OemCloseBrackets:
                    return (int)']';

                case Keys.OemComma:
                    return (int)',';

                case Keys.OemMinus:
                    return (int)'-';

                case Keys.OemOpenBrackets:
                    return (int)'[';

                case Keys.OemPeriod:
                    return (int)'.';

                case Keys.OemPipe:
                    return (int)'|';

                case Keys.OemPlus:
                    return (int)'+';

                case Keys.OemQuestion:
                    return (int)'?';

                case Keys.OemQuotes:
                    return (int)'\'';

                case Keys.OemSemicolon:
                    return (int)';';

                case Keys.OemTilde:
                    return (int)'`';

                default:
                    return (int)key;
            }
        }

        public static int ConvertToAscii(Keys key, bool shift)
        {
            if (shift)
            {
                switch (key)
                {
                    case Keys.OemBackslash:
                        return (int)'\\';

                    case Keys.OemCloseBrackets:
                        return (int)'}';

                    case Keys.OemComma:
                        return (int)'<';

                    case Keys.OemMinus:
                        return (int)'_';

                    case Keys.OemOpenBrackets:
                        return (int)'{';

                    case Keys.OemPeriod:
                        return (int)'>';

                    case Keys.OemPipe:
                        return (int)'|';

                    case Keys.OemPlus:
                        return (int)'+';

                    case Keys.OemQuestion:
                        return (int)'?';

                    case Keys.OemQuotes:
                        return (int)'"';

                    case Keys.OemSemicolon:
                        return (int)':';

                    case Keys.OemTilde:
                        return (int)'~';

                    case Keys.D1:
                        return (int)'!';

                    case Keys.D2:
                        return (int)'@';

                    case Keys.D3:
                        return (int)'#';

                    case Keys.D4:
                        return (int)'$';

                    case Keys.D5:
                        return (int)'%';

                    case Keys.D6:
                        return (int)'^';

                    case Keys.D7:
                        return (int)'&';

                    case Keys.D8:
                        return (int)'*';

                    case Keys.D9:
                        return (int)'(';

                    case Keys.D0:
                        return (int)')';

                    default:
                        return (int)key;
                }
            }
            else
            {
                switch (key)
                {
                    case Keys.OemBackslash:
                        return (int)'\\';

                    case Keys.OemCloseBrackets:
                        return (int)']';

                    case Keys.OemComma:
                        return (int)',';

                    case Keys.OemMinus:
                        return (int)'-';

                    case Keys.OemOpenBrackets:
                        return (int)'[';

                    case Keys.OemPeriod:
                        return (int)'.';

                    case Keys.OemPipe:
                        return (int)'\\';

                    case Keys.OemPlus:
                        return (int)'=';

                    case Keys.OemQuestion:
                        return (int)'/';

                    case Keys.OemQuotes:
                        return (int)'\'';

                    case Keys.OemSemicolon:
                        return (int)';';

                    case Keys.OemTilde:
                        return (int)'`';

                    default:
                        return (int)key;
                }
            }
        }

        public static bool IsTypeableKey(Keys key)
        {
            int keyInt = (int)key;
            if ((keyInt == 32) ||
                (keyInt >= 48 && keyInt <= 90) ||
                (keyInt >= 186 && keyInt <= 192) ||
                (keyInt >= 219 && keyInt <= 226))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
