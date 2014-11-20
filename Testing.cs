﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingProject
{
    class Testing
    {

        String outputFilePath;
        volatile int[][] dataArray;
        StreamWriter fileToWrite;
        public Testing(String outputFilePath)
        {
            this.outputFilePath = outputFilePath;
        }

        public void testAllMethodsWithTimes(int[][] inputArray, int[][] sortedArrayToCheck, int numberOfThreads)
        {
            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.asm, Executor.Method.bubble);
            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.cs, Executor.Method.bubble);

            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.asm, Executor.Method.insert);
            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.cs, Executor.Method.insert);

            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.asm, Executor.Method.quick);
            testMethod(inputArray, sortedArrayToCheck, numberOfThreads, Executor.Lib.cs, Executor.Method.quick);
        }


        public bool testCase_changeInputArraySizeAndNumberOfThread(int sizeFrom, int sizeTo, int blockSize, int threadsFrom, int threadsTo,
            DataType type, Executor.Method method, Executor.Lib lib) {
            try
            {
                openFileAndAddHeader("Change input array size and num of threads.", "sizeFrom: "+sizeFrom+" sizeTo: " + sizeTo +
                    " threadsFrom: " + threadsFrom + " threadsTo: " + threadsTo + " data type: " + type + " block size: " + blockSize, "size;time" );
            }
            catch (Exception e)
            {
                return false;
            }
            
            for (int i = threadsFrom; i < threadsTo; i++)
            {
                fileToWrite.WriteLine("---------------");
                fileToWrite.WriteLine("Threads number: " + i);
                fileToWrite.WriteLine("---------------");
                for (int j = sizeFrom; j < sizeTo; j++)
                {
                    dataArray = generateData(j, blockSize, type);
                    int[][] sortedArray = createSorted(dataArray);
                    long result = 0;
                    try
                    {
                        long avr = 0;
                        for (int k = 0; k < 5; k++)
                        {
                            avr += testMethod(dataArray, sortedArray, i, lib, method);
                        }
                        result = avr / 5;
                    } catch(Exceptions.ExceptionArrayComparison e)
                    {
                        fileToWrite.WriteLine("Comparison error!!");
                        fileToWrite.Flush();
                        fileToWrite.Close();
                        return false;
                    }
                    fileToWrite.WriteLine(j+";"+result);
                    fileToWrite.Flush();
                }
           }
            fileToWrite.Flush();
            fileToWrite.Close();
            return true;
        }

        private long testMethod(int[][] inputArray, int[][] sortedArrayToCheck, int numberOfThreads, Executor.Lib lib, Executor.Method method)
        {
            Executor testExec = new Executor(inputArray, numberOfThreads, lib, method);
            Stopwatch watch = Stopwatch.StartNew();
            testExec.start();
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = 0; j < inputArray[i].Length; j++)
                {
                    if (inputArray[i][j] != sortedArrayToCheck[i][j])
                    {
                        throw new Exceptions.ExceptionArrayComparison();
                    }
                }
            }
            return elapsedMs;
        }


        private void openFileAndAddHeader(String testType, String testParameters, String outputFormat)
        {
            fileToWrite = File.AppendText(outputFilePath);
            fileToWrite.WriteLine("------------------------------------------------------");
            fileToWrite.WriteLine(DateTime.Now.ToString(@"M/d/yyyy hh:mm:ss tt"));
            fileToWrite.WriteLine(testType);
            fileToWrite.WriteLine(testParameters);
            fileToWrite.WriteLine();
            fileToWrite.WriteLine(outputFormat);
        }

        private int[][] generateData(int rowCount, int rowSize, DataType type)
        {
            int[][] toReturn = new int[rowCount][];
            switch (type)
            {
                case DataType.sorted:
                    for (int i = 0; i < rowCount; i++)
                    {
                        toReturn[i] = new int[rowSize];
                        for (int j = 0; j < rowSize; j++)
                        {
                            toReturn[i][j] = j;
                        }
                    }
                        break;
                case DataType.reverseSorted:
                        for (int i = 0; i < rowCount; i++)
                        {
                            toReturn[i] = new int[rowSize];
                            for (int j = 0; j < rowSize; j++)
                            {
                                toReturn[i][j] = rowSize - j;
                            }
                        }
                    break;
                case DataType.random:
                    Random rand = new Random();
                    for (int i = 0; i < rowCount; i++)
                    {
                        toReturn[i] = new int[rowSize];
                        for (int j = 0; j < rowSize; j++)
                        {
                            int number = rand.Next();
                            toReturn[i][j] = number;
                        }
                    }
                    break;
            }
            return toReturn;
        }

        private int[][] createSorted(int[][] inputData)
        {
            int[][] sortedArray = arrayCopy2d(inputData);
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Array.Sort(sortedArray[i]);
            }
            return sortedArray;
        }

        private int[][] arrayCopy2d(int[][] source)
        {
            int[][] toReturn = new int[source.Length][];
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = new int[source[i].Length];
                Array.Copy(source[i], toReturn[i], source[i].Length);
            }
            return toReturn;
        }
        public enum DataType
        {
            random,
            sorted,
            reverseSorted
        }
    }
}
