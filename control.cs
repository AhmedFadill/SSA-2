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
        void set(bool Stage = false, bool Type = false, bool Division = false, bool Groups = false)
        {
            if (Stage) DB_Functions.SetComboBox(ref kryptonComboBoxStage, DB_Functions.Load_data("select distinct [Sta] from [info]"));
            if (Type) DB_Functions.SetComboBox(ref kryptonComboBoxType, DB_Functions.Load_data("select distinct [Typ] from [info] where [Sta]='" + kryptonComboBoxStage.Text + "'"));
            if (Division) DB_Functions.SetComboBox(ref kryptonComboBoxDiv, DB_Functions.Load_data("select distinct [Div] from [info] where  [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" ));
            if (Groups) DB_Functions.SetComboBox(ref kryptonComboBoxGro, DB_Functions.Load_data("select distinct[Gro] from [info] where  [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" ));   
        }
        string id;
        private void control_Load(object sender, EventArgs e)
        {
            set(Stage: true);
        }

        private void kryptonComboBoxStage_TextChanged(object sender, EventArgs e)
        {
            set(Type: true);
        }

        private void kryptonComboBoxType_TextChanged(object sender, EventArgs e)
        {
            set(Division: true);
        }

        private void kryptonComboBoxDiv_TextChanged(object sender, EventArgs e)
        {
            set(Groups: true);
        }

        private void kryptonComboBoxGro_TextChanged(object sender, EventArgs e)
        {
            id = DB_Functions.Load_data("select id from [info] where  [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "'").Rows[0][0].ToString();
            kryptonDataGridView1.DataSource = DB_Functions.Load_data("select id , [Name] , [cardNum],[Note] from students where [studyInformationID]="+id+" and [isDelete]=0");
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Form2 f=new Form2(null, id) { Text="اضافة طالب" };
            f.ShowDialog();
            kryptonComboBoxGro_TextChanged(sender, e);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if(kryptonDataGridView1.CurrentRow != null)
            {
                Form2 f = new Form2(kryptonDataGridView1.CurrentRow.Cells["id"].Value.ToString(), null) { Text = "تعديل طالب" };
                f.ShowDialog();
                kryptonComboBoxGro_TextChanged(sender, e);
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("هل انت متاكد من مسح الطالب : " + kryptonDataGridView1.CurrentRow.Cells["Name"].Value.ToString() + " ؟", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                DB_Functions.Execute("UPDATE [dbo].[students] SET [isDelete] = 1 where id = " + kryptonDataGridView1.CurrentRow.Cells["id"].Value.ToString());
            }
            kryptonComboBoxGro_TextChanged(sender, e);

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
                        if (re == DialogResult.Yes)
                            DB_Functions.Execute("UPDATE [dbo].[students] SET [isDelete] = 1 where [studyInformationID] =" + id);
                        foreach (DataRow row in dt.Rows)
                        {
                            if (string.IsNullOrWhiteSpace(row[NameIndex].ToString())) continue;
                            if (string.IsNullOrWhiteSpace(row[CardIndex].ToString())) continue;
                            DB_Functions.Execute("insert into students (Name,cardNum,Note,[studyInformationID]) values ('" + row[NameIndex].ToString() + "'," + row[CardIndex].ToString() + ",'" + row[Note].ToString() + "'," + id + ") ");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("يوجد خطا في الملف يرجى التاكد من ان الملف يتبع القواعد الخاصة بادخال بيانات ", "خطا في الملف المدخل", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch { MessageBox.Show("يرجى اغلاق الملف الاكسل"); }

                kryptonComboBoxGro_TextChanged(sender, e);
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            (kryptonDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] like '" + kryptonTextBox1.Text + "%'");
        }
    }
}
