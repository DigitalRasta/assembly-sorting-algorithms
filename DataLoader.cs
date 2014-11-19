using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sortingProject
{
    /*
     * Class which are loading and processing input data from user.
     * Author: Jakub'Digitalrasta'Bujny
     * Version: 0.0.0
     * Created: 22.10.2014
     * Changelog:
     */
    class DataLoader
    {
        //Path to file with input data
        private String filePath;
        public DataLoader(String filePath)
        {
            this.filePath = filePath;
        }

         //Parsing input file and pack it into 2d int array
         //separator: sign which is separating numbers in each line
         //Return: parsed input data
        public int[][] parseAndLoad(char separator)
        {
            ArrayList parsedLines = new ArrayList(); 
            try
            {
                using (TextReader reader = File.OpenText(filePath))
                {
                    String line;
                    //read and parse each line
                    while((line = reader.ReadLine()) != null) {
                        String[] stringElements = line.Split(separator);
                        int[] intElements = new int[stringElements.Length]; 
                        for (int i = 0; i < stringElements.Length; i++)
                        {
                            intElements[i] = int.Parse(stringElements[i]);
                        }
                        parsedLines.Add(intElements);
                    }
                    //convert ArrayList into normal array
                    int[][] toReturn = new int[parsedLines.Count][];
                    for (int i = 0; i < parsedLines.Count; i++)
                    {
                        toReturn[i] = (int[])parsedLines[i];
                    }
                    return toReturn;
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                throw new Exceptions.ExceptionInfoToGUI("You don't have permission to input file.");
            }
            catch (System.FormatException ex)
            {
                throw new Exceptions.ExceptionInfoToGUI("Wrong data format in file.");
            }
            catch (Exception ex)
            {
                throw new Exceptions.ExceptionInfoToGUI("Problem with input file open.");
            }
        }

         // Method used in software testing. Generates random test data.
         // minSize: min size of input data
         // maxSize: max size of input data
         // minBlockSize: min size of input in one block
         // maxBlockSize: max size of input in one block
         // minVal: min val to sorting
         // maxVal: max val to sorting 
        public static int[][] debug_generateRandomTestData(int minSize, int maxSize, int minBlockSize, int maxBlockSize, int minVal, int maxVal)
        {
            Random randomGenerator = new Random();
            int globalSize = randomGenerator.Next(minSize, maxSize);
            int[][] toReturn = new int[globalSize][];
            for (int i = 0; i < toReturn.Length; i++)
            {
                int innerSize = randomGenerator.Next(minBlockSize, maxBlockSize);
                toReturn[i] = new int[innerSize];
                for (int j = 0; j < toReturn[i].Length; j++)
                {
                    toReturn[i][j] = randomGenerator.Next(minVal, maxVal);
                }
            }
            return toReturn;
        }
    }
}
