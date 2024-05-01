using System;
using System.Data;
using System.Windows.Forms;

namespace SSA_2
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        static public string id;
        

        private void kryptonTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                loginButton_Click(null, null);
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            DataTable dt = DB_Functions.Load_data("select id from teachers where Email='"+kryptonTextBoxEmail.Text+"' and Password='"+kryptonTextBoxPassword.Text+"'");
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0][0].ToString();
                Form1 f= new Form1();
                Hide();
                f.Closed += (z, a) => this.Close();
                f.Show();
            }
            else 
                MessageBox.Show("خطا في البريد او كلمة السر","خطا في الدخال المعلومات",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            kryptonTextBoxPassword.PasswordChar=kryptonCheckBox1.CheckState==CheckState.Checked?'\0':'*';
        }
    }
}
