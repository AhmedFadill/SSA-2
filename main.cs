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
    public partial class main : UserControl
    {
        public main()
        {
            InitializeComponent();
            table_mainscreen.Rows.Add("احمد فاضل لفته", "A", "123", true, "لا يوجد");
            table_mainscreen.Rows.Add("علي قاسم محمد", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("عقيل علي خلف", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("محمد علي زيد", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("مهدي حيدر محمد", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("عباس عزيز كتاب", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("حيدر حسن كميل", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("علي مهدي مظلوم", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("محمد منتظر حسام", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("عباس مهدي", "A", "123", false, "لا يوجد");
            table_mainscreen.Rows.Add("محمد رضا هشام", "A", "123", false, "لا يوجد");
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            camera_off.Visible = false;
            camera_on.Visible = true;
            
        }

        private void camera_on_Click(object sender, EventArgs e)
        {
            camera_off.Visible = true;
            camera_on.Visible = false;
            
        }
    }
}
