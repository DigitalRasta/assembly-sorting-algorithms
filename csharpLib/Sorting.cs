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
            for (int write = 0; write < length; write++)
            {
                for (int sort = 0; sort < length - 1; sort++)
                {
                    if (pointer[sort] > pointer[sort + 1])
                    {
                        temp = pointer[sort + 1];
                        pointer[sort + 1] = pointer[sort];
                        pointer[sort] = temp;
                    }
                }
            }
        }
    }
}
