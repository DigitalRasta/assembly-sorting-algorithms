using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class MainWindow : Form
    {
        public MainWindow()
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
            int[][] inputData = DataLoader.debug_generateRandomTestData(300, 3000, 8000, 12000, 1, 30000);
            int[][] sortedArray = inputData.Select(a => a.ToArray()).ToArray();
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Array.Sort(sortedArray[i]);
            }
            //Testing testObject = new Testing();
            //testObject.testAllMethodsWithTimes(inputData, sortedArray, 4);
        }
    }
}
