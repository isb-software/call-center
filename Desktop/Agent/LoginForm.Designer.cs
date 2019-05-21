namespace Agent
{
    partial class LoginForm
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
            this.UsersComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.InboundRadioButton = new System.Windows.Forms.RadioButton();
            this.OutboundRadioButton = new System.Windows.Forms.RadioButton();
            this.legalHolidayLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UsersComboBox
            // 
            this.UsersComboBox.FormattingEnabled = true;
            this.UsersComboBox.Location = new System.Drawing.Point(182, 100);
            this.UsersComboBox.Name = "UsersComboBox";
            this.UsersComboBox.Size = new System.Drawing.Size(188, 21);
            this.UsersComboBox.TabIndex = 0;
            this.UsersComboBox.SelectedIndexChanged += new System.EventHandler(this.UsersComboBox_SelectedIndexChanged);
            this.UsersComboBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.UsersComboBoxFormat);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selectati Utilizatorul";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(182, 150);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(188, 23);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "Autentificare";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
            // 
            // InboundRadioButton
            // 
            this.InboundRadioButton.AutoSize = true;
            this.InboundRadioButton.Location = new System.Drawing.Point(306, 127);
            this.InboundRadioButton.Name = "InboundRadioButton";
            this.InboundRadioButton.Size = new System.Drawing.Size(64, 17);
            this.InboundRadioButton.TabIndex = 3;
            this.InboundRadioButton.Text = "Inbound";
            this.InboundRadioButton.UseVisualStyleBackColor = true;
            // 
            // OutboundRadioButton
            // 
            this.OutboundRadioButton.AutoSize = true;
            this.OutboundRadioButton.Checked = true;
            this.OutboundRadioButton.Location = new System.Drawing.Point(182, 127);
            this.OutboundRadioButton.Name = "OutboundRadioButton";
            this.OutboundRadioButton.Size = new System.Drawing.Size(72, 17);
            this.OutboundRadioButton.TabIndex = 4;
            this.OutboundRadioButton.TabStop = true;
            this.OutboundRadioButton.Text = "Outbound";
            this.OutboundRadioButton.UseVisualStyleBackColor = true;
            // 
            // legalHolidayLabel
            // 
            this.legalHolidayLabel.AutoSize = true;
            this.legalHolidayLabel.ForeColor = System.Drawing.Color.Red;
            this.legalHolidayLabel.Location = new System.Drawing.Point(207, 176);
            this.legalHolidayLabel.Name = "legalHolidayLabel";
            this.legalHolidayLabel.Size = new System.Drawing.Size(136, 13);
            this.legalHolidayLabel.TabIndex = 5;
            this.legalHolidayLabel.Text = "Astazi este zi de sarbatoare";
            this.legalHolidayLabel.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 343);
            this.Controls.Add(this.legalHolidayLabel);
            this.Controls.Add(this.OutboundRadioButton);
            this.Controls.Add(this.InboundRadioButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsersComboBox);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox UsersComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.RadioButton InboundRadioButton;
        private System.Windows.Forms.RadioButton OutboundRadioButton;
        private System.Windows.Forms.Label legalHolidayLabel;
    }
}