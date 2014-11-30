namespace ThortonsHL7
{
    partial class ServiceChooser
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPurchaseTotaller = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thorton\'s XXX";
            // 
            // buttonPurchaseTotaller
            // 
            this.buttonPurchaseTotaller.Location = new System.Drawing.Point(82, 93);
            this.buttonPurchaseTotaller.Name = "buttonPurchaseTotaller";
            this.buttonPurchaseTotaller.Size = new System.Drawing.Size(108, 59);
            this.buttonPurchaseTotaller.TabIndex = 1;
            this.buttonPurchaseTotaller.Text = "Purchase Totaller";
            this.buttonPurchaseTotaller.UseVisualStyleBackColor = true;
            this.buttonPurchaseTotaller.Click += new System.EventHandler(this.buttonPurchaseTotaller_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServiceChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 209);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonPurchaseTotaller);
            this.Controls.Add(this.label1);
            this.Name = "ServiceChooser";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPurchaseTotaller;
        private System.Windows.Forms.Button button1;

    }
}

