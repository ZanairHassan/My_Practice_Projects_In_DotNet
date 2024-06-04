using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class StringReversed
    {
        public string ReversedString(string input)
        {
            if (string.IsNullOrEmpty(input))
            { 
                return input;
            }
            char[] charArray= input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
