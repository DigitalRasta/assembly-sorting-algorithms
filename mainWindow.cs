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
            if (text_sourceFile.Text.Length > 0)
            {
                return "OK";
            }
            else
            {
                return "You must select input file!";
            }
        }

        private long currentExTime = -1;
        private long previousExTime = -1;
        private void button_start_Click(object sender, EventArgs e)
        {
            String validation = vaildateUserInput();
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
            catch (Exceptions.ExceptionInfoToGUI ex)
            {
                MessageBox.Show(ex.getMessage());
                return;
            }
            if (currentExTime != -1)
            {
                previousExTime = currentExTime;
            }
            Executor.Lib selectedLib;
            Executor.Method selectedMethod;
            if(combo_lib.SelectedItem.ToString().Equals("C#")) {
                selectedLib = Executor.Lib.cs;
            } else {
                selectedLib = Executor.Lib.asm;
            }

            if(combo_method.SelectedItem.ToString().Equals("bubble")) {
                selectedMethod = Executor.Method.bubble;
            } else if(combo_method.SelectedItem.ToString().Equals("insert")) {
                selectedMethod = Executor.Method.insert;
            } else {
                selectedMethod = Executor.Method.quick;
            }

            Executor execIt;
            try
            {
                execIt = new Executor(inputData, int.Parse(combo_numThreads.SelectedItem.ToString()), selectedLib, selectedMethod);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem while loading dlls!");
                return;
            }
            Stopwatch watch = Stopwatch.StartNew();
            execIt.start();
            watch.Stop();
            currentExTime = watch.ElapsedMilliseconds;
            try
            {
                loader.saveToFile(inputData, text_outputFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save to file!");
            }
            
            text_exTime.Text = currentExTime.ToString();
            text_prTime.Text = previousExTime.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
