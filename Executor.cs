using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        Object lockingObject = new Object();

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
            int tasksLength = 0;
            if (availableThreadsCounter > tasksCount)
            {
                tasksLength = tasksCount;
            }
            else
            {
                tasksLength = availableThreadsCounter;
            }
            Thread[] threadsArray = new Thread[tasksLength];
            for (int i = 0; i < tasksLength; i++)
            {
                threadsArray[i] = new Thread(new ThreadStart(startSorting));
                threadsArray[i].Start();
            }
            for (int i = 0; i < tasksLength; i++)
            {
                threadsArray[i].Join();
            }
        }

        private unsafe void startSorting()
        {
            IntPtr packedPointer;
            int length = 0;
            while (true)
            {
                lock (lockingObject)
                {
                    if (tasksAlreadyComputed < tasksCount)
                    {
                        length = dataArray[(int)tasksAlreadyComputed].Length;
                        fixed (int* ptr = dataArray[(int)tasksAlreadyComputed])
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
                sortingMethod.Invoke(sortingObject, new object[] { packedPointer, length });
            }
        }

        public unsafe bool debug_executeAndCompareResult(Executor.Method method)
        {
            int arraySize = 100000;
            int[] asmArray = new int[arraySize];
            int[] csArray = new int[arraySize];
            int randomSeed = new Random().Next();
            //int randomSeed = 0;
            Random randomGenerator = new Random(randomSeed);
            for (int i = 0; i < asmArray.Length; i++)
            {
                int randomVal = randomGenerator.Next();
                //int randomVal = 100000-i;
                asmArray[i] = randomVal;
                csArray[i] = randomVal;
            }
            MethodInfo csMethod = sortingObject.GetType().GetMethod("cs_" + method);
            MethodInfo asmMethod = sortingObject.GetType().GetMethod("asm_" + method);
            var watch = Stopwatch.StartNew();
            fixed (int* ptr = csArray)
            {
                IntPtr packedPointer = new IntPtr(ptr);
                csMethod.Invoke(sortingObject, new object[] { packedPointer, csArray.Length });
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(method + " cs time: " + elapsedMs);

            watch = Stopwatch.StartNew();
            fixed (int* ptr = asmArray)
            {
                IntPtr packedPointer = new IntPtr(ptr);
                asmMethod.Invoke(sortingObject, new object[] { packedPointer, asmArray.Length });
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(method + " asm time: " + elapsedMs);

            for (int i = 0; i < asmArray.Length; i++)
            {
                if (asmArray[i] != csArray[i])
                {
                    throw new Exception("Implementation error. Random seed: " + randomSeed);
                }
            }
            return true;
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
