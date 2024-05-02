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

        string id;

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
        void set(bool Stage = false, bool Type = false, bool Division = false, bool Groups = false, bool Subjects = false)
        {

            if (Stage) DB_Functions.SetComboBox(ref kryptonComboBoxStage, DB_Functions.Load_data("select distinct [Sta] from [SA] where [teacherID]=" + login.id));
            if (Type) DB_Functions.SetComboBox(ref kryptonComboBoxType, DB_Functions.Load_data("select distinct [Typ] from [SA] where [teacherID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'"));
            if (Division) DB_Functions.SetComboBox(ref kryptonComboBoxDiv, DB_Functions.Load_data("select [Div] from [SA] where [teacherID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + "  group by [Div];"));
            if (Groups) DB_Functions.SetComboBox(ref kryptonComboBoxGro, DB_Functions.Load_data("select [Gro] from [SA] where [teacherID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + "  group by [Gro];"));
            if (Subjects) DB_Functions.SetComboBox(ref kryptonComboBoxLes, DB_Functions.Load_data("select distinct [lessonName] from [SA] where [teacherID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "'"));
        }

        private void reports_Load(object sender, EventArgs e)
        {
            set(Stage:true);
        }

        private void kryptonComboBoxStage_TextChanged(object sender, EventArgs e)
        {
            set(Type: true);
        }

        private void kryptonComboBoxType_TextChanged(object sender, EventArgs e)
        {
            set(Division:true);
        }

        private void kryptonComboBoxDiv_TextChanged(object sender, EventArgs e)
        {
            set(Groups: true);
        }

        private void kryptonComboBoxGro_TextChanged(object sender, EventArgs e)
        {
            set(Subjects:true);
            
        }

        private void kryptonComboBoxLes_TextChanged(object sender, EventArgs e)
        {
            kryptonDataGridView1.Columns.Clear();
            id = DB_Functions.Load_data("select [infID] from [SA] where [teacherID]=" + login.id +
                " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" +
                " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "' and [lessonName] ='" + kryptonComboBoxLes.Text + "'").Rows[0][0].ToString();
            DataTable dt=DB_Functions.Load_data("select DISTINCT [studentID] as id,Name from SA where [infID]=" + id);
            dt.Columns.Add("result");
            dt.Columns.Add("state");
            foreach (DataRow dr in DB_Functions.Load_data("select distinct [Date] from SA where lessonName='" + kryptonComboBoxLes.Text + "' and [infID] = " + id).Rows)
                dt.Columns.Add(dr[0].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 4; j < dt.Columns.Count; j++)
                    dt.Rows[i][j] = DB_Functions.Load_data("select PA from SA where Date = '" + dt.Columns[j].ColumnName + "' and [studentID] =" + dt.Rows[i]["id"]).Rows[0][0].ToString();
                dt.Rows[i]["result"] = DB_Functions.Load_data("SELECT count(*) FROM [SSA].[dbo].[SA] where PA='غائب'  and infID = "+id+" and lessonName='" + kryptonComboBoxLes.Text + "' and studentID=" + dt.Rows[i]["id"]).Rows[0][0].ToString();
                dt.Rows[i]["state"] = alarms(dt.Rows[i]["result"].ToString());
            }
            kryptonDataGridView1.DataSource=dt;
            kryptonDataGridView1.Columns["id"].Visible = false;
            for (int i = 0; i < kryptonDataGridView1.ColumnCount; i++)
                kryptonDataGridView1.Columns[i].ReadOnly = true;
            kryptonDataGridView1.Columns["Name"].HeaderText = "اسم الطالب";
            kryptonDataGridView1.Columns["result"].HeaderText = "مجموع الغياب";
            kryptonDataGridView1.Columns["state"].HeaderText = "الحالة";
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            reports_Load(null,null);
        }
    }
}
