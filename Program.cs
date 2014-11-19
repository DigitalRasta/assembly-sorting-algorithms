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
                testObject.testCase_changeInputArraySizeAndNumberOfThread(10, 1000, 1000, 2, 128, Testing.DataType.random, Executor.Method.bubble, Executor.Lib.asm);
            }
            
        }
    }
}
