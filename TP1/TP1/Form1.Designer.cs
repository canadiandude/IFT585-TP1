namespace TP1
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
            this.LBX_Emetteur = new System.Windows.Forms.ListBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LBX_Support = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LBX_Recepteur = new System.Windows.Forms.ListBox();
            this.BTN_Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBX_Emetteur
            // 
            this.LBX_Emetteur.FormattingEnabled = true;
            this.LBX_Emetteur.Location = new System.Drawing.Point(12, 25);
            this.LBX_Emetteur.Name = "LBX_Emetteur";
            this.LBX_Emetteur.Size = new System.Drawing.Size(169, 173);
            this.LBX_Emetteur.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(49, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Émetteur";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Support";
            // 
            // LBX_Support
            // 
            this.LBX_Support.FormattingEnabled = true;
            this.LBX_Support.Location = new System.Drawing.Point(187, 25);
            this.LBX_Support.Name = "LBX_Support";
            this.LBX_Support.Size = new System.Drawing.Size(165, 173);
            this.LBX_Support.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(355, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Récepteur";
            // 
            // LBX_Recepteur
            // 
            this.LBX_Recepteur.FormattingEnabled = true;
            this.LBX_Recepteur.Location = new System.Drawing.Point(358, 25);
            this.LBX_Recepteur.Name = "LBX_Recepteur";
            this.LBX_Recepteur.Size = new System.Drawing.Size(180, 173);
            this.LBX_Recepteur.TabIndex = 4;
            // 
            // BTN_Start
            // 
            this.BTN_Start.Location = new System.Drawing.Point(463, 205);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(75, 23);
            this.BTN_Start.TabIndex = 7;
            this.BTN_Start.Text = "START !";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 240);
            this.Controls.Add(this.BTN_Start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LBX_Recepteur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LBX_Support);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.LBX_Emetteur);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LBX_Emetteur;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LBX_Support;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox LBX_Recepteur;
        private System.Windows.Forms.Button BTN_Start;
    }
}

