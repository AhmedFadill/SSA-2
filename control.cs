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
    public partial class control : UserControl
    {
        public control()
        {
            InitializeComponent();
            kryptonDataGridView1.Rows.Add("احمد فاضل لفته", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("علي قاسم محمد", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("عقيل علي خلف", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد علي زيد", "A", "123",  "لا يوجد");
            kryptonDataGridView1.Rows.Add("مهدي حيدر محمد", "A", "123",  "لا يوجد");
            kryptonDataGridView1.Rows.Add("عباس عزيز كتاب", "A", "123",  "لا يوجد");
            kryptonDataGridView1.Rows.Add("حيدر حسن كميل", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("علي مهدي مظلوم", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد منتظر حسام", "A", "123", "لا يوجد");
            kryptonDataGridView1.Rows.Add("عباس مهدي", "A", "123",  "لا يوجد");
            kryptonDataGridView1.Rows.Add("محمد رضا هشام", "A", "123",  "لا يوجد");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
