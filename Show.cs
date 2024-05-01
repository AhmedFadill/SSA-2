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
            login.id = "1";
        }
        void set(bool Stage = false, bool Type = false, bool Division = false, bool Groups = false, bool Subjects = false,bool Date=false)
        {

            if (Stage) DB_Functions.SetComboBox(ref kryptonComboBoxStage, DB_Functions.Load_data("select distinct [Sta] from [SA] where [teacherID]=" + login.id ));
            if (Type) DB_Functions.SetComboBox(ref kryptonComboBoxType, DB_Functions.Load_data("select distinct [Typ] from [SA] where [teacherID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text +"'" ));
            if (Division) DB_Functions.SetComboBox(ref kryptonComboBoxDiv, DB_Functions.Load_data("select [Div] from [SA] where [teacherID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + "  group by [Div];"));
            if (Groups) DB_Functions.SetComboBox(ref kryptonComboBoxGro, DB_Functions.Load_data("select [Gro] from [SA] where [teacherID]= " + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + "  group by [Gro];"));
            if (Subjects) DB_Functions.SetComboBox(ref kryptonComboBoxLes, DB_Functions.Load_data("select distinct [lessonName] from [SA] where [teacherID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "'"));
            if(Date) DB_Functions.SetComboBox(ref kryptonComboBoxDate, DB_Functions.Load_data("select distinct [Date] from [SA] where [teacherID]=" + login.id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "' and [lessonName] = '"+ kryptonComboBoxLes.Text+ "'"));
        }
        private void Show_Load(object sender, EventArgs e)
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
            set(Subjects: true);
        }

        private void kryptonComboBoxLes_TextChanged(object sender, EventArgs e)
        {
            set(Date: true);
        }
        private void kryptonComboBoxDate_TextChanged(object sender, EventArgs e)
        {
            table_show.DataSource = DB_Functions.Load_data("SELECT [Name] as 'اسم الطالب',[cardNum] as 'رقم البطاقة',[PA]  as 'الغياب',[notS] as 'ملاحظة على الطالب',[notA] as 'ملاحظة على الغياب' FROM [SSA].[dbo].[SA] where  teacherID="+login.id+" and [Sta]='" + kryptonComboBoxStage.Text+"' and [Typ] = '"+kryptonComboBoxType.Text+"' and [Div] = '"+kryptonComboBoxDiv.Text+"' and [Gro] = '"+kryptonComboBoxGro.Text+"' and [lessonName]='"+ kryptonComboBoxLes.Text + "' and [Date]='" + kryptonComboBoxDate.Text +"'");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            set(Stage: true);
        }
    }
}
