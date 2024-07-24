using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        }
       
     

        
        

      

      

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("هل تريد حذف السجل السابق ؟", "مسح السجل السابق", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);

            if (re != DialogResult.Cancel)
            {
                DataTable dt = new DataTable();
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                            {
                                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                                {
                                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                    {
                                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                        {
                                            UseHeaderRow = true
                                        }
                                    });
                                    dt = result.Tables[0];
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("error in import data from excel !");
                            return;
                        }
                    }

                    if (!dt.Columns.Contains("الاسم")) { MessageBox.Show("! حقل الاسم غير موجود يرجى اضافته الى الاكسل", "حقل اسم الطالب غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (!dt.Columns.Contains("الرقم")) { MessageBox.Show("! حقل الرقم غير موجود يرجى اضافته الى الاكسل", "حقل رقم البطاقة غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (!dt.Columns.Contains("الملاحظة")) { MessageBox.Show("! حقل الملاحظة غير موجود يرجى اضافته الى الاكسل", "حقل الملاحظة غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                }
                catch { MessageBox.Show("يرجى اغلاق الملف الاكسل"); }
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            (kryptonDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] like '" + kryptonTextBox1.Text + "%'");
        }

        private void kryptonComboBoxStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setType(ref kryptonComboBoxType, ref kryptonComboBoxStage);
        }

        private void kryptonComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setDivision(ref kryptonComboBoxDiv,ref kryptonComboBoxType);
        }

        private void kryptonComboBoxDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setGroup(ref kryptonComboBoxGro, ref kryptonComboBoxDiv);
        }
        private void kryptonComboBoxGro_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.DataSource =  FunctionsDataBase.view_table("students", $"group_id={kryptonComboBoxGro.SelectedValue}", "name,card_num,note");

        }

        private void control_Load(object sender, EventArgs e)
        {
            controller.setStage(ref kryptonComboBoxStage);
        }
    }
}
