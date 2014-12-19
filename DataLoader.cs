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
    * Description:  Class which are loading and processing input data from user.
    * Author: Jakub'Digitalrasta'Bujny
    * Version: 0.3.0
    * Changelog:
    *      0.0.0: added parsing and loading method
    *      0.1.0: added save to file method
    *      0.2.0: added debug method for generating random data
     *     0.2.1: fixed problem in saving to file (not closing file)
     *     0.3.0: removed debug method
    */
    class DataLoader
    {
        //Path to file with input data
        private String filePath;

        /*
         * Description: standard setting constructor
         * Arguments:
         * filePath - path to file to load data
         */
        public DataLoader(String filePath)
        {
            this.filePath = filePath;
        }

        /*
        * Description: Parsing input file and pack it into 2d int array
        * Arguments:
        * separator - sign which is separating numbers in each line
        * Return: parsed input data
        */
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

        /*
        * Description: Saving 2D array to file and append mode
        * Arguments:
        * dataToSave - 2D array to save
        * nameOfFile - name of file to save
        */
        public void saveToFile(int[][] dataToSave, String nameOfFile)
        {
            if (nameOfFile.Equals(""))
            {
                return;
            }
            StreamWriter writeToFile = new StreamWriter(nameOfFile, true);
            for (int i = 0; i < dataToSave.Length; i++)
            {
                String lineToWrite = "";
                for (int j = 0; j < dataToSave[i].Length; j++)
                {
                    lineToWrite += dataToSave[i][j].ToString();
                    if (j == dataToSave[i].Length-1) {
                        continue;
                    }
                    lineToWrite += ";";
                }
                writeToFile.WriteLine(lineToWrite);
            }
            writeToFile.Close();
        }
    }
}
