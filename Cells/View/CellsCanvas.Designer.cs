﻿using Cells.Properties;

namespace Cells.View
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
            this.tBCellSize = new System.Windows.Forms.TextBox();
            this.lCellSize = new System.Windows.Forms.Label();
            this.tBNumberOfTeams = new System.Windows.Forms.TextBox();
            this.lNumberOfTeams = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lDamageOnOpponent = new System.Windows.Forms.Label();
            this.lCellDivCost = new System.Windows.Forms.Label();
            this.lMaxNumberOfCells = new System.Windows.Forms.Label();
            this.lInitialPopPerBrain = new System.Windows.Forms.Label();
            this.tBMaxAltitude = new System.Windows.Forms.TextBox();
            this.tBMinAltitude = new System.Windows.Forms.TextBox();
            this.tBCellSensoryViewSize = new System.Windows.Forms.TextBox();
            this.tBSpawnLifeThreshold = new System.Windows.Forms.TextBox();
            this.tBDamageOnOpponent = new System.Windows.Forms.TextBox();
            this.tBCellDivisionCost = new System.Windows.Forms.TextBox();
            this.lCellInitialLife = new System.Windows.Forms.Label();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.tBCellInitialLife = new System.Windows.Forms.TextBox();
            this.tBMaxNumberCells = new System.Windows.Forms.TextBox();
            this.lMaxAltitude = new System.Windows.Forms.Label();
            this.lMinAltitude = new System.Windows.Forms.Label();
            this.tBIntialPopPerBrain = new System.Windows.Forms.TextBox();
            this.lSubViewSize = new System.Windows.Forms.Label();
            this.lBBrains = new System.Windows.Forms.ListBox();
            this.lbMaps = new System.Windows.Forms.ListBox();
            this.lBrainsTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).BeginInit();
            this.ParametersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawBox
            // 
            this.DrawBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DrawBox.Location = new System.Drawing.Point(6, 8);
            this.DrawBox.Name = "DrawBox";
            this.DrawBox.Size = new System.Drawing.Size(500, 500);
            this.DrawBox.TabIndex = 0;
            this.DrawBox.TabStop = false;
            // 
            // bStartEngine
            // 
            this.bStartEngine.BackColor = System.Drawing.SystemColors.Control;
            this.bStartEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStartEngine.Location = new System.Drawing.Point(512, 8);
            this.bStartEngine.Name = "bStartEngine";
            this.bStartEngine.Size = new System.Drawing.Size(77, 23);
            this.bStartEngine.TabIndex = 1;
            this.bStartEngine.Text = "Start Engine";
            this.bStartEngine.UseVisualStyleBackColor = true;
            this.bStartEngine.Click += new System.EventHandler(this.BStartEngineClick);
            // 
            // bStopEngine
            // 
            this.bStopEngine.BackColor = System.Drawing.SystemColors.Menu;
            this.bStopEngine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStopEngine.Location = new System.Drawing.Point(595, 8);
            this.bStopEngine.Name = "bStopEngine";
            this.bStopEngine.Size = new System.Drawing.Size(80, 23);
            this.bStopEngine.TabIndex = 2;
            this.bStopEngine.Text = "Stop Engine";
            this.bStopEngine.UseVisualStyleBackColor = true;
            this.bStopEngine.Click += new System.EventHandler(this.BStopEngineClick);
            // 
            // ParametersGroupBox
            // 
            this.ParametersGroupBox.Controls.Add(this.tBCellSize);
            this.ParametersGroupBox.Controls.Add(this.lCellSize);
            this.ParametersGroupBox.Controls.Add(this.tBNumberOfTeams);
            this.ParametersGroupBox.Controls.Add(this.lNumberOfTeams);
            this.ParametersGroupBox.Controls.Add(this.label4);
            this.ParametersGroupBox.Controls.Add(this.lDamageOnOpponent);
            this.ParametersGroupBox.Controls.Add(this.lCellDivCost);
            this.ParametersGroupBox.Controls.Add(this.lMaxNumberOfCells);
            this.ParametersGroupBox.Controls.Add(this.lInitialPopPerBrain);
            this.ParametersGroupBox.Controls.Add(this.tBMaxAltitude);
            this.ParametersGroupBox.Controls.Add(this.tBMinAltitude);
            this.ParametersGroupBox.Controls.Add(this.tBCellSensoryViewSize);
            this.ParametersGroupBox.Controls.Add(this.tBSpawnLifeThreshold);
            this.ParametersGroupBox.Controls.Add(this.tBDamageOnOpponent);
            this.ParametersGroupBox.Controls.Add(this.tBCellDivisionCost);
            this.ParametersGroupBox.Controls.Add(this.lCellInitialLife);
            this.ParametersGroupBox.Controls.Add(this.bSaveSettings);
            this.ParametersGroupBox.Controls.Add(this.tBCellInitialLife);
            this.ParametersGroupBox.Controls.Add(this.tBMaxNumberCells);
            this.ParametersGroupBox.Controls.Add(this.lMaxAltitude);
            this.ParametersGroupBox.Controls.Add(this.lMinAltitude);
            this.ParametersGroupBox.Controls.Add(this.tBIntialPopPerBrain);
            this.ParametersGroupBox.Controls.Add(this.lSubViewSize);
            this.ParametersGroupBox.Location = new System.Drawing.Point(755, 8);
            this.ParametersGroupBox.Name = "ParametersGroupBox";
            this.ParametersGroupBox.Size = new System.Drawing.Size(236, 500);
            this.ParametersGroupBox.TabIndex = 3;
            this.ParametersGroupBox.TabStop = false;
            this.ParametersGroupBox.Text = "Parameters";
            // 
            // tBCellSize
            // 
            this.tBCellSize.Location = new System.Drawing.Point(175, 277);
            this.tBCellSize.Name = "tBCellSize";
            this.tBCellSize.Size = new System.Drawing.Size(56, 20);
            this.tBCellSize.TabIndex = 27;
            // 
            // lCellSize
            // 
            this.lCellSize.AutoSize = true;
            this.lCellSize.Location = new System.Drawing.Point(6, 280);
            this.lCellSize.Name = "lCellSize";
            this.lCellSize.Size = new System.Drawing.Size(115, 13);
            this.lCellSize.TabIndex = 26;
            this.lCellSize.Text = "Map cell size (in Pixels)";
            // 
            // tBNumberOfTeams
            // 
            this.tBNumberOfTeams.Location = new System.Drawing.Point(175, 251);
            this.tBNumberOfTeams.Name = "tBNumberOfTeams";
            this.tBNumberOfTeams.Size = new System.Drawing.Size(56, 20);
            this.tBNumberOfTeams.TabIndex = 25;
            // 
            // lNumberOfTeams
            // 
            this.lNumberOfTeams.AutoSize = true;
            this.lNumberOfTeams.Location = new System.Drawing.Point(6, 254);
            this.lNumberOfTeams.Name = "lNumberOfTeams";
            this.lNumberOfTeams.Size = new System.Drawing.Size(91, 13);
            this.lNumberOfTeams.TabIndex = 24;
            this.lNumberOfTeams.Text = "Number of Teams";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Spawn life threshold";
            // 
            // lDamageOnOpponent
            // 
            this.lDamageOnOpponent.AutoSize = true;
            this.lDamageOnOpponent.Location = new System.Drawing.Point(6, 124);
            this.lDamageOnOpponent.Name = "lDamageOnOpponent";
            this.lDamageOnOpponent.Size = new System.Drawing.Size(112, 13);
            this.lDamageOnOpponent.TabIndex = 21;
            this.lDamageOnOpponent.Text = "Damage on Opponent";
            // 
            // lCellDivCost
            // 
            this.lCellDivCost.AutoSize = true;
            this.lCellDivCost.Location = new System.Drawing.Point(6, 98);
            this.lCellDivCost.Name = "lCellDivCost";
            this.lCellDivCost.Size = new System.Drawing.Size(97, 13);
            this.lCellDivCost.TabIndex = 20;
            this.lCellDivCost.Text = "Cost of cell division";
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
            // tBMaxAltitude
            // 
            this.tBMaxAltitude.Location = new System.Drawing.Point(175, 225);
            this.tBMaxAltitude.Name = "tBMaxAltitude";
            this.tBMaxAltitude.Size = new System.Drawing.Size(56, 20);
            this.tBMaxAltitude.TabIndex = 15;
            // 
            // tBMinAltitude
            // 
            this.tBMinAltitude.Location = new System.Drawing.Point(175, 199);
            this.tBMinAltitude.Name = "tBMinAltitude";
            this.tBMinAltitude.Size = new System.Drawing.Size(56, 20);
            this.tBMinAltitude.TabIndex = 14;
            // 
            // tBCellSensoryViewSize
            // 
            this.tBCellSensoryViewSize.Location = new System.Drawing.Point(175, 173);
            this.tBCellSensoryViewSize.Name = "tBCellSensoryViewSize";
            this.tBCellSensoryViewSize.Size = new System.Drawing.Size(56, 20);
            this.tBCellSensoryViewSize.TabIndex = 13;
            // 
            // tBSpawnLifeThreshold
            // 
            this.tBSpawnLifeThreshold.Location = new System.Drawing.Point(175, 147);
            this.tBSpawnLifeThreshold.Name = "tBSpawnLifeThreshold";
            this.tBSpawnLifeThreshold.Size = new System.Drawing.Size(56, 20);
            this.tBSpawnLifeThreshold.TabIndex = 12;
            // 
            // tBDamageOnOpponent
            // 
            this.tBDamageOnOpponent.Location = new System.Drawing.Point(175, 121);
            this.tBDamageOnOpponent.Name = "tBDamageOnOpponent";
            this.tBDamageOnOpponent.Size = new System.Drawing.Size(56, 20);
            this.tBDamageOnOpponent.TabIndex = 10;
            // 
            // tBCellDivisionCost
            // 
            this.tBCellDivisionCost.Location = new System.Drawing.Point(175, 95);
            this.tBCellDivisionCost.Name = "tBCellDivisionCost";
            this.tBCellDivisionCost.Size = new System.Drawing.Size(56, 20);
            this.tBCellDivisionCost.TabIndex = 9;
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
            this.bSaveSettings.Location = new System.Drawing.Point(143, 471);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(87, 23);
            this.bSaveSettings.TabIndex = 7;
            this.bSaveSettings.Text = "Save Changes";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            this.bSaveSettings.Click += new System.EventHandler(this.BSaveSettingsClick);
            // 
            // tBCellInitialLife
            // 
            this.tBCellInitialLife.Location = new System.Drawing.Point(175, 69);
            this.tBCellInitialLife.Name = "tBCellInitialLife";
            this.tBCellInitialLife.Size = new System.Drawing.Size(56, 20);
            this.tBCellInitialLife.TabIndex = 5;
            // 
            // tBMaxNumberCells
            // 
            this.tBMaxNumberCells.Location = new System.Drawing.Point(175, 43);
            this.tBMaxNumberCells.Name = "tBMaxNumberCells";
            this.tBMaxNumberCells.Size = new System.Drawing.Size(56, 20);
            this.tBMaxNumberCells.TabIndex = 4;
            // 
            // lMaxAltitude
            // 
            this.lMaxAltitude.AutoSize = true;
            this.lMaxAltitude.Location = new System.Drawing.Point(6, 228);
            this.lMaxAltitude.Name = "lMaxAltitude";
            this.lMaxAltitude.Size = new System.Drawing.Size(64, 13);
            this.lMaxAltitude.TabIndex = 3;
            this.lMaxAltitude.Text = "Max altitude";
            // 
            // lMinAltitude
            // 
            this.lMinAltitude.AutoSize = true;
            this.lMinAltitude.Location = new System.Drawing.Point(6, 202);
            this.lMinAltitude.Name = "lMinAltitude";
            this.lMinAltitude.Size = new System.Drawing.Size(61, 13);
            this.lMinAltitude.TabIndex = 2;
            this.lMinAltitude.Text = "Min altitude";
            // 
            // tBIntialPopPerBrain
            // 
            this.tBIntialPopPerBrain.Location = new System.Drawing.Point(175, 17);
            this.tBIntialPopPerBrain.Name = "tBIntialPopPerBrain";
            this.tBIntialPopPerBrain.Size = new System.Drawing.Size(56, 20);
            this.tBIntialPopPerBrain.TabIndex = 1;
            // 
            // lSubViewSize
            // 
            this.lSubViewSize.AutoSize = true;
            this.lSubViewSize.Location = new System.Drawing.Point(6, 176);
            this.lSubViewSize.Name = "lSubViewSize";
            this.lSubViewSize.Size = new System.Drawing.Size(114, 13);
            this.lSubViewSize.TabIndex = 0;
            this.lSubViewSize.Text = "Cell Sensory View Size";
            // 
            // lBBrains
            // 
            this.lBBrains.FormattingEnabled = true;
            this.lBBrains.Location = new System.Drawing.Point(512, 52);
            this.lBBrains.Name = "lBBrains";
            this.lBBrains.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lBBrains.Size = new System.Drawing.Size(237, 303);
            this.lBBrains.TabIndex = 4;
            // 
            // lbMaps
            // 
            this.lbMaps.FormattingEnabled = true;
            this.lbMaps.Location = new System.Drawing.Point(512, 374);
            this.lbMaps.Name = "lbMaps";
            this.lbMaps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbMaps.Size = new System.Drawing.Size(236, 134);
            this.lbMaps.TabIndex = 5;
            this.lbMaps.SelectedIndexChanged += new System.EventHandler(this.lbMaps_SelectedIndexChanged);
            // 
            // lBrainsTitle
            // 
            this.lBrainsTitle.AutoSize = true;
            this.lBrainsTitle.Location = new System.Drawing.Point(509, 34);
            this.lBrainsTitle.Name = "lBrainsTitle";
            this.lBrainsTitle.Size = new System.Drawing.Size(131, 13);
            this.lBrainsTitle.TabIndex = 6;
            this.lBrainsTitle.Text = "Brain Selection (choose 2)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Map Selection (choose 1)";
            // 
            // CellsCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(997, 514);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lBrainsTitle);
            this.Controls.Add(this.lbMaps);
            this.Controls.Add(this.lBBrains);
            this.Controls.Add(this.bStopEngine);
            this.Controls.Add(this.ParametersGroupBox);
            this.Controls.Add(this.bStartEngine);
            this.Controls.Add(this.DrawBox);
            this.Name = "CellsCanvas";
            this.Text = "CellEngine";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CellsCanvas_FormClosed);
            this.Load += new System.EventHandler(this.CellsCanvasLoad);
            ((System.ComponentModel.ISupportInitialize)(this.DrawBox)).EndInit();
            this.ParametersGroupBox.ResumeLayout(false);
            this.ParametersGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawBox;
        private System.Windows.Forms.Button bStartEngine;
        private System.Windows.Forms.Button bStopEngine;
        private System.Windows.Forms.GroupBox ParametersGroupBox;
        private System.Windows.Forms.TextBox tBIntialPopPerBrain;
        private System.Windows.Forms.Label lSubViewSize;
        private System.Windows.Forms.Label lMaxAltitude;
        private System.Windows.Forms.Label lMinAltitude;
        private System.Windows.Forms.TextBox tBCellInitialLife;
        private System.Windows.Forms.TextBox tBMaxNumberCells;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.TextBox tBMaxAltitude;
        private System.Windows.Forms.TextBox tBMinAltitude;
        private System.Windows.Forms.TextBox tBCellSensoryViewSize;
        private System.Windows.Forms.TextBox tBSpawnLifeThreshold;
        private System.Windows.Forms.TextBox tBDamageOnOpponent;
        private System.Windows.Forms.TextBox tBCellDivisionCost;
        private System.Windows.Forms.Label lCellInitialLife;
        private System.Windows.Forms.Label lMaxNumberOfCells;
        private System.Windows.Forms.Label lInitialPopPerBrain;
        private System.Windows.Forms.Label lCellDivCost;
        private System.Windows.Forms.Label lDamageOnOpponent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBNumberOfTeams;
        private System.Windows.Forms.Label lNumberOfTeams;
        private System.Windows.Forms.TextBox tBCellSize;
        private System.Windows.Forms.Label lCellSize;
        private System.Windows.Forms.ListBox lBBrains;
        private System.Windows.Forms.ListBox lbMaps;
        private System.Windows.Forms.Label lBrainsTitle;
        private System.Windows.Forms.Label label1;
    }
}

