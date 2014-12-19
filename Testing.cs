using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingProject
{

    /*
   * Description: Class testing performance and correctness of alghorithms 
   * Author: Jakub'Digitalrasta'Bujny
   * Version: 0.2.1
   * Changelog:
   *      0.0.0: basic correctness testing
   *      0.1.0: added data generator
   *      0.2.0: added enum with data type
   *      0.3.0: added saving to file
   *      0.3.1: fixed bug while saving to file
   *      0.4.0: added testing method: change input array size
   *      0.5.0: added testing method: change block size   
   */
    class Testing
    {
        //Path to output file
        String outputFilePath;
        //storing data
        volatile int[][] dataArray;
        //save to file stream
        StreamWriter fileToWrite;
        /*
        * Description: standard setting constructor
        */
        public Testing(String outputFilePath)
        {
            this.outputFilePath = outputFilePath;
        }

        /*
        * Description: performance and correctness testing. Change block size
        * Arguments:
        * sizeFrom - start point for block size
        * sizeTo - end point for block size
        * dataSize - rows count in array
        * threadsNum - number of used threads
        * type - type of data
        * method - which method
        * lib - which lib
        * Return: false - fail
        */
        public bool testCase_changeBlockSize(int sizeFrom, int sizeTo, int dataSize,
            int threadsNum, DataType type, Executor.Method method, Executor.Lib lib)
        {
            try
            {
                openFileAndAddHeader("Change block size.", "lib: " + lib + " method: " + method + " sizeFrom: " + sizeFrom + " sizeTo: " + sizeTo +
                    " threadsNum: " + threadsNum + " data type: " + type + " data size: " + dataSize, "size;time");
            }
            catch (Exception e)
            {
                return false;
            }
            for (int i = sizeFrom; i < sizeTo; i += 1000)
            {
                long result = 0;
                try
                {
                    long avr = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        dataArray = generateData(dataSize, i, type);
                        int[][] sortedArray = createSorted(dataArray);
                        avr += testMethod(dataArray, sortedArray, i, lib, method);
                    }
                    result = avr / 5;
                }
                catch (Exceptions.ExceptionArrayComparison e)
                {
                    fileToWrite.WriteLine("Comparison error!!");
                    fileToWrite.Flush();
                    fileToWrite.Close();
                    return false;
                }
                fileToWrite.WriteLine(i + ";" + result);
                fileToWrite.Flush();
            }
            fileToWrite.Flush();
            fileToWrite.Close();
            return true;
        }

        /*
        * Description: performance and correctness testing. Change rows count.
        * Arguments:
        * sizeFrom - start point for rows count
        * sizeTo - end point for rows count
        * blockSize - block size in row
        * threadsNum - number of used threads
        * type - type of data
        * method - which method
        * lib - which lib
        * Return: false - fail
        */
        public bool testCase_changeInputArraySizeAndNumberOfThread(int sizeFrom, int sizeTo, int blockSize,
            int threadsFrom, int threadsTo, DataType type, Executor.Method method, Executor.Lib lib)
        {
            try
            {
                openFileAndAddHeader("Change input array size and num of threads.", "lib: " + lib + " method: " + method + " sizeFrom: " + sizeFrom + " sizeTo: " + sizeTo +
                    " threadsFrom: " + threadsFrom + " threadsTo: " + threadsTo + " data type: " + type + " block size: " + blockSize, "size;time");
            }
            catch (Exception e)
            {
                return false;
            }

            for (int i = threadsFrom; i < threadsTo + 1; i = i * 2)
            {
                fileToWrite.WriteLine("---------------");
                fileToWrite.WriteLine("Threads count: " + i);
                fileToWrite.WriteLine("---------------");
                for (int j = sizeFrom; j < sizeTo; j++)
                {
                    long result = 0;
                    try
                    {
                        long avr = 0;
                        for (int k = 0; k < 5; k++)
                        {
                            dataArray = generateData(j, blockSize, type);
                            int[][] sortedArray = createSorted(dataArray);
                            avr += testMethod(dataArray, sortedArray, i, lib, method);
                        }
                        result = avr / 5;
                    }
                    catch (Exceptions.ExceptionArrayComparison e)
                    {
                        fileToWrite.WriteLine("Comparison error!!");
                        fileToWrite.Flush();
                        fileToWrite.Close();
                        return false;
                    }
                    fileToWrite.WriteLine(j + ";" + result);
                    fileToWrite.Flush();
                }
            }
            fileToWrite.Flush();
            fileToWrite.Close();
            return true;
        }

        /*
        * Description: one test with correctness check
        * Arguments:
        * inputArray - array with input data
        * sortedArrayToCheck - array for correctness check
        * numberOfThreads - number of used threads
        * method - which method
        * lib - which lib
        * Return: computing time in ms
        */
        private long testMethod(int[][] inputArray, int[][] sortedArrayToCheck,
            int numberOfThreads, Executor.Lib lib, Executor.Method method)
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

        /*
        * Description: open file and add test header to it
        * Arguments:
        * testType - name of testing method
        * testParameters - testing conditions
        * outputFormat - information about output format
        */
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

        /*
        * Description: generate data for tests
        * Arguments:
        * rowCount - number of rows to generate
        * rowSize - length of row
        * type - data type
        * Return: generated array
        */
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

        /*
        * Description: sort array
        */ 
        private int[][] createSorted(int[][] inputData)
        {
            int[][] sortedArray = arrayCopy2d(inputData);
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Array.Sort(sortedArray[i]);
            }
            return sortedArray;
        }

        /*
        * Description: copy array
        */ 
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
