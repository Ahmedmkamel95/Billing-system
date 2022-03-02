namespace Billing_ADSL
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sheetNameTextBox = new System.Windows.Forms.TextBox();
            this.saveToTextBox = new System.Windows.Forms.TextBox();
            this.saveToButton = new System.Windows.Forms.Button();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.counterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ExcelSheetDataGridView = new System.Windows.Forms.DataGridView();
            this.loadExcelSheetButton = new System.Windows.Forms.Button();
            this.loadFromTextBox = new System.Windows.Forms.TextBox();
            this.loadFromButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ADSLButton = new System.Windows.Forms.Button();
            this.breakButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.billingCounterLabel = new System.Windows.Forms.Label();
            this.newLoginLinkLabel = new System.Windows.Forms.LinkLabel();
            this.billingADSLButton = new System.Windows.Forms.Button();
            this.billingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ExcelSheetDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "ex. Sheet1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Sheet Name";
            // 
            // sheetNameTextBox
            // 
            this.sheetNameTextBox.Location = new System.Drawing.Point(205, 113);
            this.sheetNameTextBox.Name = "sheetNameTextBox";
            this.sheetNameTextBox.Size = new System.Drawing.Size(188, 20);
            this.sheetNameTextBox.TabIndex = 2;
            // 
            // saveToTextBox
            // 
            this.saveToTextBox.Enabled = false;
            this.saveToTextBox.Location = new System.Drawing.Point(205, 74);
            this.saveToTextBox.Name = "saveToTextBox";
            this.saveToTextBox.Size = new System.Drawing.Size(266, 20);
            this.saveToTextBox.TabIndex = 37;
            // 
            // saveToButton
            // 
            this.saveToButton.Location = new System.Drawing.Point(59, 74);
            this.saveToButton.Name = "saveToButton";
            this.saveToButton.Size = new System.Drawing.Size(75, 23);
            this.saveToButton.TabIndex = 1;
            this.saveToButton.Text = "Save To";
            this.saveToButton.UseVisualStyleBackColor = true;
            this.saveToButton.Click += new System.EventHandler(this.saveToButton_Click);
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Location = new System.Drawing.Point(123, 494);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(49, 13);
            this.elapsedTimeLabel.TabIndex = 35;
            this.elapsedTimeLabel.Text = "00:00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 494);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Elapsed Time";
            // 
            // counterLabel
            // 
            this.counterLabel.AutoSize = true;
            this.counterLabel.Location = new System.Drawing.Point(457, 470);
            this.counterLabel.Name = "counterLabel";
            this.counterLabel.Size = new System.Drawing.Size(24, 13);
            this.counterLabel.TabIndex = 33;
            this.counterLabel.Text = "0/0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "ADSL Counter";
            // 
            // ExcelSheetDataGridView
            // 
            this.ExcelSheetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExcelSheetDataGridView.Location = new System.Drawing.Point(11, 195);
            this.ExcelSheetDataGridView.Name = "ExcelSheetDataGridView";
            this.ExcelSheetDataGridView.Size = new System.Drawing.Size(492, 216);
            this.ExcelSheetDataGridView.TabIndex = 30;
            // 
            // loadExcelSheetButton
            // 
            this.loadExcelSheetButton.Location = new System.Drawing.Point(223, 157);
            this.loadExcelSheetButton.Name = "loadExcelSheetButton";
            this.loadExcelSheetButton.Size = new System.Drawing.Size(75, 23);
            this.loadExcelSheetButton.TabIndex = 4;
            this.loadExcelSheetButton.Text = "Load";
            this.loadExcelSheetButton.UseVisualStyleBackColor = true;
            this.loadExcelSheetButton.Click += new System.EventHandler(this.loadExcelSheetButton_Click);
            // 
            // loadFromTextBox
            // 
            this.loadFromTextBox.Enabled = false;
            this.loadFromTextBox.Location = new System.Drawing.Point(205, 35);
            this.loadFromTextBox.Name = "loadFromTextBox";
            this.loadFromTextBox.Size = new System.Drawing.Size(266, 20);
            this.loadFromTextBox.TabIndex = 28;
            // 
            // loadFromButton
            // 
            this.loadFromButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.loadFromButton.Location = new System.Drawing.Point(59, 35);
            this.loadFromButton.Name = "loadFromButton";
            this.loadFromButton.Size = new System.Drawing.Size(75, 23);
            this.loadFromButton.TabIndex = 0;
            this.loadFromButton.Text = "Load From";
            this.loadFromButton.UseVisualStyleBackColor = true;
            this.loadFromButton.Click += new System.EventHandler(this.loadFromButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ADSLButton
            // 
            this.ADSLButton.Location = new System.Drawing.Point(371, 429);
            this.ADSLButton.Name = "ADSLButton";
            this.ADSLButton.Size = new System.Drawing.Size(100, 23);
            this.ADSLButton.TabIndex = 7;
            this.ADSLButton.Text = "ADSL";
            this.ADSLButton.UseVisualStyleBackColor = true;
            this.ADSLButton.Click += new System.EventHandler(this.ADSLButton_Click);
            // 
            // breakButton
            // 
            this.breakButton.Location = new System.Drawing.Point(218, 470);
            this.breakButton.Name = "breakButton";
            this.breakButton.Size = new System.Drawing.Size(98, 37);
            this.breakButton.TabIndex = 8;
            this.breakButton.Text = "BREAK";
            this.breakButton.UseVisualStyleBackColor = true;
            this.breakButton.Click += new System.EventHandler(this.breakButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 494);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "Billing Counter";
            // 
            // billingCounterLabel
            // 
            this.billingCounterLabel.AutoSize = true;
            this.billingCounterLabel.Location = new System.Drawing.Point(457, 494);
            this.billingCounterLabel.Name = "billingCounterLabel";
            this.billingCounterLabel.Size = new System.Drawing.Size(24, 13);
            this.billingCounterLabel.TabIndex = 45;
            this.billingCounterLabel.Text = "0/0";
            // 
            // newLoginLinkLabel
            // 
            this.newLoginLinkLabel.AutoSize = true;
            this.newLoginLinkLabel.Location = new System.Drawing.Point(427, 167);
            this.newLoginLinkLabel.Name = "newLoginLinkLabel";
            this.newLoginLinkLabel.Size = new System.Drawing.Size(58, 13);
            this.newLoginLinkLabel.TabIndex = 46;
            this.newLoginLinkLabel.TabStop = true;
            this.newLoginLinkLabel.Text = "New Login";
            this.newLoginLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newLoginLinkLabel_LinkClicked);
            // 
            // billingADSLButton
            // 
            this.billingADSLButton.Location = new System.Drawing.Point(216, 429);
            this.billingADSLButton.Name = "billingADSLButton";
            this.billingADSLButton.Size = new System.Drawing.Size(100, 23);
            this.billingADSLButton.TabIndex = 6;
            this.billingADSLButton.Text = "Billing + ADSL";
            this.billingADSLButton.UseVisualStyleBackColor = true;
            this.billingADSLButton.Click += new System.EventHandler(this.billingADSLButton_Click);
            // 
            // billingButton
            // 
            this.billingButton.Location = new System.Drawing.Point(59, 429);
            this.billingButton.Name = "billingButton";
            this.billingButton.Size = new System.Drawing.Size(100, 23);
            this.billingButton.TabIndex = 5;
            this.billingButton.Text = "Billing";
            this.billingButton.UseVisualStyleBackColor = true;
            this.billingButton.Click += new System.EventHandler(this.billingButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 519);
            this.Controls.Add(this.newLoginLinkLabel);
            this.Controls.Add(this.billingCounterLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.breakButton);
            this.Controls.Add(this.ADSLButton);
            this.Controls.Add(this.billingButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sheetNameTextBox);
            this.Controls.Add(this.saveToTextBox);
            this.Controls.Add(this.saveToButton);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.counterLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.billingADSLButton);
            this.Controls.Add(this.ExcelSheetDataGridView);
            this.Controls.Add(this.loadExcelSheetButton);
            this.Controls.Add(this.loadFromTextBox);
            this.Controls.Add(this.loadFromButton);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Billing/ADSL";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ExcelSheetDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sheetNameTextBox;
        private System.Windows.Forms.TextBox saveToTextBox;
        private System.Windows.Forms.Button saveToButton;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label counterLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView ExcelSheetDataGridView;
        private System.Windows.Forms.Button loadExcelSheetButton;
        private System.Windows.Forms.TextBox loadFromTextBox;
        private System.Windows.Forms.Button loadFromButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ADSLButton;
        private System.Windows.Forms.Button breakButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label billingCounterLabel;
        private System.Windows.Forms.LinkLabel newLoginLinkLabel;
        private System.Windows.Forms.Button billingADSLButton;
        private System.Windows.Forms.Button billingButton;
    }
}

