using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Class which are loading and processing input data from user.
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */
namespace sortingProject
{
    class DataLoader
    {
        //Path to file with input data
        private String filePath;
        public DataLoader(String filePath)
        {
            this.filePath = filePath;
        }

        public int[][] parseAndLoad()
        {
            ArrayList result = new ArrayList(); 
            try
            {
                using (TextReader reader = File.OpenText(filePath))
                {
                    String line;
                    while((line = reader.ReadLine()) != null) {
                        String[] stringElements = line.Split(';');
                        int[] intElements = new int[stringElements.Length]; 
                        for (int i = 0; i < stringElements.Length; i++)
                        {
                            intElements[i] = int.Parse(stringElements[i]);
                        }
                        result.Add(intElements);
                    }
                    int[][] toReturn = new int[result.Count][];
                    for (int i = 0; i < result.Count; i++)
                    {
                        toReturn[i] = (int[])result[i];
                    }
                    return toReturn;
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                throw new ExceptionInfoToGUI("You don't have permission to input file.");
            }
            catch (System.FormatException ex)
            {
                throw new ExceptionInfoToGUI("Wrong data format in file.");
            }
            catch (Exception ex)
            {
                throw new ExceptionInfoToGUI("Problem with input file open.");
            }
        }
    }
}
