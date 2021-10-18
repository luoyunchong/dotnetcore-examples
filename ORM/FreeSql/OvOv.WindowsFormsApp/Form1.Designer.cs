
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textKey = new System.Windows.Forms.TextBox();
            this.textValue = new System.Windows.Forms.TextBox();
            this.ConfigKey = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelId = new System.Windows.Forms.Label();
            this.textId = new System.Windows.Forms.TextBox();
            this.textCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "add or update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textKey
            // 
            this.textKey.Location = new System.Drawing.Point(241, 83);
            this.textKey.Name = "textKey";
            this.textKey.Size = new System.Drawing.Size(221, 23);
            this.textKey.TabIndex = 1;
            // 
            // textValue
            // 
            this.textValue.Location = new System.Drawing.Point(241, 134);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(221, 23);
            this.textValue.TabIndex = 2;
            // 
            // ConfigKey
            // 
            this.ConfigKey.AutoSize = true;
            this.ConfigKey.Location = new System.Drawing.Point(162, 87);
            this.ConfigKey.Name = "ConfigKey";
            this.ConfigKey.Size = new System.Drawing.Size(71, 17);
            this.ConfigKey.TabIndex = 3;
            this.ConfigKey.Text = "Config Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Config Value";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(133, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 26);
            this.button2.TabIndex = 5;
            this.button2.Text = "query";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(153, 39);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(71, 17);
            this.labelId.TabIndex = 6;
            this.labelId.Text = "Config Key";
            // 
            // textId
            // 
            this.textId.Location = new System.Drawing.Point(241, 36);
            this.textId.Name = "textId";
            this.textId.Size = new System.Drawing.Size(221, 23);
            this.textId.TabIndex = 7;
            // 
            // textCount
            // 
            this.textCount.Location = new System.Drawing.Point(241, 182);
            this.textCount.Name = "textCount";
            this.textCount.Size = new System.Drawing.Size(221, 23);
            this.textCount.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Count";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textCount);
            this.Controls.Add(this.textId);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConfigKey);
            this.Controls.Add(this.textValue);
            this.Controls.Add(this.textKey);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textKey;
        private System.Windows.Forms.TextBox textValue;
        private System.Windows.Forms.Label ConfigKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox textId;
        private System.Windows.Forms.TextBox textCount;
        private System.Windows.Forms.Label label2;
    }
}

