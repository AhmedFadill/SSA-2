namespace SSA_2
{
    partial class Form3
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
            this.addStage1 = new SSA_2.AddStage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // addStage1
            // 
            this.addStage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addStage1.Location = new System.Drawing.Point(0, 0);
            this.addStage1.Name = "addStage1";
            this.addStage1.Size = new System.Drawing.Size(991, 595);
            this.addStage1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Location = new System.Drawing.Point(334, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 510);
            this.panel2.TabIndex = 2;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 595);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.addStage1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private AddStage addStage1;
        private System.Windows.Forms.Panel panel2;
    }
}