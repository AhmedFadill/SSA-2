using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSA_2
{
    internal static class controller
    {
        internal static void setStage(ref KryptonComboBox combobox)
        {
            combobox.DisplayMember = "Key";
            combobox.ValueMember = "Value";
            combobox.DataSource = FunctionsDataBase.view("CD", "true", "stage,stage_id");
        }
        internal static void setType(ref KryptonComboBox combobox,string id)
        {
            combobox.DisplayMember = "Key";
            combobox.ValueMember = "Value";
            combobox.DataSource = FunctionsDataBase.view("CD", $"stage_id={id}", "type,stage_id");
        }
        internal static void setLecture(ref KryptonComboBox combobox, string id)
        {
            combobox.DisplayMember = "Key";
            combobox.ValueMember = "Value";
            combobox.DataSource = FunctionsDataBase.view("CD", $"stage_id={id}", "lecture,lec_id");
        }
        internal static void setDivision(ref KryptonComboBox combobox, string id)
        {
            combobox.DisplayMember = "Key";
            combobox.ValueMember = "Value";
            combobox.DataSource = FunctionsDataBase.view("CD", $"stage_id={id}", "division,div_id");
        }
        internal static void setGroup(ref KryptonComboBox combobox, string id)
        {
            combobox.DisplayMember = "Key";
            combobox.ValueMember = "Value";
            combobox.DataSource = FunctionsDataBase.view("CD", $"div_id={id}", "gro,group_id");
        }
    }
}
