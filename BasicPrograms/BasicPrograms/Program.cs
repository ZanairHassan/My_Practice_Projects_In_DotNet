using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //**************************************  Class Objects**************************************

            StringReversed objStringReversed = new StringReversed();
            LongestWord objLongestWord = new LongestWord();
            RemoveDublicateValuesFromArray objRemoveDublicateValuesFromArray = new RemoveDublicateValuesFromArray();
            SortArray objSortArray = new SortArray();
            FindItemWithIndex objFindItemWithIndex= new FindItemWithIndex();
            //Console.WriteLine("1: Reversed String \n2: Longest Word in sentense \n3: Remove Dublicate Values From Array  \n4: Sort the Given Array" +
            //    "\n5: Find the item with index\nEnter your choise:");

            //string inputnumber=Console.ReadLine();
            //int operation=int.Parse(inputnumber);

            //switch (operation)
            //{
            //    case 1:
            //        {
            //            //******************************  Reversed String
            //            Console.WriteLine("enter a dummy string");
            //            var input = Console.ReadLine();
            //            string reversedString = objStringReversed.ReversedString(input);
            //            Console.WriteLine($"The reversed string is : {reversedString}");
            //            break;
            //        }
            //        case 2:
            //        {
            //            //*****************************  Longest Word in sentense ****************************

            //            Console.WriteLine("Enter a dummy sentense");
            //            var inputSentese = Console.ReadLine();
            //            var longestWord = objLongestWord.LongestWordInSentense(inputSentese);
            //            Console.WriteLine($"the Longest Word is: {longestWord}");
            //            break ;
            //        }

            //    //***********************************  Remove Dublicate values  ******************************
            //    case 3:
            //        {

            //            int[] dummyArray = { 1, 2, 3, 4, 5, 6, 7, 8, 3, 4, 1, 5, 12, 4, 0, 1 };
            //            int[] uniqueArray=objRemoveDublicateValuesFromArray.DeleteDublicateValues(dummyArray);
            //            Console.WriteLine("Resulted array with unique values");
            //            foreach(int items in uniqueArray)
            //            {
            //                Console.Write(items+ " ,");
            //            }
            //            break;
            //        }
            //        case 4:
            //        {
            //     //********************************* Remove Dublicate values **********************************       
            //            int[] dummyArray = { 1, 2, 3, 4, 7, 1, 9, 2, 6, 7, 8, 30, 27, 21, 16 };
            //            Console.WriteLine("the entered dummy array");

            //            objSortArray.PrintArray(dummyArray);
            //            var unique = objRemoveDublicateValuesFromArray.DeleteDublicateValues(dummyArray);
            //            foreach (int items in unique)
            //            {
            //                Console.Write(items + ", ");
            //            }
            //            Console.WriteLine("\nthe sorted array ");
            //            objSortArray.ArraySort(unique);
            //            objSortArray.PrintArray(unique);
            //            break ;

            //        }
            //        case 5:
            //        {
            //            //************************ Find the item with index ************************************

            //            int[] arr = { 10, 20, 30, 40, 50 };
            //            Console.WriteLine("The given array:");
            //            objSortArray.PrintArray(arr);
            //            Console.Write("Enter the index of the item you want to find: ");
            //            string itemIndex = Console.ReadLine();
            //            var index = int.Parse(itemIndex);
            //            int itemValue = objFindItemWithIndex.ItemAtGivenIndex(arr, index);

            //            if (itemValue != -1)
            //            {
            //                Console.WriteLine($"Item at index {index}: {itemValue}");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Invalid index entered.");
            //            }
            //            break ;

            //        }

            //}

            while (true)
            {
                Console.WriteLine("1: Reversed String \n2: Longest Word in Sentence \n3: Remove Duplicate Values From Array  \n4: Sort the Given Array" +
                                  "\n5: Find the item with index\n6: Exit\nEnter your choice:");

                string inputNumber = Console.ReadLine();
                int operation;

                // Validate input to ensure it's a number
                if (!int.TryParse(inputNumber, out operation))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        {
                            //******************************  Reversed String
                            Console.WriteLine("Enter a dummy string:");
                            var input = Console.ReadLine();
                            string reversedString = objStringReversed.ReversedString(input);
                            Console.WriteLine($"The reversed string is: {reversedString}");
                            break;
                        }
                    case 2:
                        {
                            //*****************************  Longest Word in Sentence ****************************
                            Console.WriteLine("Enter a dummy sentence:");
                            var inputSentence = Console.ReadLine();
                            var longestWord = objLongestWord.LongestWordInSentense(inputSentence);
                            Console.WriteLine($"The longest word is: {longestWord}");
                            break;
                        }
                    case 3:
                        {
                            //***********************************  Remove Duplicate values  ******************************
                            int[] dummyArray = { 1, 2, 3, 4, 5, 6, 7, 8, 3, 4, 1, 5, 12, 4, 0, 1 };
                            int[] uniqueArray = objRemoveDublicateValuesFromArray.DeleteDublicateValues(dummyArray);
                            Console.WriteLine("Resulted array with unique values:");
                            foreach (int item in uniqueArray)
                            {
                                Console.Write(item + " ,");
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 4:
                        {
                            //********************************* Sort Array **********************************
                            int[] dummyArray = { 1, 2, 3, 4, 7, 1, 9, 2, 6, 7, 8, 30, 27, 21, 16 };
                            Console.WriteLine("The entered dummy array:");
                            objSortArray.PrintArray(dummyArray);
                            var unique = objRemoveDublicateValuesFromArray.DeleteDublicateValues(dummyArray);
                            foreach (int item in unique)
                            {
                                Console.Write(item + ", ");
                            }
                            Console.WriteLine("\nThe sorted array:");
                            objSortArray.ArraySort(unique);
                            objSortArray.PrintArray(unique);
                            break;
                        }
                    case 5:
                        {
                            //************************ Find the item with index ************************************
                            int[] arr = { 10, 20, 30, 40, 50 };
                            Console.WriteLine("The given array:");
                            objSortArray.PrintArray(arr);
                            Console.Write("Enter the index of the item you want to find: ");
                            string itemIndex = Console.ReadLine();
                            var index = int.Parse(itemIndex);
                            int itemValue = objFindItemWithIndex.ItemAtGivenIndex(arr, index);

                            if (itemValue != -1)
                            {
                                Console.WriteLine($"Item at index {index}: {itemValue}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid index entered.");
                            }
                            break;
                        }
                    case 6:
                        {
                            //************************ Exit ************************************
                            Console.WriteLine("Exiting the program.");
                            return; // Exit the Main method, thus ending the program
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                        }
                }

                Console.WriteLine(); // Add an empty line for better readability
            }
        }
    }
}
