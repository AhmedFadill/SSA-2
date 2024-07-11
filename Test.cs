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
           dataGridView1.DataSource= FunctionsDataBase.view_table("cd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FunctionsDataBase.add_element("divisions", "name,type", $"'{textBox1.Text}',{textBox2.Text}"))
            {
                MessageBox.Show("added done");
                Test_Load(null,EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("can't add");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FunctionsDataBase.updata_element("stages", $"name='{textBox1.Text}',type={textBox2.Text}"
                , "id="+textBox3.Text))
            {
                MessageBox.Show("added done");
                Test_Load(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("can't add");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FunctionsDataBase.view_table(textBox5.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.SelectedValue.ToString();
        }

       

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox2.SelectedValue.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value=comboBox3.SelectedValue.ToString();
            label3.Text = value;
            dataGridView1.DataSource=FunctionsDataBase.view_table("students",$" group_id={value}");

        }
    }
}
