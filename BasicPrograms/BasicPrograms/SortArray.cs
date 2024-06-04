using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class SortArray
    {
        public void ArraySort(int[] array)
        {
            int size = array.Length;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - i-1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
        public void PrintArray(int[] array)
        {
            foreach(var items in array)
            {
                Console.Write(items + " ,");
            }
            Console.WriteLine();
        }
    }
}
        
    

