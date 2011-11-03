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
            this.FPSTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).BeginInit();
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
            this.bStartEngine.Location = new System.Drawing.Point(520, 13);
            this.bStartEngine.Name = "bStartEngine";
            this.bStartEngine.Size = new System.Drawing.Size(75, 23);
            this.bStartEngine.TabIndex = 1;
            this.bStartEngine.Text = "Start Engine";
            this.bStartEngine.UseVisualStyleBackColor = true;
            this.bStartEngine.Click += new System.EventHandler(this.BStartEngineClick);
            // 
            // bStopEngine
            // 
            this.bStopEngine.Location = new System.Drawing.Point(520, 43);
            this.bStopEngine.Name = "bStopEngine";
            this.bStopEngine.Size = new System.Drawing.Size(75, 23);
            this.bStopEngine.TabIndex = 2;
            this.bStopEngine.Text = "Stop Engine";
            this.bStopEngine.UseVisualStyleBackColor = true;
            this.bStopEngine.Click += new System.EventHandler(this.BStopEngineClick);
            // 
            // FPSTextBox
            // 
            this.FPSTextBox.Location = new System.Drawing.Point(520, 73);
            this.FPSTextBox.Name = "FPSTextBox";
            this.FPSTextBox.Size = new System.Drawing.Size(73, 20);
            this.FPSTextBox.TabIndex = 3;
            // 
            // CellsCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(605, 526);
            this.Controls.Add(this.FPSTextBox);
            this.Controls.Add(this.bStopEngine);
            this.Controls.Add(this.bStartEngine);
            this.Controls.Add(this.DrawBox);
            this.Name = "CellsCanvas";
            this.Text = "CellEngine";
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawBox;
        private System.Windows.Forms.Button bStartEngine;
        private System.Windows.Forms.Button bStopEngine;
        private System.Windows.Forms.TextBox FPSTextBox;
    }
}

