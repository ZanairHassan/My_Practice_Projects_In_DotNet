using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class LongestWord
    {
        public string LongestWordInSentense(string sentense)
        {
            if(string.IsNullOrEmpty(sentense))
                return String.Empty;
            string[] words = sentense.Split(new string[] {" ",",",".","|","?","!","/" }, StringSplitOptions.RemoveEmptyEntries);
            string longestWord=words.OrderByDescending(word => word.Length).FirstOrDefault();
            return longestWord;
        }
    }
}
