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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SideBarControll(1);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel4.Controls.Clear();
            panel4.Controls.Add(userControl);
            userControl.BringToFront();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SideBarControll(int num)
        {
            panelMainScreem.BackColor = num == 1 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShow.BackColor = num == 2 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelListStudent.BackColor = num == 3 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShowReport.BackColor = num == 4 ? Color.FromArgb(235, 239, 255) : Color.Transparent;

            labelMainScreen.ForeColor = num == 1 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShow.ForeColor = num == 2 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelListStudent.ForeColor = num == 3 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShowReport.ForeColor = num == 4 ? Color.FromArgb(100, 119, 219) : Color.Black;

            //table_mainscreen.Visible = num == 1 ? true : false;
            //filteer_mainscreen.Visible = num == 1 ? true : false;
            if (num == 1)
            {
                main m= new main();
                addUserControl(m);
            }
            else if (num == 2)
            {
                Show s = new Show();
                addUserControl(s);
            }else if (num == 3)
            {
                control c = new control();
                addUserControl(c);
            }else if(num == 4)
            {
                reports r = new reports();
                addUserControl(r);
            }
            //show1.Visible = num == 2 ? true : false;

            //control1.Visible = num == 3 ? true : false;

            //reports1.Visible = num == 4 ? true : false;
        }

        private void panelMainScreem_Click(object sender, EventArgs e)
        {
            SideBarControll(1);
        }

        private void panelShow_Click(object sender, EventArgs e)
        {
            SideBarControll(2);
            
        }

        private void panelListStudent_Click(object sender, EventArgs e)
        {
            SideBarControll(3);

        }

        private void panelShowReport_Click(object sender, EventArgs e)
        {
            SideBarControll(4);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
