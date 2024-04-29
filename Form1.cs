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
            kryptonDataGridView1.Rows.Add("احمد فاضل لفته", "A", "123", true, "لا يوجد");
            kryptonDataGridView1.Rows.Add("علي قاسم محمد", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("عقيل علي خلف", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد علي زيد", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("مهدي حيدر محمد", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("عباس عزيز كتاب", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("حيدر حسن كميل", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("علي مهدي مظلوم", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد منتظر حسام", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("عباس مهدي", "A", "123", false, "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد رضا هشام", "A", "123", false, "لا يوجد");
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            camera_off.Visible = false;
            camera_on.Visible = true;
            camera_barcode.Visible = true;
            name_student.Text = "احمد فاضل لفته كاظم";
        }

        private void camera_on_Click(object sender, EventArgs e)
        {
            camera_off.Visible = true;
            camera_on.Visible = false;
            camera_barcode.Visible = false;
        }
    }
}
