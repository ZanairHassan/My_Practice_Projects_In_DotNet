using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class FindItemWithIndex
    {
        public int ItemAtGivenIndex(int[] array, int index)
        {
            if(index>=0 && index<array.Length)
            {
                int itemValue= array[index];
                return itemValue;
            }
            else
            {
                return -1;
            }
        }
    }
}
