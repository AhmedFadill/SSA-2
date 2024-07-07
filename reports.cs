using ClosedXML.Excel;
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


        string alarms(string cell)
        {
            if (int.TryParse(cell, out _))
            {
                int c1 = int.Parse(cell),
                a0 = 2,
                a1 = 3,
                a2 = 4,
                a3 = 5,
                a4 = 7;
                if (c1 >= a4)
                    return "فصل";
                else if (c1 >= a3)
                    return "انذار نهائي";
                else if (c1 >= a2)
                    return "انذار ثاني";
                else if (c1 >= a1)
                    return "انذار اول";
                else if (c1 >= a0)
                    return "تنبيه";
            }
            return string.Empty;
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
            dt.Columns["result"].ColumnName = "مجموع الغياب";
            dt.Columns["state"].ColumnName = "الحالة";

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

        
    }
}
