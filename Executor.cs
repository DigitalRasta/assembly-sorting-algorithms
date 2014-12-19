using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sortingProject
{

    /*
    * Description:  Class with program logic. Process data from user input, creates threads, manage them
    * and execute computing functions from libs
    * Author: Jakub'Digitalrasta'Bujny
    * Version: 0.5.0
    * Changelog:
    *      0.0.0: added basic method for start computing
    *      0.0.1: fixed problem with thread barrier
    *      0.2.0: reflection concept of invoking method
     *     0.3.0: added debug method for testing
     *     0.4.0: added destructor and memory pinning
     *     0.5.0: removed debug method
    */
    class Executor
    {
        //Array with data from user
        volatile  int[][] dataArray;

        //Handler for pinning memory
        GCHandle[] inputArrayMemoryHandle;
        //available threads to use
        volatile int availableThreadsCounter;
        //tasks left to complete computing
        volatile int tasksCount;
        //Number of tasks already computed
        volatile int tasksAlreadyComputed = 0;
        //Iterating on dataArray
        volatile int dataArrayIterator = 0;
        //Object for threads synchro
        volatile Object lockingObject = new Object();
        //Method to use
        volatile MethodInfo sortingMethod;
        //Object containing sortingMethod
        volatile Sorting sortingObject;
        //sorting method
        Method methodType;

        /*
        * Description: Standard constructor which is pinning memory and set params
        * Arguments:
        * inputArray - array with input data
        * numberOfThreads - number of computing threads
        * library - library: c#/ASM
        * method - bubble/insert/quick
        */
        public unsafe Executor(int[][] inputArray, int numberOfThreads, Lib library, Method method)
        {
            this.dataArray = inputArray;
            inputArrayMemoryHandle = new GCHandle[this.dataArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                inputArrayMemoryHandle[i] = GCHandle.Alloc(this.dataArray[i], GCHandleType.Pinned);
            }
                
            this.availableThreadsCounter = numberOfThreads;
            this.tasksCount = inputArray.Length;
            sortingObject = new Sorting();
            sortingMethod = sortingObject.GetType().GetMethod(library + "_" + method);
            methodType = method;
        }

        /*
        * Description: Destructor. Free memory
        */
        ~Executor() {
            for (int i = 0; i < this.dataArray.Length; i++)
            {
                inputArrayMemoryHandle[i].Free();
            }
        }

        /*
        * Description: Start computing. Creates threads and divide work.
        */
        public void start()
        {
            int tasksLength = 0;
            
            if (availableThreadsCounter > tasksCount)
            {
                //We've got more threads than tasks
                tasksLength = tasksCount;
            }
            else
            {
                tasksLength = availableThreadsCounter;
            }
            //divide work to threads
            Thread[] threadsArray = new Thread[tasksLength];
            for (int i = 0; i < tasksLength; i++)
            {
                if (methodType == Method.quick)
                {
                    threadsArray[i] = new Thread(new ThreadStart(startSorting), 52428800);
                }
                else
                {
                    threadsArray[i] = new Thread(new ThreadStart(startSorting));
                }
                threadsArray[i].Start();
            }
            for (int i = 0; i < tasksLength; i++)
            {
                threadsArray[i].Join();
            }
        }

        /*
        * Description: Method used in thread creating. Creates locks 
        */
        private unsafe void startSorting()
        {
            //pointer to place in dataArray
            IntPtr packedPointer;
            int length = 0;
            while (true)
            {
                lock (lockingObject)
                {
                    if (tasksAlreadyComputed < tasksCount)
                    {
                        //We've got more work. Get row from array.
                        length = dataArray[tasksAlreadyComputed].Length;
                        fixed (int* ptr = dataArray[tasksAlreadyComputed])
                        {
                            packedPointer = new IntPtr(ptr);
                        }
                    }
                    else
                    {
                        break;
                    }
                    tasksAlreadyComputed++;
                }
                //start computing
                sortingMethod.Invoke(sortingObject, new object[] { packedPointer, length });
            }
        }

        public enum Lib
        {
            cs,
            asm
        }

        public enum Method
        {
            bubble,
            insert,
            quick
        }
    }
}
