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
        private string Id = null,Info=null;
        public Form2(string id = null,string info=null)
        {
            InitializeComponent();
            Id = id;
            Info = info;
            if (id != null)
            {
                DataTable data = DB_Functions.Load_data("select Name,cardNum,Note from students where id =" + id);
                if (data != null)
                {
                    textBox_Name.Text = data.Rows[0][0].ToString();
                    textBox_CardNumber.Text = data.Rows[0][1].ToString();
                    textBoxNote.Text = data.Rows[0][2].ToString();
                }
            }
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
            if (Id == null)
                 DB_Functions.Execute("insert into students([Name],[cardNum],[studyInformationID],[Note]) values ('" + textBox_Name.Text + "'," + textBox_CardNumber.Text + "," + Info + ",'" + textBoxNote.Text + "')");
            else
                DB_Functions.Execute("update students set Name = '" + textBox_Name.Text + "' , cardNum = " + textBox_CardNumber.Text + " , Note='" + textBoxNote.Text + "' where id = " + Id);
            MessageBox.Show("Done");
            this.Close();
            
        }
    }
}
