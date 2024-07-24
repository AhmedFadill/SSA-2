using ClosedXML.Excel;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SSA_2
{
    public partial class Show : UserControl
    {
        public Show()
        {
            InitializeComponent();
        }
        
        

       

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
            (table_show.DataSource as DataTable).DefaultView.RowFilter = string.Format("[اسم الطالب] like '" + kryptonTextBox3.Text + "%'");

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (table_show.DataSource as DataTable);

            using (SaveFileDialog std = new SaveFileDialog() { Filter = "Excel|*.xlsx" })
            {
                std.FileName = "تقرير  " + kryptonComboBoxType.Text;
                if (std.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wk = new XLWorkbook())
                        {
                            wk.Worksheets.Add(dt, "التقرير");
                            wk.SaveAs(std.FileName);
                        }
                        MessageBox.Show(".تم تحويل البيانات الى ملف اكسل بنجاح");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void kryptonComboBoxStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setType(ref kryptonComboBoxType, ref kryptonComboBoxStage);

        }

        private void kryptonComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setDivision(ref kryptonComboBoxDiv, ref kryptonComboBoxType);

        }

        private void kryptonComboBoxDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setGroup(ref kryptonComboBoxGro, ref kryptonComboBoxDiv);

        }

        private void kryptonComboBoxGro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Show_Load(object sender, EventArgs e)
        {
            controller.setStage(ref kryptonComboBoxStage);

        }
    }
}
