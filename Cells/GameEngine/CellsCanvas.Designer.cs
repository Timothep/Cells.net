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
            DrawBox = new System.Windows.Forms.PictureBox();
            bStartEngine = new System.Windows.Forms.Button();
            bStopEngine = new System.Windows.Forms.Button();
            FPSTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(DrawBox)).BeginInit();
            SuspendLayout();
            // 
            // DrawBox
            // 
            DrawBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            DrawBox.Location = new System.Drawing.Point(13, 13);
            DrawBox.Name = "DrawBox";
            DrawBox.Size = new System.Drawing.Size(500, 500);
            DrawBox.TabIndex = 0;
            DrawBox.TabStop = false;
            // 
            // bStartEngine
            // 
            bStartEngine.Location = new System.Drawing.Point(520, 13);
            bStartEngine.Name = "bStartEngine";
            bStartEngine.Size = new System.Drawing.Size(75, 23);
            bStartEngine.TabIndex = 1;
            bStartEngine.Text = "Start Engine";
            bStartEngine.UseVisualStyleBackColor = true;
            bStartEngine.Click += new System.EventHandler(BStartEngineClick);
            // 
            // bStopEngine
            // 
            bStopEngine.Location = new System.Drawing.Point(520, 43);
            bStopEngine.Name = "bStopEngine";
            bStopEngine.Size = new System.Drawing.Size(75, 23);
            bStopEngine.TabIndex = 2;
            bStopEngine.Text = "Stop Engine";
            bStopEngine.UseVisualStyleBackColor = true;
            bStopEngine.Click += new System.EventHandler(BStopEngineClick);
            // 
            // FPSTextBox
            // 
            FPSTextBox.Location = new System.Drawing.Point(520, 73);
            FPSTextBox.Name = "FPSTextBox";
            FPSTextBox.Size = new System.Drawing.Size(73, 20);
            FPSTextBox.TabIndex = 3;
            // 
            // CellsCanvas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(605, 526);
            Controls.Add(FPSTextBox);
            Controls.Add(bStopEngine);
            Controls.Add(bStartEngine);
            Controls.Add(DrawBox);
            Name = "CellsCanvas";
            Text = "CellEngine";
            ((System.ComponentModel.ISupportInitialize)(DrawBox)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawBox;
        private System.Windows.Forms.Button bStartEngine;
        private System.Windows.Forms.Button bStopEngine;
        private System.Windows.Forms.TextBox FPSTextBox;
    }
}

