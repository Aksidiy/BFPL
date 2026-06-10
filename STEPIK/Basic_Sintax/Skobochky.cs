using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Sintax
{
    internal class Skobochky
    {
        static bool IsSymetricSkobochki(string input)
        {
            //((()()()()))((()()()())) ([]){()<>}[{}[]]<[()]> ([{< >}])
            List<char> openBraks = ['(', '[', '{', '<'];
            List<char> closeBraks = [')', ']', '}', '>'];

            if (input == null) input = "";
            Stack<char> chars = new Stack<char>();

            foreach (char s in input)
            {
                if (openBraks.Contains(s)) chars.Push(s);
                else if (closeBraks.Contains(s))
                {
                    if (chars.Count == 0)
                    { chars.Push(s); break; }
                    else if (chars.Peek() == (char)(s - 1) || chars.Peek() == (char)(s - 2))
                    { chars.Pop(); }
                    else if (chars.Peek() != (char)(s - 1) || chars.Peek() != (char)(s - 2))
                    { break; }
                }
            }
            if (chars.Count == 0) return true;
            else return false;
        }
    }
}
