using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FredLib.Utilities
{
    public static class StringManip
    {
        public static string AddCharAtPos(string str, char c, int p)
        {
            if (p <= str.Length)
            {
                string first, second;
                first = str.Substring(0, p);
                second = str.Substring(p);
                str = first + c + second;
            }
            else
            {
                str += c;
            }

            return str;
        }

        public static string RemoveCharAtPos(string str, int p)
        {
            if (str.Length != 0)
            {
                if (p > 0 && p <= str.Length)
                {
                    string first, second;
                    first = str.Substring(0, p - 1);
                    second = str.Substring(p);
                    str = first + second;
                }
                else if (p == 0)
                {
                    str = str.Substring(p + 1);
                }
                else
                {
                    str = str.Remove(str.Length - 1);
                }
            }

            return str;
        }
    }
}
