using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace sortingProject
{
    static class Program
    {
        /*
         * Description:  Start point
         * Author: Jakub'Digitalrasta'Bujny
         * Version: 0.0.0
         * Changelog:
        */
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) //GUI MODE
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            else //TESTING MODE
            {
                Testing testObject = new Testing("output.txt");
                try
                {
                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.cs);
                    
                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.bubble, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.bubble, Executor.Lib.cs);

                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.insert, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(5000, 6000, 20, 4, Testing.DataType.random, Executor.Method.insert, Executor.Lib.cs);
                }
                catch (Exception e)
                {
                    System.IO.File.WriteAllText("error.txt", e.ToString());
                }
            }
            
        }
    }
}
