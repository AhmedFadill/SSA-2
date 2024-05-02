namespace SSA_2
{
    partial class Form2
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
            this.labelNote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label_user = new System.Windows.Forms.Label();
            this.textBox_CardNumber = new System.Windows.Forms.TextBox();
            this.Done = new System.Windows.Forms.Button();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Font = new System.Drawing.Font("Dubai", 9.749999F);
            this.labelNote.Location = new System.Drawing.Point(159, 126);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(52, 22);
            this.labelNote.TabIndex = 21;
            this.labelNote.Text = "الملاحظة\r\n";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(26, 151);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(185, 20);
            this.textBoxNote.TabIndex = 17;
            // 
            // label_user
            // 
            this.label_user.AutoSize = true;
            this.label_user.Font = new System.Drawing.Font("Dubai", 9.749999F);
            this.label_user.Location = new System.Drawing.Point(155, 66);
            this.label_user.Name = "label_user";
            this.label_user.Size = new System.Drawing.Size(56, 22);
            this.label_user.TabIndex = 19;
            this.label_user.Text = "رقم بطاقة";
            // 
            // textBox_CardNumber
            // 
            this.textBox_CardNumber.Location = new System.Drawing.Point(26, 91);
            this.textBox_CardNumber.Name = "textBox_CardNumber";
            this.textBox_CardNumber.Size = new System.Drawing.Size(185, 20);
            this.textBox_CardNumber.TabIndex = 16;
            // 
            // Done
            // 
            this.Done.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
            this.Done.Font = new System.Drawing.Font("Dubai", 9.749999F);
            this.Done.ForeColor = System.Drawing.Color.White;
            this.Done.Location = new System.Drawing.Point(57, 187);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(131, 40);
            this.Done.TabIndex = 18;
            this.Done.Text = "الانتهاء";
            this.Done.UseVisualStyleBackColor = false;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Dubai", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.Location = new System.Drawing.Point(147, 9);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(64, 22);
            this.label_name.TabIndex = 25;
            this.label_name.Text = "اسم الطالب";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(26, 34);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(185, 20);
            this.textBox_Name.TabIndex = 15;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 250);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.label_user);
            this.Controls.Add(this.textBox_CardNumber);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.textBox_Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label_user;
        private System.Windows.Forms.TextBox textBox_CardNumber;
        private System.Windows.Forms.Button Done;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_Name;
    }
}