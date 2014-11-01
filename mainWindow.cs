using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Events handler. 
 * Author: Jakub'Digitalrasta'Bujny
 * Version: 0.0.0
 * Created: 22.10.2014
 * Changelog:
 */

namespace sortingProject
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
                InitializeComponent();
                for (int i = 0; i < combo_numThreads.Items.Count; i++)
                {
                    //Set default selection in num threads combobox
                    if (Environment.ProcessorCount == int.Parse(combo_numThreads.Items[i].ToString()))
                    {
                        combo_numThreads.SelectedItem = combo_numThreads.Items[i];
                    }
                }
                combo_lib.SelectedIndex = 0;
                combo_method.SelectedIndex = 0;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_openFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Place input file path selected by user to textbox
                text_sourceFile.Text = openFileDialog.FileName;
            }
        }

        private void text_sourceFile_TextChanged(object sender, EventArgs e)
        {
            //show end of path in textbox
            text_sourceFile.SelectionStart = text_sourceFile.Text.Length;
        }

        private String vaildateUserInput() {
            //todo: method content
            return "OK";
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            /*String validation = vaildateUserInput();
            if (validation != "OK")
            {
                MessageBox.Show(validation);
                return;
            }
            DataLoader loader = new DataLoader(text_sourceFile.Text);
            int[][] inputData;
            try
            {
                inputData = loader.parseAndLoad(';');
            }
            catch (ExceptionInfoToGUI ex)
            {
                MessageBox.Show(ex.getMessage());
                return;
            }*/
            int[][] inputData = DataLoader.debug_generateRandomTestData(600, 900, 2000, 3000, 1, 30000);
            Executor executorObject;
            try
            {
                executorObject = new Executor(inputData, 4, Executor.Lib.asm, Executor.Method.bubble);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("Missing csharpLib.dll");
                return;
            }
            catch (System.DllNotFoundException ex)
            {
                MessageBox.Show("Missing asmLib.dll");
                return;
            }
            executorObject.start();

            /*if (executorObject.debug_executeAndCompareResult(Executor.Method.bubble))
            {
                Console.WriteLine("Bubble is working!");
            }
            if (executorObject.debug_executeAndCompareResult(Executor.Method.insert))
            {
                Console.WriteLine("Insert is working!");
            }*/
        }
    }
}
