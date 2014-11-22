using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Start point of application
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */

namespace sortingProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            else
            {
                Testing testObject = new Testing("output.txt");
                try
                {
                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.bubble, Executor.Lib.asm);
                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.bubble, Executor.Lib.cs);

                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.insert, Executor.Lib.asm);
                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.insert, Executor.Lib.cs);

                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.quick, Executor.Lib.asm);
                    testObject.testCase_changeInputArraySizeAndNumberOfThread(100, 300, 2000, 2, 64, Testing.DataType.reverseSorted, Executor.Method.quick, Executor.Lib.cs);
                
                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.insert, Executor.Lib.asm);

                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.insert, Executor.Lib.asm);

                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.asm);
                    testObject.testCase_changeBlockSize(200, 30000, 50, 4, Testing.DataType.random, Executor.Method.insert, Executor.Lib.asm);
                }
                catch (Exception e)
                {
                    System.IO.File.WriteAllText("error.txt", e.ToString());
                }
                //testObject.testCase_changeBlockSize(200, 21000, 50, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.asm);
                //testObject.testCase_changeBlockSize(200, 21000, 50, 4, Testing.DataType.random, Executor.Method.quick, Executor.Lib.cs);
            }
            
        }
    }
}
