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
    public partial class reports : UserControl
    {
        public reports()
        {
            InitializeComponent();
            kryptonDataGridView1.Rows.Add("احمد فاضل لفته", "0", "", "حاضر", "حاضر", "حاضر");
            kryptonDataGridView1.Rows.Add("علي قاسم محمد", "1", "تنبيه", "حاضر", "حاضر", "غائب");
            kryptonDataGridView1.Rows.Add("عقيل علي خلف", "3", "انذار ثاني", "غائب", "غائب", "غائب");
            kryptonDataGridView1.Rows.Add("محمد علي زيد", "2", "انذار اولي", "غائب", "غائب", "حاضر");
            kryptonDataGridView1.Rows.Add("مهدي حيدر محمد", "1", "تنبيه", "غائب", "حاضر", "حاضر");
            kryptonDataGridView1.Rows.Add("عباس عزيز كتاب", "0", "", "حاضر", "حاضر", "حاضر");
            kryptonDataGridView1.Rows.Add("حيدر حسن كميل", "0", "", "حاضر", "حاضر", "حاضر");
            kryptonDataGridView1.Rows.Add("علي مهدي مظلوم", "3", "انذار ثاني", "غائب", "غائب", "غائب");
            kryptonDataGridView1.Rows.Add("محمد منتظر حسام", "2", "انذار اولي", "غائب", "غائب", "حاضر");
            kryptonDataGridView1.Rows.Add("عباس مهدي", "1", "تنبيه", "غائب", "حاضر", "حاضر");
            kryptonDataGridView1.Rows.Add("محمد رضا هشام", "0", "", "حاضر", "حاضر", "حاضر");
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
