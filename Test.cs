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
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource= FunctionsDataBase.view_table("students");
            controller.setStage(ref kryptonComboBox4);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (controller.editStudent(textBox1.Text, kryptonComboBox1.SelectedValue.ToString(), numericUpDown1.Value.ToString(),textBox2.Text,textBox3.Text))
            {
                MessageBox.Show("added done");
                Test_Load(null,EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("can't add");
            }

        }

        private void kryptonComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setType(ref kryptonComboBox3, ref kryptonComboBox4);
        }

        private void kryptonComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setDivision(ref kryptonComboBox2, ref kryptonComboBox3);
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setGroup(ref kryptonComboBox1,ref kryptonComboBox2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(controller.deleteStudent(textBox3.Text))
                MessageBox.Show("Delete Successfully");
            else
            {
                MessageBox.Show("Delete faild");
            }
            Test_Load(null, EventArgs.Empty);

        }
    }
}
