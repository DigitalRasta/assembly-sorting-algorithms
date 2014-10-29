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


        public Executor(int[][] inputArray, int numberOfThreads, Lib library, Method method)
        {
            this.dataArray = inputArray;
            this.availableThreadsCounter = numberOfThreads;
            this.tasksCount = inputArray.Length;
            sortingObject = new Sorting();
            sortingMethod = sortingObject.GetType().GetMethod(library + "_" + method);
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
                    IntPtr packedPointer = new IntPtr(ptr);
                    sortingMethod.Invoke(sortingObject, new object[] { packedPointer, dataArray[(int)positionInInputArray].Length });
            }
            operationsInThreadCompleted();
        }

        private void operationsInThreadCompleted()
        {
            tasksAlreadyComputed++;
            availableThreadsCounter++;
        }

        public unsafe void debug_executeAndCompareResult(Executor.Method method)
        {
            int[] asmArray = new int[500];
            int[] csArray = new int[500];
            int randomSeed = new Random().Next();
            Random randomGenerator = new Random(randomSeed);
            for (int i = 0; i < asmArray.Length; i++)
            {
                asmArray[i] = randomGenerator.Next();
                csArray[i] = randomGenerator.Next();
            }
            MethodInfo csMethod = sortingObject.GetType().GetMethod("cs_" + method);
            MethodInfo asmMethod = sortingObject.GetType().GetMethod("asm_" + method);
            fixed (int* ptr = asmArray)
            {
                IntPtr packedPointer = new IntPtr(ptr);
                asmMethod.Invoke(sortingObject, new object[] { packedPointer, asmArray.Length });
            }
            fixed (int* ptr = csArray)
            {
                IntPtr packedPointer = new IntPtr(ptr);
                csMethod.Invoke(sortingObject, new object[] { packedPointer, csArray.Length });
            }
            for (int i = 0; i < asmArray.Length; i++)
            {
                if (asmArray[i] != csArray[i])
                {
                    throw new Exception("Implementation error. Random seed: " + randomSeed);
                }
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
