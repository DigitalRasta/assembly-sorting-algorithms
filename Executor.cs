﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sortingProject
{
    /*
    * Class with program logic. Process data from user input, creates threads, manage them
    * and execute computing functions from libs
    * Author: Jakub'Digitalrasta'Bujny
    * Version: 0.0.0
    * Created: 22.10.2014
    * Changelog:
    */
    class Executor
    {
        //Array with data from user
        int[][] dataArray;
        //available threads to use
        int availableThreadsCounter;
        //tasks left to complete computing
        int tasksCount;
        //Number of tasks already computed
        int tasksAlreadyComputed = 0;
        //Iterating on dataArray
        volatile int dataArrayIterator = 0;
        //Object for threads synchro
        Object lockingObject = new Object();
        //Method to use
        MethodInfo sortingMethod;
        //Object containing sortingMethod
        Sorting sortingObject;

        //Standard constructor.
        //numberOfThreads: to use in computing
        //library: C# or ASM
        //method: bubble/insert/quick
        public Executor(int[][] inputArray, int numberOfThreads, Lib library, Method method)
        {
            this.dataArray = inputArray;
            this.availableThreadsCounter = numberOfThreads;
            this.tasksCount = inputArray.Length;
            sortingObject = new Sorting();
            sortingMethod = sortingObject.GetType().GetMethod(library + "_" + method);
        }

        //Start computing. Creates threads and divide work.
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
                threadsArray[i] = new Thread(new ThreadStart(startSorting));
                threadsArray[i].Start();
            }
            for (int i = 0; i < tasksLength; i++)
            {
                threadsArray[i].Join();
            }
        }

        //Method used in thread creating. Creates locks  
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
                //start computing
                sortingMethod.Invoke(sortingObject, new object[] { packedPointer, length });
            }
        }

        public unsafe bool debug_executeAndCompareResult(Executor.Method method)
        {
            int arraySize = 10;
            int[] asmArray = new int[arraySize];
            int[] csArray = new int[arraySize];
            int randomSeed = new Random().Next();
            //int randomSeed = 0;
            Random randomGenerator = new Random(randomSeed);
            for (int i = 0; i < asmArray.Length; i++)
            {
                int randomVal = randomGenerator.Next();
                //int randomVal = 10-i;
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
