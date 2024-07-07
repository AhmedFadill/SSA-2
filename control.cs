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
                    try
                    {
                        int NameIndex = dt.Columns.IndexOf("اسم الطالب"),
                            CardIndex = dt.Columns.IndexOf("رقم البطاقة"),
                            Note = dt.Columns.IndexOf("الملاحظة");
                        if (NameIndex == -1) { MessageBox.Show("! حقل اسم الطالب غير موجود يرجى اضافته الى الاكسل", "حقل اسم الطالب غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        if (CardIndex == -1) { MessageBox.Show("! حقل رقم البطاقة غير موجود يرجى اضافته الى الاكسل", "حقل رقم البطاقة غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        if (Note == -1) { MessageBox.Show("! حقل الملاحظة غير موجود يرجى اضافته الى الاكسل", "حقل الملاحظة غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        //if (re == DialogResult.Yes)
                        //    DB_Functions.Execute("UPDATE [dbo].[students] SET [isDelete] = 1 where [studyInformationID] =" + id);
                        //foreach (DataRow row in dt.Rows)
                        //{
                        //    if (string.IsNullOrWhiteSpace(row[NameIndex].ToString())) continue;
                        //    if (string.IsNullOrWhiteSpace(row[CardIndex].ToString())) continue;
                        //    DB_Functions.Execute("insert into students (Name,cardNum,Note,[studyInformationID]) values ('" + row[NameIndex].ToString() + "'," + row[CardIndex].ToString() + ",'" + row[Note].ToString() + "'," + id + ") ");
                        //}
                    }
                    catch
                    {
                        MessageBox.Show("يوجد خطا في الملف يرجى التاكد من ان الملف يتبع القواعد الخاصة بادخال بيانات ", "خطا في الملف المدخل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch { MessageBox.Show("يرجى اغلاق الملف الاكسل"); }

                //kryptonComboBoxGro_TextChanged(sender, e);
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            (kryptonDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] like '" + kryptonTextBox1.Text + "%'");
        }
    }
}
