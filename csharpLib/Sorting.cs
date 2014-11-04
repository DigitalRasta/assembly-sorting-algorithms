using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpLib
{
    public class Sorting
    {

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

        public unsafe void quick(int* pointer, int length)
        {
            quick_resursive(pointer, 0, length-1);
        }

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
