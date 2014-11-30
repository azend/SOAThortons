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
            this.buttonRegisterTeam = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxTeamName = new System.Windows.Forms.TextBox();
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
            this.buttonPurchaseTotaller.Enabled = false;
            this.buttonPurchaseTotaller.Location = new System.Drawing.Point(83, 125);
            this.buttonPurchaseTotaller.Name = "buttonPurchaseTotaller";
            this.buttonPurchaseTotaller.Size = new System.Drawing.Size(111, 42);
            this.buttonPurchaseTotaller.TabIndex = 1;
            this.buttonPurchaseTotaller.Text = "Purchase Totaller";
            this.buttonPurchaseTotaller.UseVisualStyleBackColor = true;
            this.buttonPurchaseTotaller.Click += new System.EventHandler(this.buttonPurchaseTotaller_Click);
            // 
            // buttonRegisterTeam
            // 
            this.buttonRegisterTeam.Location = new System.Drawing.Point(83, 95);
            this.buttonRegisterTeam.Name = "buttonRegisterTeam";
            this.buttonRegisterTeam.Size = new System.Drawing.Size(110, 24);
            this.buttonRegisterTeam.TabIndex = 2;
            this.buttonRegisterTeam.Text = "Register Team";
            this.buttonRegisterTeam.UseVisualStyleBackColor = true;
            this.buttonRegisterTeam.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(83, 173);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(111, 24);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // textBoxTeamName
            // 
            this.textBoxTeamName.Location = new System.Drawing.Point(83, 69);
            this.textBoxTeamName.Name = "textBoxTeamName";
            this.textBoxTeamName.Size = new System.Drawing.Size(110, 20);
            this.textBoxTeamName.TabIndex = 4;
            // 
            // ServiceChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 209);
            this.Controls.Add(this.textBoxTeamName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonRegisterTeam);
            this.Controls.Add(this.buttonPurchaseTotaller);
            this.Controls.Add(this.label1);
            this.Name = "ServiceChooser";
            this.Text = "ServiceChooser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPurchaseTotaller;
        private System.Windows.Forms.Button buttonRegisterTeam;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textBoxTeamName;

    }
}

