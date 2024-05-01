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
            
        }
        string id;
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
            DataTable dt=DB_Functions.Load_data("select [studentID] as id,Name from SA where [infID]=" + id);
            dt.Columns.Add("result");
            dt.Columns.Add("state");
            foreach (DataRow dr in DB_Functions.Load_data("select distinct [Date] from SA where [infID] = " + id).Rows)
                dt.Columns.Add(dr[0].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 4; j < dt.Columns.Count; j++)
                    dt.Rows[i][j] = DB_Functions.Load_data("select PA from SA where Date = '" + dt.Columns[j].ColumnName + "' and [studentID] =" + dt.Rows[i]["id"]).Rows[0][0].ToString();
            }
                kryptonDataGridView1.DataSource=dt;
        }
    }
}
