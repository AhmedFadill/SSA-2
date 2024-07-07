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
           dataGridView1.DataSource= FunctionsDataBase.view_table("stages");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) { }
            if (FunctionsDataBase.add_element("stages", "name,type", $"'{textBox1.Text}',{textBox2.Text}"))
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
            if (FunctionsDataBase.updata_element("stages", $"name='{textBox1.Text}'", "id="+textBox2.Text))
            {
                MessageBox.Show("added done");
                Test_Load(null, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("can't add");
            }
        }
    }
}
