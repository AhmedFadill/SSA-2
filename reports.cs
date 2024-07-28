using ClosedXML.Excel;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Windows.Forms;

namespace SSA_2
{
    public partial class reports : UserControl
    {
        public reports()
        {
            InitializeComponent();
            
        }


        
        

       

        private void kryptonComboBoxLes_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            (kryptonDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] like '" + kryptonTextBox1.Text + "%'");

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (kryptonDataGridView1.DataSource as DataTable);
            dt.PrimaryKey = null;
            dt.Columns.Remove("id");
            dt.Columns["Name"].ColumnName = "اسم الطالب";
            dt.Columns["Totle"].ColumnName = "مجموع الغياب";
            dt.Columns["Warn"].ColumnName = "الحالة";

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

        

        private void reports_Load(object sender, EventArgs e)
        {
            controller.setStage(ref kryptonComboBoxStage);
            kryptonDataGridView1.Columns["id"].Visible = false;
            kryptonDataGridView1.Columns["Totle"].HeaderText = "مجموع الغياب";
            kryptonDataGridView1.Columns["Name"].HeaderText = "اسم الطالب";
            kryptonDataGridView1.Columns["Warn"].HeaderText = "الحالة";


        }
        private void kryptonComboBoxStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setType(ref kryptonComboBoxType, ref kryptonComboBoxStage);

        }

        private void kryptonComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setDivision(ref kryptonComboBoxDiv, ref kryptonComboBoxType);
            controller.setLecture(ref kryptonComboBoxLes, ref kryptonComboBoxType);
        }

        private void kryptonComboBoxDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setGroup(ref kryptonComboBoxGro, ref kryptonComboBoxDiv);
        }

    private void kryptonComboBoxGro_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setLecture(ref kryptonComboBoxLes,ref kryptonComboBoxType);
        }

        private void kryptonComboBoxLes_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.DataSource= controller.report(kryptonComboBoxGro.SelectedValue.ToString(),kryptonComboBoxLes.SelectedValue.ToString());
        }
    }
}
