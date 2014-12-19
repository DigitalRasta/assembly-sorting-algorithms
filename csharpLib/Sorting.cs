using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpLib
{
    /*
     * Description: Csharp library containing sorting methods
     * Author: Jakub'Digitalrasta'Bujny
     * Version: 0.2.0
     * Changelog:
     *      0.0.0: added basic bubble sort implementation
     *      0.1.0: added basic insert sort implementation
     *      0.2.0: added basic quick sort implementation
     */
    public class Sorting
    {
        /*
         * Description: simple bubble sort implementation
         * Arguments:
         * pointer - pointer to array
         * length - length of array
         */
        public unsafe void bubble(int* pointer, int length)
        {
            int temp = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if (pointer[j] > pointer[j + 1])
                    {
                        temp = pointer[j + 1];
                        pointer[j + 1] = pointer[j];
                        pointer[j] = temp;
                    }
                }
            }
        }

        /*
         * Description: simple insert sort implementation
         * Arguments:
         * pointer - pointer to array
         * length - length of array
         */
        public unsafe void insert(int* pointer, int length)
        {
            int temp = 0;
            int j = 0;
            for (int i = 1; i < length; i++)
            {
                temp = pointer[i];
                j = i - 1;
                while ((temp < pointer[j]) && (j >= 0))
                {
                    pointer[j + 1] = pointer[j];
                    j = j - 1;
                }
                pointer[j + 1] = temp;
            }
        }

        /*
         * Description: method used in recusrive quick sort
         * Arguments:
         * pointer - pointer to array
         * length - length of array
         */
        public unsafe void quick(int* pointer, int length)
        {
            quick_resursive(pointer, 0, length-1);
        }

        /*
         * Description: simple recursive quick sort implementation
         * Arguments:
         * array - pointer to array
         * low - low index of array
         * high - high index of array
         */
        private unsafe void quick_resursive(int* array, int low, int high)
        { 
            if (low < high)
            {
                int temp = 0; 
                int divide = low;
                int i = low;
                int j = high;
                while (i < j)
                {
                    while ((array[i] <= array[divide]) && (i < high))
                    {
                        i++;
                    }

                    while (array[j] > array[divide])
                    {
                        j--;
                    }

                    if (i < j)
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }

                temp = array[divide];
                array[divide] = array[j];
                array[j] = temp;
                quick_resursive(array, low, j - 1);
                quick_resursive(array, j + 1, high);
            }
        }

    }
}
