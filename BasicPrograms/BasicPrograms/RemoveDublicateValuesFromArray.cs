using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class RemoveDublicateValuesFromArray
    {
        public int[] DeleteDublicateValues(int[] inputArray)
        {
            if(inputArray == null || inputArray.Length == 0)
            {
                return inputArray;
            }
            HashSet<int> uniqueItems = new HashSet<int>();
            foreach(int item in inputArray)
            {
                uniqueItems.Add(item);
            }
            return uniqueItems.ToArray();
        }
    }
}
