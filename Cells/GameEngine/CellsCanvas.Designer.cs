using Cells.Properties;

namespace Cells.GameEngine
{
    partial class CellsCanvas
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
            this.DrawBox = new System.Windows.Forms.PictureBox();
            this.bStartEngine = new System.Windows.Forms.Button();
            this.bStopEngine = new System.Windows.Forms.Button();
            this.ParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lMaxNumberOfCells = new System.Windows.Forms.Label();
            this.lInitialPopPerBrain = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lCellInitialLife = new System.Windows.Forms.Label();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lMaxLandscapeHeight = new System.Windows.Forms.Label();
            this.lMinLandscapeHeight = new System.Windows.Forms.Label();
            this.tBViewSize = new System.Windows.Forms.TextBox();
            this.lSubViewSize = new System.Windows.Forms.Label();
            this.gBBrainSelection = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).BeginInit();
            this.ParametersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawBox
            // 
            this.DrawBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DrawBox.Location = new System.Drawing.Point(13, 13);
            this.DrawBox.Name = "DrawBox";
            this.DrawBox.Size = new System.Drawing.Size(500, 500);
            this.DrawBox.TabIndex = 0;
            this.DrawBox.TabStop = false;
            // 
            // bStartEngine
            // 
            this.bStartEngine.BackColor = System.Drawing.SystemColors.Control;
            this.bStartEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStartEngine.Location = new System.Drawing.Point(519, 13);
            this.bStartEngine.Name = "bStartEngine";
            this.bStartEngine.Size = new System.Drawing.Size(111, 23);
            this.bStartEngine.TabIndex = 1;
            this.bStartEngine.Text = "Start Engine";
            this.bStartEngine.UseVisualStyleBackColor = true;
            this.bStartEngine.Click += new System.EventHandler(this.BStartEngineClick);
            // 
            // bStopEngine
            // 
            this.bStopEngine.BackColor = System.Drawing.SystemColors.Menu;
            this.bStopEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStopEngine.Location = new System.Drawing.Point(645, 13);
            this.bStopEngine.Name = "bStopEngine";
            this.bStopEngine.Size = new System.Drawing.Size(111, 23);
            this.bStopEngine.TabIndex = 2;
            this.bStopEngine.Text = "Stop Engine";
            this.bStopEngine.UseVisualStyleBackColor = true;
            this.bStopEngine.Click += new System.EventHandler(this.BStopEngineClick);
            // 
            // ParametersGroupBox
            // 
            this.ParametersGroupBox.Controls.Add(this.label4);
            this.ParametersGroupBox.Controls.Add(this.label3);
            this.ParametersGroupBox.Controls.Add(this.label2);
            this.ParametersGroupBox.Controls.Add(this.label1);
            this.ParametersGroupBox.Controls.Add(this.lMaxNumberOfCells);
            this.ParametersGroupBox.Controls.Add(this.lInitialPopPerBrain);
            this.ParametersGroupBox.Controls.Add(this.textBox9);
            this.ParametersGroupBox.Controls.Add(this.textBox8);
            this.ParametersGroupBox.Controls.Add(this.textBox7);
            this.ParametersGroupBox.Controls.Add(this.textBox6);
            this.ParametersGroupBox.Controls.Add(this.textBox5);
            this.ParametersGroupBox.Controls.Add(this.textBox4);
            this.ParametersGroupBox.Controls.Add(this.textBox3);
            this.ParametersGroupBox.Controls.Add(this.lCellInitialLife);
            this.ParametersGroupBox.Controls.Add(this.bSaveSettings);
            this.ParametersGroupBox.Controls.Add(this.textBox2);
            this.ParametersGroupBox.Controls.Add(this.textBox1);
            this.ParametersGroupBox.Controls.Add(this.lMaxLandscapeHeight);
            this.ParametersGroupBox.Controls.Add(this.lMinLandscapeHeight);
            this.ParametersGroupBox.Controls.Add(this.tBViewSize);
            this.ParametersGroupBox.Controls.Add(this.lSubViewSize);
            this.ParametersGroupBox.Location = new System.Drawing.Point(519, 155);
            this.ParametersGroupBox.Name = "ParametersGroupBox";
            this.ParametersGroupBox.Size = new System.Drawing.Size(237, 358);
            this.ParametersGroupBox.TabIndex = 3;
            this.ParametersGroupBox.TabStop = false;
            this.ParametersGroupBox.Text = "Parameters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Damage on aggressive opponent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Damage on passive opponent";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Cost of cell division";
            // 
            // lMaxNumberOfCells
            // 
            this.lMaxNumberOfCells.AutoSize = true;
            this.lMaxNumberOfCells.Location = new System.Drawing.Point(6, 46);
            this.lMaxNumberOfCells.Name = "lMaxNumberOfCells";
            this.lMaxNumberOfCells.Size = new System.Drawing.Size(122, 13);
            this.lMaxNumberOfCells.TabIndex = 19;
            this.lMaxNumberOfCells.Text = "Maximal Number of Cells";
            // 
            // lInitialPopPerBrain
            // 
            this.lInitialPopPerBrain.AutoSize = true;
            this.lInitialPopPerBrain.Location = new System.Drawing.Point(6, 20);
            this.lInitialPopPerBrain.Name = "lInitialPopPerBrain";
            this.lInitialPopPerBrain.Size = new System.Drawing.Size(129, 13);
            this.lInitialPopPerBrain.TabIndex = 18;
            this.lInitialPopPerBrain.Text = "Initial Population per Brain";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(175, 251);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(56, 20);
            this.textBox9.TabIndex = 15;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(175, 225);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(56, 20);
            this.textBox8.TabIndex = 14;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(175, 199);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(56, 20);
            this.textBox7.TabIndex = 13;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(175, 173);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(56, 20);
            this.textBox6.TabIndex = 12;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(175, 147);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(56, 20);
            this.textBox5.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(175, 121);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(56, 20);
            this.textBox4.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(175, 95);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(56, 20);
            this.textBox3.TabIndex = 9;
            // 
            // lCellInitialLife
            // 
            this.lCellInitialLife.AutoSize = true;
            this.lCellInitialLife.Location = new System.Drawing.Point(6, 72);
            this.lCellInitialLife.Name = "lCellInitialLife";
            this.lCellInitialLife.Size = new System.Drawing.Size(71, 13);
            this.lCellInitialLife.TabIndex = 8;
            this.lCellInitialLife.Text = "Cell Initial Life";
            // 
            // bSaveSettings
            // 
            this.bSaveSettings.Location = new System.Drawing.Point(146, 329);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(87, 23);
            this.bSaveSettings.TabIndex = 7;
            this.bSaveSettings.Text = "Save Changes";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(175, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(56, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(175, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(56, 20);
            this.textBox1.TabIndex = 4;
            // 
            // lMaxLandscapeHeight
            // 
            this.lMaxLandscapeHeight.AutoSize = true;
            this.lMaxLandscapeHeight.Location = new System.Drawing.Point(6, 254);
            this.lMaxLandscapeHeight.Name = "lMaxLandscapeHeight";
            this.lMaxLandscapeHeight.Size = new System.Drawing.Size(117, 13);
            this.lMaxLandscapeHeight.TabIndex = 3;
            this.lMaxLandscapeHeight.Text = "Max Landscape Height";
            // 
            // lMinLandscapeHeight
            // 
            this.lMinLandscapeHeight.AutoSize = true;
            this.lMinLandscapeHeight.Location = new System.Drawing.Point(6, 228);
            this.lMinLandscapeHeight.Name = "lMinLandscapeHeight";
            this.lMinLandscapeHeight.Size = new System.Drawing.Size(114, 13);
            this.lMinLandscapeHeight.TabIndex = 2;
            this.lMinLandscapeHeight.Text = "Min Landscape Height";
            // 
            // tBViewSize
            // 
            this.tBViewSize.Location = new System.Drawing.Point(175, 17);
            this.tBViewSize.Name = "tBViewSize";
            this.tBViewSize.Size = new System.Drawing.Size(56, 20);
            this.tBViewSize.TabIndex = 1;
            // 
            // lSubViewSize
            // 
            this.lSubViewSize.AutoSize = true;
            this.lSubViewSize.Location = new System.Drawing.Point(6, 202);
            this.lSubViewSize.Name = "lSubViewSize";
            this.lSubViewSize.Size = new System.Drawing.Size(114, 13);
            this.lSubViewSize.TabIndex = 0;
            this.lSubViewSize.Text = "Cell Sensory View Size";
            // 
            // gBBrainSelection
            // 
            this.gBBrainSelection.Location = new System.Drawing.Point(519, 41);
            this.gBBrainSelection.Name = "gBBrainSelection";
            this.gBBrainSelection.Size = new System.Drawing.Size(237, 108);
            this.gBBrainSelection.TabIndex = 4;
            this.gBBrainSelection.TabStop = false;
            this.gBBrainSelection.Text = "Brain Selection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Spawn life threshold";
            // 
            // CellsCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(764, 524);
            this.Controls.Add(this.bStopEngine);
            this.Controls.Add(this.gBBrainSelection);
            this.Controls.Add(this.ParametersGroupBox);
            this.Controls.Add(this.bStartEngine);
            this.Controls.Add(this.DrawBox);
            this.Name = "CellsCanvas";
            this.Text = "CellEngine";
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).EndInit();
            this.ParametersGroupBox.ResumeLayout(false);
            this.ParametersGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawBox;
        private System.Windows.Forms.Button bStartEngine;
        private System.Windows.Forms.Button bStopEngine;
        private System.Windows.Forms.GroupBox ParametersGroupBox;
        private System.Windows.Forms.TextBox tBViewSize;
        private System.Windows.Forms.Label lSubViewSize;
        private System.Windows.Forms.Label lMaxLandscapeHeight;
        private System.Windows.Forms.Label lMinLandscapeHeight;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lCellInitialLife;
        private System.Windows.Forms.GroupBox gBBrainSelection;
        private System.Windows.Forms.Label lMaxNumberOfCells;
        private System.Windows.Forms.Label lInitialPopPerBrain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}

