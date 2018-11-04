namespace MultiscaleModelling
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxNeighbourhood = new System.Windows.Forms.ComboBox();
            this.comboBoxBC = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownNumberOfGrain = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.microstructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericSizeOfInclusions = new System.Windows.Forms.NumericUpDown();
            this.numericAmountOfInclusions = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxTypeOfInclusion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAddInclusions = new System.Windows.Forms.Button();
            this.numericMooreProbability = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxStructureType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonSelectGrains = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.numericUpDownSubGrainsNum = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfGrain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeOfInclusions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAmountOfInclusions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMooreProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubGrainsNum)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 336);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Neighbourhood";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comboBoxNeighbourhood
            // 
            this.comboBoxNeighbourhood.FormattingEnabled = true;
            this.comboBoxNeighbourhood.Items.AddRange(new object[] {
            "Von Neumann",
            "Moore",
            "Moore 2"});
            this.comboBoxNeighbourhood.Location = new System.Drawing.Point(438, 108);
            this.comboBoxNeighbourhood.Name = "comboBoxNeighbourhood";
            this.comboBoxNeighbourhood.Size = new System.Drawing.Size(126, 21);
            this.comboBoxNeighbourhood.TabIndex = 6;
            this.comboBoxNeighbourhood.SelectedIndexChanged += new System.EventHandler(this.comboBoxNeighbourhood_SelectedIndexChanged);
            // 
            // comboBoxBC
            // 
            this.comboBoxBC.FormattingEnabled = true;
            this.comboBoxBC.Items.AddRange(new object[] {
            "Periodical",
            "Non-periodical"});
            this.comboBoxBC.Location = new System.Drawing.Point(438, 135);
            this.comboBoxBC.Name = "comboBoxBC";
            this.comboBoxBC.Size = new System.Drawing.Size(126, 21);
            this.comboBoxBC.TabIndex = 8;
            this.comboBoxBC.SelectedIndexChanged += new System.EventHandler(this.comboBoxBC_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Boundary Condition";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 384);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(104, 25);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(122, 384);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(76, 25);
            this.buttonStop.TabIndex = 10;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(204, 384);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(122, 25);
            this.buttonRestart.TabIndex = 11;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(437, 30);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownWidth.TabIndex = 12;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(437, 56);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownHeight.TabIndex = 13;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericUpDownNumberOfGrain
            // 
            this.numericUpDownNumberOfGrain.Location = new System.Drawing.Point(438, 82);
            this.numericUpDownNumberOfGrain.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownNumberOfGrain.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumberOfGrain.Name = "numericUpDownNumberOfGrain";
            this.numericUpDownNumberOfGrain.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownNumberOfGrain.TabIndex = 15;
            this.numericUpDownNumberOfGrain.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Amount of grains";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(814, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.microstructureToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // microstructureToolStripMenuItem
            // 
            this.microstructureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.microstructureToolStripMenuItem.Name = "microstructureToolStripMenuItem";
            this.microstructureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.microstructureToolStripMenuItem.Text = "Microstructure";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click_1);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toTextToolStripMenuItem,
            this.toBitmapToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toTextToolStripMenuItem
            // 
            this.toTextToolStripMenuItem.Name = "toTextToolStripMenuItem";
            this.toTextToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.toTextToolStripMenuItem.Text = "To Text";
            this.toTextToolStripMenuItem.Click += new System.EventHandler(this.toTextToolStripMenuItem_Click_1);
            // 
            // toBitmapToolStripMenuItem
            // 
            this.toBitmapToolStripMenuItem.Name = "toBitmapToolStripMenuItem";
            this.toBitmapToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.toBitmapToolStripMenuItem.Text = "To Bitmap";
            this.toBitmapToolStripMenuItem.Click += new System.EventHandler(this.toBitmapToolStripMenuItem_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(107, 368);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Growing - simulation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Inclusions";
            // 
            // numericSizeOfInclusions
            // 
            this.numericSizeOfInclusions.Location = new System.Drawing.Point(439, 282);
            this.numericSizeOfInclusions.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericSizeOfInclusions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSizeOfInclusions.Name = "numericSizeOfInclusions";
            this.numericSizeOfInclusions.Size = new System.Drawing.Size(126, 20);
            this.numericSizeOfInclusions.TabIndex = 22;
            this.numericSizeOfInclusions.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // numericAmountOfInclusions
            // 
            this.numericAmountOfInclusions.Location = new System.Drawing.Point(439, 256);
            this.numericAmountOfInclusions.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericAmountOfInclusions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAmountOfInclusions.Name = "numericAmountOfInclusions";
            this.numericAmountOfInclusions.Size = new System.Drawing.Size(126, 20);
            this.numericAmountOfInclusions.TabIndex = 21;
            this.numericAmountOfInclusions.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Size of inclusions";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Amount of inclusions";
            // 
            // comboBoxTypeOfInclusion
            // 
            this.comboBoxTypeOfInclusion.FormattingEnabled = true;
            this.comboBoxTypeOfInclusion.Items.AddRange(new object[] {
            "Circle",
            "Square"});
            this.comboBoxTypeOfInclusion.Location = new System.Drawing.Point(439, 308);
            this.comboBoxTypeOfInclusion.Name = "comboBoxTypeOfInclusion";
            this.comboBoxTypeOfInclusion.Size = new System.Drawing.Size(126, 21);
            this.comboBoxTypeOfInclusion.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Type of inclusion";
            // 
            // buttonAddInclusions
            // 
            this.buttonAddInclusions.Location = new System.Drawing.Point(337, 338);
            this.buttonAddInclusions.Name = "buttonAddInclusions";
            this.buttonAddInclusions.Size = new System.Drawing.Size(228, 25);
            this.buttonAddInclusions.TabIndex = 25;
            this.buttonAddInclusions.Text = "Add inclusions";
            this.buttonAddInclusions.UseVisualStyleBackColor = true;
            this.buttonAddInclusions.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericMooreProbability
            // 
            this.numericMooreProbability.Location = new System.Drawing.Point(438, 162);
            this.numericMooreProbability.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMooreProbability.Name = "numericMooreProbability";
            this.numericMooreProbability.Size = new System.Drawing.Size(126, 20);
            this.numericMooreProbability.TabIndex = 27;
            this.numericMooreProbability.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(333, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Probability";
            // 
            // comboBoxStructureType
            // 
            this.comboBoxStructureType.FormattingEnabled = true;
            this.comboBoxStructureType.Items.AddRange(new object[] {
            "Substructure",
            "Dual phase"});
            this.comboBoxStructureType.Location = new System.Drawing.Point(678, 27);
            this.comboBoxStructureType.Name = "comboBoxStructureType";
            this.comboBoxStructureType.Size = new System.Drawing.Size(126, 21);
            this.comboBoxStructureType.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(569, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Substructure Type";
            // 
            // buttonSelectGrains
            // 
            this.buttonSelectGrains.Location = new System.Drawing.Point(572, 80);
            this.buttonSelectGrains.Name = "buttonSelectGrains";
            this.buttonSelectGrains.Size = new System.Drawing.Size(232, 25);
            this.buttonSelectGrains.TabIndex = 30;
            this.buttonSelectGrains.Text = "Select grains";
            this.buttonSelectGrains.UseVisualStyleBackColor = true;
            this.buttonSelectGrains.Click += new System.EventHandler(this.buttonSelectGrains_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(572, 111);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(232, 25);
            this.buttonGenerate.TabIndex = 31;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // numericUpDownSubGrainsNum
            // 
            this.numericUpDownSubGrainsNum.Location = new System.Drawing.Point(678, 54);
            this.numericUpDownSubGrainsNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownSubGrainsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubGrainsNum.Name = "numericUpDownSubGrainsNum";
            this.numericUpDownSubGrainsNum.Size = new System.Drawing.Size(126, 20);
            this.numericUpDownSubGrainsNum.TabIndex = 32;
            this.numericUpDownSubGrainsNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(569, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Amount of subgrains";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(627, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Substructure";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(409, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "General";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 421);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDownSubGrainsNum);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonSelectGrains);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBoxStructureType);
            this.Controls.Add(this.numericMooreProbability);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonAddInclusions);
            this.Controls.Add(this.comboBoxTypeOfInclusion);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericSizeOfInclusions);
            this.Controls.Add(this.numericAmountOfInclusions);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.numericUpDownNumberOfGrain);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.comboBoxBC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxNeighbourhood);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(830, 460);
            this.MinimumSize = new System.Drawing.Size(830, 460);
            this.Name = "Form1";
            this.Text = "Multiscale Modelling";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfGrain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeOfInclusions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAmountOfInclusions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMooreProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubGrainsNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxNeighbourhood;
        private System.Windows.Forms.ComboBox comboBoxBC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfGrain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem microstructureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBitmapToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericSizeOfInclusions;
        private System.Windows.Forms.NumericUpDown numericAmountOfInclusions;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxTypeOfInclusion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAddInclusions;
        private System.Windows.Forms.NumericUpDown numericMooreProbability;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxStructureType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonSelectGrains;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.NumericUpDown numericUpDownSubGrainsNum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

