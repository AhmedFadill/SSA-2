using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSA_2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }

        private void Done_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox_Name.Text))
            {
                MessageBox.Show("يجب ملى حقل الاسم ");
                return;
            }
            if (string.IsNullOrEmpty(textBox_CardNumber.Text))
            {
                MessageBox.Show("يجب ملى حقل رقم البطاقة ");
                return;
            }
            MessageBox.Show("Done");
            this.Close();
            
        }
    }
}
