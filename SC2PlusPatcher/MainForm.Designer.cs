
namespace SC2PlusPatcher
{
    partial class MainForm
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
            this.olkTextBox = new System.Windows.Forms.TextBox();
            this.olkGroupBox = new System.Windows.Forms.GroupBox();
            this.olkPathButton = new System.Windows.Forms.Button();
            this.exeGroupBox = new System.Windows.Forms.GroupBox();
            this.exePathButton = new System.Windows.Forms.Button();
            this.exeTextBox = new System.Windows.Forms.TextBox();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.versionGroupBox = new System.Windows.Forms.GroupBox();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.olkPatchCheckBox = new System.Windows.Forms.CheckBox();
            this.exePatchCheckBox = new System.Windows.Forms.CheckBox();
            this.expandCheckBox = new System.Windows.Forms.CheckBox();
            this.patchButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modPathButton = new System.Windows.Forms.Button();
            this.modTextBox = new System.Windows.Forms.TextBox();
            this.olkOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.exeOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusTextBox = new System.Windows.Forms.RichTextBox();
            this.olkGroupBox.SuspendLayout();
            this.exeGroupBox.SuspendLayout();
            this.versionGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // olkTextBox
            // 
            this.olkTextBox.Location = new System.Drawing.Point(6, 19);
            this.olkTextBox.Name = "olkTextBox";
            this.olkTextBox.ReadOnly = true;
            this.olkTextBox.Size = new System.Drawing.Size(221, 20);
            this.olkTextBox.TabIndex = 0;
            // 
            // olkGroupBox
            // 
            this.olkGroupBox.Controls.Add(this.olkPathButton);
            this.olkGroupBox.Controls.Add(this.olkTextBox);
            this.olkGroupBox.Location = new System.Drawing.Point(12, 78);
            this.olkGroupBox.Name = "olkGroupBox";
            this.olkGroupBox.Size = new System.Drawing.Size(283, 56);
            this.olkGroupBox.TabIndex = 1;
            this.olkGroupBox.TabStop = false;
            this.olkGroupBox.Text = "Root OLK";
            // 
            // olkPathButton
            // 
            this.olkPathButton.Location = new System.Drawing.Point(234, 19);
            this.olkPathButton.Name = "olkPathButton";
            this.olkPathButton.Size = new System.Drawing.Size(37, 19);
            this.olkPathButton.TabIndex = 1;
            this.olkPathButton.Text = "···";
            this.olkPathButton.UseVisualStyleBackColor = true;
            this.olkPathButton.Click += new System.EventHandler(this.olkPathButton_Click);
            // 
            // exeGroupBox
            // 
            this.exeGroupBox.Controls.Add(this.exePathButton);
            this.exeGroupBox.Controls.Add(this.exeTextBox);
            this.exeGroupBox.Location = new System.Drawing.Point(12, 140);
            this.exeGroupBox.Name = "exeGroupBox";
            this.exeGroupBox.Size = new System.Drawing.Size(283, 56);
            this.exeGroupBox.TabIndex = 2;
            this.exeGroupBox.TabStop = false;
            this.exeGroupBox.Text = "Game Executable";
            // 
            // exePathButton
            // 
            this.exePathButton.Location = new System.Drawing.Point(233, 19);
            this.exePathButton.Name = "exePathButton";
            this.exePathButton.Size = new System.Drawing.Size(37, 19);
            this.exePathButton.TabIndex = 1;
            this.exePathButton.Text = "···";
            this.exePathButton.UseVisualStyleBackColor = true;
            this.exePathButton.Click += new System.EventHandler(this.exePathButton_Click);
            // 
            // exeTextBox
            // 
            this.exeTextBox.Location = new System.Drawing.Point(6, 19);
            this.exeTextBox.Name = "exeTextBox";
            this.exeTextBox.ReadOnly = true;
            this.exeTextBox.Size = new System.Drawing.Size(221, 20);
            this.exeTextBox.TabIndex = 0;
            // 
            // versionComboBox
            // 
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Items.AddRange(new object[] {
            "Gamecube",
            "Xbox",
            "PlayStation 2"});
            this.versionComboBox.Location = new System.Drawing.Point(6, 19);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(121, 21);
            this.versionComboBox.TabIndex = 3;
            this.versionComboBox.SelectedIndexChanged += new System.EventHandler(this.versionComboBox_SelectedIndexChanged);
            // 
            // versionGroupBox
            // 
            this.versionGroupBox.Controls.Add(this.versionComboBox);
            this.versionGroupBox.Location = new System.Drawing.Point(334, 12);
            this.versionGroupBox.Name = "versionGroupBox";
            this.versionGroupBox.Size = new System.Drawing.Size(137, 56);
            this.versionGroupBox.TabIndex = 4;
            this.versionGroupBox.TabStop = false;
            this.versionGroupBox.Text = "Version";
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.olkPatchCheckBox);
            this.optionsGroupBox.Controls.Add(this.exePatchCheckBox);
            this.optionsGroupBox.Controls.Add(this.expandCheckBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(334, 78);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(137, 100);
            this.optionsGroupBox.TabIndex = 5;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // olkPatchCheckBox
            // 
            this.olkPatchCheckBox.AutoSize = true;
            this.olkPatchCheckBox.Location = new System.Drawing.Point(11, 45);
            this.olkPatchCheckBox.Name = "olkPatchCheckBox";
            this.olkPatchCheckBox.Size = new System.Drawing.Size(78, 17);
            this.olkPatchCheckBox.TabIndex = 2;
            this.olkPatchCheckBox.Text = "Patch OLK";
            this.olkPatchCheckBox.UseVisualStyleBackColor = true;
            this.olkPatchCheckBox.CheckedChanged += new System.EventHandler(this.olkPatchCheckBox_CheckedChanged);
            // 
            // exePatchCheckBox
            // 
            this.exePatchCheckBox.AutoSize = true;
            this.exePatchCheckBox.Location = new System.Drawing.Point(11, 68);
            this.exePatchCheckBox.Name = "exePatchCheckBox";
            this.exePatchCheckBox.Size = new System.Drawing.Size(79, 17);
            this.exePatchCheckBox.TabIndex = 1;
            this.exePatchCheckBox.Text = "Patch DOL";
            this.exePatchCheckBox.UseVisualStyleBackColor = true;
            this.exePatchCheckBox.CheckedChanged += new System.EventHandler(this.exePatchCheckBox_CheckedChanged);
            // 
            // expandCheckBox
            // 
            this.expandCheckBox.AutoSize = true;
            this.expandCheckBox.Location = new System.Drawing.Point(11, 22);
            this.expandCheckBox.Name = "expandCheckBox";
            this.expandCheckBox.Size = new System.Drawing.Size(86, 17);
            this.expandCheckBox.TabIndex = 0;
            this.expandCheckBox.Text = "Expand OLK";
            this.expandCheckBox.UseVisualStyleBackColor = true;
            this.expandCheckBox.CheckedChanged += new System.EventHandler(this.expandCheckBox_CheckedChanged);
            // 
            // patchButton
            // 
            this.patchButton.Location = new System.Drawing.Point(356, 216);
            this.patchButton.Name = "patchButton";
            this.patchButton.Size = new System.Drawing.Size(75, 23);
            this.patchButton.TabIndex = 6;
            this.patchButton.Text = "Patch";
            this.patchButton.UseVisualStyleBackColor = true;
            this.patchButton.Click += new System.EventHandler(this.patchButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modPathButton);
            this.groupBox1.Controls.Add(this.modTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 56);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mod Files";
            // 
            // modPathButton
            // 
            this.modPathButton.Location = new System.Drawing.Point(234, 19);
            this.modPathButton.Name = "modPathButton";
            this.modPathButton.Size = new System.Drawing.Size(37, 19);
            this.modPathButton.TabIndex = 1;
            this.modPathButton.Text = "···";
            this.modPathButton.UseVisualStyleBackColor = true;
            this.modPathButton.Click += new System.EventHandler(this.modPathButton_Click);
            // 
            // modTextBox
            // 
            this.modTextBox.Location = new System.Drawing.Point(6, 19);
            this.modTextBox.Name = "modTextBox";
            this.modTextBox.ReadOnly = true;
            this.modTextBox.Size = new System.Drawing.Size(221, 20);
            this.modTextBox.TabIndex = 0;
            // 
            // olkOpenDialog
            // 
            this.olkOpenDialog.Filter = "OLK files|*.olk|All files|*.*";
            // 
            // exeOpenDialog
            // 
            this.exeOpenDialog.Filter = "DOL files|*.dol|All files|*.*";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(12, 269);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(459, 80);
            this.statusTextBox.TabIndex = 8;
            this.statusTextBox.Text = "";
            this.statusTextBox.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.patchButton);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.versionGroupBox);
            this.Controls.Add(this.exeGroupBox);
            this.Controls.Add(this.olkGroupBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SC2 Plus Patcher 0.9.8";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.olkGroupBox.ResumeLayout(false);
            this.olkGroupBox.PerformLayout();
            this.exeGroupBox.ResumeLayout(false);
            this.exeGroupBox.PerformLayout();
            this.versionGroupBox.ResumeLayout(false);
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox olkTextBox;
        private System.Windows.Forms.GroupBox olkGroupBox;
        private System.Windows.Forms.Button olkPathButton;
        private System.Windows.Forms.GroupBox exeGroupBox;
        private System.Windows.Forms.Button exePathButton;
        private System.Windows.Forms.TextBox exeTextBox;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.GroupBox versionGroupBox;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.CheckBox expandCheckBox;
        private System.Windows.Forms.Button patchButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button modPathButton;
        private System.Windows.Forms.TextBox modTextBox;
        private System.Windows.Forms.OpenFileDialog olkOpenDialog;
        private System.Windows.Forms.OpenFileDialog exeOpenDialog;
        private System.Windows.Forms.RichTextBox statusTextBox;
        private System.Windows.Forms.CheckBox exePatchCheckBox;
        private System.Windows.Forms.CheckBox olkPatchCheckBox;
    }
}

