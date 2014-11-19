namespace sortingProject
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_sourceFile = new System.Windows.Forms.Label();
            this.text_sourceFile = new System.Windows.Forms.TextBox();
            this.button_openFile = new System.Windows.Forms.Button();
            this.combo_numThreads = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_lib = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_outputFile = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.text_exTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_method = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label_sourceFile
            // 
            this.label_sourceFile.AutoSize = true;
            this.label_sourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_sourceFile.Location = new System.Drawing.Point(2, 7);
            this.label_sourceFile.Name = "label_sourceFile";
            this.label_sourceFile.Size = new System.Drawing.Size(104, 24);
            this.label_sourceFile.TabIndex = 0;
            this.label_sourceFile.Text = "Source file:";
            this.label_sourceFile.Click += new System.EventHandler(this.label1_Click);
            // 
            // text_sourceFile
            // 
            this.text_sourceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.text_sourceFile.HideSelection = false;
            this.text_sourceFile.Location = new System.Drawing.Point(112, 4);
            this.text_sourceFile.Name = "text_sourceFile";
            this.text_sourceFile.ReadOnly = true;
            this.text_sourceFile.Size = new System.Drawing.Size(291, 29);
            this.text_sourceFile.TabIndex = 1;
            this.text_sourceFile.TextChanged += new System.EventHandler(this.text_sourceFile_TextChanged);
            // 
            // button_openFile
            // 
            this.button_openFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_openFile.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_openFile.Location = new System.Drawing.Point(420, 5);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(112, 29);
            this.button_openFile.TabIndex = 2;
            this.button_openFile.Text = "Open file";
            this.button_openFile.UseVisualStyleBackColor = true;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // combo_numThreads
            // 
            this.combo_numThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.combo_numThreads.FormattingEnabled = true;
            this.combo_numThreads.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64"});
            this.combo_numThreads.Location = new System.Drawing.Point(170, 90);
            this.combo_numThreads.Name = "combo_numThreads";
            this.combo_numThreads.Size = new System.Drawing.Size(48, 32);
            this.combo_numThreads.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of threads";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(224, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Library";
            // 
            // combo_lib
            // 
            this.combo_lib.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.combo_lib.FormattingEnabled = true;
            this.combo_lib.Items.AddRange(new object[] {
            "C#",
            "ASM"});
            this.combo_lib.Location = new System.Drawing.Point(296, 90);
            this.combo_lib.Name = "combo_lib";
            this.combo_lib.Size = new System.Drawing.Size(52, 32);
            this.combo_lib.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(2, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output filename:";
            // 
            // text_outputFile
            // 
            this.text_outputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.text_outputFile.Location = new System.Drawing.Point(150, 47);
            this.text_outputFile.Name = "text_outputFile";
            this.text_outputFile.Size = new System.Drawing.Size(253, 29);
            this.text_outputFile.TabIndex = 8;
            // 
            // button_start
            // 
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_start.Location = new System.Drawing.Point(208, 129);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(120, 38);
            this.button_start.TabIndex = 9;
            this.button_start.TabStop = false;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(37, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Execution time:";
            // 
            // text_exTime
            // 
            this.text_exTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.text_exTime.Location = new System.Drawing.Point(187, 174);
            this.text_exTime.Name = "text_exTime";
            this.text_exTime.ReadOnly = true;
            this.text_exTime.Size = new System.Drawing.Size(270, 31);
            this.text_exTime.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(354, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "Method";
            // 
            // combo_method
            // 
            this.combo_method.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.combo_method.FormattingEnabled = true;
            this.combo_method.Items.AddRange(new object[] {
            "bubble",
            "insert",
            "quick"});
            this.combo_method.Location = new System.Drawing.Point(434, 90);
            this.combo_method.Name = "combo_method";
            this.combo_method.Size = new System.Drawing.Size(109, 32);
            this.combo_method.TabIndex = 13;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 212);
            this.Controls.Add(this.combo_method);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_exTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.text_outputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combo_lib);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_numThreads);
            this.Controls.Add(this.button_openFile);
            this.Controls.Add(this.text_sourceFile);
            this.Controls.Add(this.label_sourceFile);
            this.Name = "mainWindow";
            this.Text = "Sorting Algorithms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label_sourceFile;
        private System.Windows.Forms.TextBox text_sourceFile;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.ComboBox combo_numThreads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_lib;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_outputFile;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_exTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_method;
    }
}

