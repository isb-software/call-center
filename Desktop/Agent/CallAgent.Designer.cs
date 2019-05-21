namespace Agent
{
    partial class CallAgent
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.UserNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LoggedSinceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CallCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CountyTextBox = new System.Windows.Forms.TextBox();
            this.CallButton = new System.Windows.Forms.Button();
            this.notificationsListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.NotesTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.StatusComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CityTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.HangUpButton = new System.Windows.Forms.Button();
            this.StatusErrorLabel = new System.Windows.Forms.Label();
            this.DocumentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.AddNumberButton = new System.Windows.Forms.Button();
            this.workDayErrorLabel = new System.Windows.Forms.Label();
            this.PhoneNumberTextBox = new System.Windows.Forms.MaskedTextBox();
            this.EducationComboBox = new System.Windows.Forms.ComboBox();
            this.AgeRangeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EmployeeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserNameLabel,
            this.LoggedSinceLabel,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.CallCountLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 564);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(905, 30);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(91, 25);
            this.UserNameLabel.Text = "Nume agent";
            // 
            // LoggedSinceLabel
            // 
            this.LoggedSinceLabel.AutoSize = false;
            this.LoggedSinceLabel.Name = "LoggedSinceLabel";
            this.LoggedSinceLabel.Size = new System.Drawing.Size(110, 25);
            this.LoggedSinceLabel.Text = "De la: 08:50";
            this.LoggedSinceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(69, 25);
            this.toolStripStatusLabel1.Text = "Tel verde";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.ToolTipText = "Tel verde";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(110, 25);
            this.toolStripStatusLabel2.Text = "Apeluri initiate:";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.ToolTipText = "Apeluri initiate";
            // 
            // CallCountLabel
            // 
            this.CallCountLabel.Name = "CallCountLabel";
            this.CallCountLabel.Size = new System.Drawing.Size(17, 25);
            this.CallCountLabel.Text = "0";
            this.CallCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CallCountLabel.ToolTipText = "Apeluri initiate";
            // 
            // CountyTextBox
            // 
            this.CountyTextBox.Location = new System.Drawing.Point(80, 146);
            this.CountyTextBox.Name = "CountyTextBox";
            this.CountyTextBox.Size = new System.Drawing.Size(100, 20);
            this.CountyTextBox.TabIndex = 7;
            // 
            // CallButton
            // 
            this.CallButton.Location = new System.Drawing.Point(581, 17);
            this.CallButton.Name = "CallButton";
            this.CallButton.Size = new System.Drawing.Size(75, 43);
            this.CallButton.TabIndex = 40;
            this.CallButton.Text = "Apeleaza";
            this.CallButton.UseVisualStyleBackColor = true;
            this.CallButton.Click += new System.EventHandler(this.CallButtonClick);
            // 
            // notificationsListBox
            // 
            this.notificationsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notificationsListBox.FormattingEnabled = true;
            this.notificationsListBox.Location = new System.Drawing.Point(581, 94);
            this.notificationsListBox.Name = "notificationsListBox";
            this.notificationsListBox.Size = new System.Drawing.Size(303, 132);
            this.notificationsListBox.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Observatii";
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Location = new System.Drawing.Point(292, 29);
            this.NotesTextBox.Multiline = true;
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.Size = new System.Drawing.Size(240, 164);
            this.NotesTextBox.TabIndex = 9;
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(12, 220);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 30);
            this.SaveButton.TabIndex = 32;
            this.SaveButton.Text = "Salveaza";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // StatusComboBox
            // 
            this.StatusComboBox.FormattingEnabled = true;
            this.StatusComboBox.Location = new System.Drawing.Point(80, 172);
            this.StatusComboBox.Name = "StatusComboBox";
            this.StatusComboBox.Size = new System.Drawing.Size(135, 21);
            this.StatusComboBox.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Status";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Educatie";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Virsta";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Judet";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Localitate";
            // 
            // CityTextBox
            // 
            this.CityTextBox.Location = new System.Drawing.Point(80, 120);
            this.CityTextBox.Name = "CityTextBox";
            this.CityTextBox.Size = new System.Drawing.Size(100, 20);
            this.CityTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Telefon";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(556, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(2, 266);
            this.label10.TabIndex = 44;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(0, 271);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1008, 2);
            this.label11.TabIndex = 45;
            // 
            // HangUpButton
            // 
            this.HangUpButton.Enabled = false;
            this.HangUpButton.Location = new System.Drawing.Point(671, 17);
            this.HangUpButton.Name = "HangUpButton";
            this.HangUpButton.Size = new System.Drawing.Size(75, 43);
            this.HangUpButton.TabIndex = 46;
            this.HangUpButton.Text = "Inchide";
            this.HangUpButton.UseVisualStyleBackColor = true;
            this.HangUpButton.Click += new System.EventHandler(this.HangUpButtonClick);
            // 
            // StatusErrorLabel
            // 
            this.StatusErrorLabel.AutoSize = true;
            this.StatusErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.StatusErrorLabel.Location = new System.Drawing.Point(93, 229);
            this.StatusErrorLabel.Name = "StatusErrorLabel";
            this.StatusErrorLabel.Size = new System.Drawing.Size(145, 13);
            this.StatusErrorLabel.TabIndex = 47;
            this.StatusErrorLabel.Text = "Va rog sa completati statusul!";
            this.StatusErrorLabel.Visible = false;
            // 
            // DocumentRichTextBox
            // 
            this.DocumentRichTextBox.Location = new System.Drawing.Point(15, 288);
            this.DocumentRichTextBox.Name = "DocumentRichTextBox";
            this.DocumentRichTextBox.ReadOnly = true;
            this.DocumentRichTextBox.Size = new System.Drawing.Size(869, 260);
            this.DocumentRichTextBox.TabIndex = 48;
            this.DocumentRichTextBox.Text = "";
            // 
            // AddNumberButton
            // 
            this.AddNumberButton.Location = new System.Drawing.Point(760, 17);
            this.AddNumberButton.Name = "AddNumberButton";
            this.AddNumberButton.Size = new System.Drawing.Size(86, 43);
            this.AddNumberButton.TabIndex = 49;
            this.AddNumberButton.Text = "Adauga numar";
            this.AddNumberButton.UseVisualStyleBackColor = true;
            this.AddNumberButton.Click += new System.EventHandler(this.AddNumberButton_Click);
            // 
            // workDayErrorLabel
            // 
            this.workDayErrorLabel.AutoSize = true;
            this.workDayErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.workDayErrorLabel.Location = new System.Drawing.Point(578, 71);
            this.workDayErrorLabel.Name = "workDayErrorLabel";
            this.workDayErrorLabel.Size = new System.Drawing.Size(133, 13);
            this.workDayErrorLabel.TabIndex = 50;
            this.workDayErrorLabel.Text = "Puteti suna doar intre orele";
            this.workDayErrorLabel.Visible = false;
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(80, 12);
            this.PhoneNumberTextBox.Mask = "4\\0.000.000.000";
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(100, 20);
            this.PhoneNumberTextBox.TabIndex = 51;
            // 
            // EducationComboBox
            // 
            this.EducationComboBox.FormattingEnabled = true;
            this.EducationComboBox.Location = new System.Drawing.Point(80, 66);
            this.EducationComboBox.Name = "EducationComboBox";
            this.EducationComboBox.Size = new System.Drawing.Size(135, 21);
            this.EducationComboBox.TabIndex = 52;
            // 
            // AgeRangeComboBox
            // 
            this.AgeRangeComboBox.FormattingEnabled = true;
            this.AgeRangeComboBox.Location = new System.Drawing.Point(80, 39);
            this.AgeRangeComboBox.Name = "AgeRangeComboBox";
            this.AgeRangeComboBox.Size = new System.Drawing.Size(135, 21);
            this.AgeRangeComboBox.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Tip Angajat";
            // 
            // EmployeeTypeComboBox1
            // 
            this.EmployeeTypeComboBox.FormattingEnabled = true;
            this.EmployeeTypeComboBox.Location = new System.Drawing.Point(80, 93);
            this.EmployeeTypeComboBox.Name = "EmployeeTypeComboBox1";
            this.EmployeeTypeComboBox.Size = new System.Drawing.Size(135, 21);
            this.EmployeeTypeComboBox.TabIndex = 55;
            // 
            // CallAgent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 594);
            this.Controls.Add(this.EmployeeTypeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AgeRangeComboBox);
            this.Controls.Add(this.EducationComboBox);
            this.Controls.Add(this.PhoneNumberTextBox);
            this.Controls.Add(this.workDayErrorLabel);
            this.Controls.Add(this.AddNumberButton);
            this.Controls.Add(this.DocumentRichTextBox);
            this.Controls.Add(this.StatusErrorLabel);
            this.Controls.Add(this.HangUpButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CountyTextBox);
            this.Controls.Add(this.CallButton);
            this.Controls.Add(this.notificationsListBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.StatusComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CityTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(638, 476);
            this.Name = "CallAgent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desktop Agent";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CallAgent_FormClosed);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel UserNameLabel;
        private System.Windows.Forms.ToolStripStatusLabel LoggedSinceLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel CallCountLabel;
        private System.Windows.Forms.TextBox CountyTextBox;
        private System.Windows.Forms.Button CallButton;
        private System.Windows.Forms.ListBox notificationsListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NotesTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ComboBox StatusComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CityTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button HangUpButton;
        private System.Windows.Forms.Label StatusErrorLabel;
        private System.Windows.Forms.RichTextBox DocumentRichTextBox;
        private System.Windows.Forms.Button AddNumberButton;
        private System.Windows.Forms.Label workDayErrorLabel;
        private System.Windows.Forms.MaskedTextBox PhoneNumberTextBox;
        private System.Windows.Forms.ComboBox EducationComboBox;
        private System.Windows.Forms.ComboBox AgeRangeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox EmployeeTypeComboBox;
    }
}

