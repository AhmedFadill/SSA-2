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
            //id = DB_Functions.Load_data("select id from [info] where  [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' [Gro]='" + kryptonComboBoxGro.Text + "'").Rows[0][0].ToString();
            kryptonDataGridView1.DataSource = DB_Functions.Load_data("select id , [Name] , [cardNum],[Note] from students where [studyInformationID]=(select id from [info] where  [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "') and [isDelete]=0");
        }
    }
}
