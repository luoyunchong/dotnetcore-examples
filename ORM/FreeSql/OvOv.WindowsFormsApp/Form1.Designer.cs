
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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "add config";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textKey
            // 
            this.textKey.Location = new System.Drawing.Point(246, 17);
            this.textKey.Name = "textKey";
            this.textKey.Size = new System.Drawing.Size(221, 23);
            this.textKey.TabIndex = 1;
            // 
            // textValue
            // 
            this.textValue.Location = new System.Drawing.Point(246, 62);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(221, 23);
            this.textValue.TabIndex = 2;
            // 
            // ConfigKey
            // 
            this.ConfigKey.AutoSize = true;
            this.ConfigKey.Location = new System.Drawing.Point(167, 20);
            this.ConfigKey.Name = "ConfigKey";
            this.ConfigKey.Size = new System.Drawing.Size(65, 15);
            this.ConfigKey.TabIndex = 3;
            this.ConfigKey.Text = "Config Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Config Value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

