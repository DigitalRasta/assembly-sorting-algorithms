using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 * Class with program logic. Grab data from GUI and user input, creates threads, manage them
 * and execute computing functions from libs
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */
namespace sortingProject
{
    class Executor
    {
        int[][] dataArray;
        int availableThreadsCounter;
        int tasksCount;
        int tasksAlreadyComputed = 0;
        volatile int dataArrayIterator = 0;

        MethodInfo sortingMethod;
        Sorting sortingObject;


        public Executor(int[][] inputArray, int numberOfThreads)
        {
            this.dataArray = inputArray;
            this.availableThreadsCounter = numberOfThreads;
            this.tasksCount = inputArray.Length;
            sortingObject = new Sorting();
            sortingObject.cs_bubble("Kutas w cyc");
        }

        public void start()
        {
            while (tasksAlreadyComputed < tasksCount)
            {
                    if (availableThreadsCounter != 0 && dataArrayIterator < tasksCount)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(startSorting));
                        thread.Start(dataArrayIterator);
                        dataArrayIterator++;
                        availableThreadsCounter--;
                    }
                    else
                    {
                        Thread.Sleep(20);
                    }
            }
        }

        private unsafe void startSorting(object positionInInputArray)
        {
            int[] array = dataArray[(int)positionInInputArray];
            fixed (int* ptr = dataArray[(int)positionInInputArray])
            {
                testIt(ptr, array.Length);
            }
            operationsInThreadCompleted();
        }

        private unsafe void testIt(int* pointer, int length)
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
        private void operationsInThreadCompleted()
        {
            tasksAlreadyComputed++;
            availableThreadsCounter++;
        }
    }
}
