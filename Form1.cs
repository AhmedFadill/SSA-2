using AForge.Video;
using AForge.Video.DirectShow;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ZXing;

namespace SSA_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Variables
        static public SqlConnection sqlcon = DB_Functions.Connection();
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice device;
        string test, infoid,id=login.id;
        bool isedit=false;



        //Method
        void Load_data_specific()
        {
            DataTable data;
            data = DB_Functions.Load_data("SELECT [studentID] ,[Name],[cardNum],[notS],[PA],[notA] FROM SA  where [teacherID] = " + id
                + "and [Date]='" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") + "'  " + "and infID = " + infoid);
            if (data.Rows.Count > 0)
            {
                isedit = true;
                data = DB_Functions.Load_data("select [studentID] ,Name,cardNum,[notS],[notA]  from [SA] where Date='" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") +"' and infID="+infoid+ " and [lessonName]='"+kryptonComboBoxLes.Text+"'");
                data.Columns.Add("PA", typeof(bool));
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string p=DB_Functions.Load_data("select PA from [SA] where Date='" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") + "' and infID=" + infoid + " and [lessonName]='" + kryptonComboBoxLes.Text + "' and [studentID]=" + data.Rows[i][0]).Rows[0][0].ToString();
                    data.Rows[i]["PA"] = p == "حاضر";
                }
            }
            else
            {
                isedit = false;
                data = DB_Functions.Load_data("select [id]  as studentID,[Name],[cardNum],[Note] as 'notS' from [students] where  [studyInformationID] = " + infoid);
                if (data != null)
                {
                    for (int i = 1; i < data.Columns.Count; i++)
                        data.Columns[i].ReadOnly = true;
                    data.Columns.Add("PA", typeof(bool));
                    data.Columns.Add("notA", typeof(string));
                }
            }
            table_mainscreen.DataSource = data;
            table_mainscreen.Columns["studentID"].Visible = false;
            table_mainscreen.Columns["Name"].Width = 200;
            table_mainscreen.Columns["Name"].HeaderText = "اسم الطالب";
            table_mainscreen.Columns["cardNum"].HeaderText = "رقم البطاقة";
            table_mainscreen.Columns["notS"].HeaderText = "المعلومات الاضافية";
            if (table_mainscreen.Columns.Contains("PA") == true)
            {
                table_mainscreen.Columns["PA"].HeaderText = "الغياب";
                table_mainscreen.Columns["notA"].HeaderText = "ملاحظة";
                table_mainscreen.Columns["PA"].ReadOnly = false;
            }
            for (int i = 2; i < table_mainscreen.ColumnCount - 1; i++)
            {
                table_mainscreen.Columns[i].Width = 200;
            }
            

        }
        private void start()
        {
            device = new VideoCaptureDevice(filterInfoCollection[cbocamera.SelectedIndex].MonikerString);
            device.NewFrame += VideoCaptureDevice_NewFrame;
            device.Start();
        }
        private string Check(string card_number, bool beep = true)
        {
            for (int i = 0; i < table_mainscreen.Rows.Count; i++)
            {
                if (table_mainscreen.Rows[i].Cells["cardNum"].Value.ToString() == card_number)
                {
                    name_student.Visible = true;
                    table_mainscreen.Rows[i].Cells["PA"].Value = true;
                    if (beep) Console.Beep(500, 750);
                    return table_mainscreen.Rows[i].Cells["Name"].Value.ToString();
                }
            }
            if (beep) Console.Beep(1000, 1000);
            return "البطاقة غير معرفة";
        }
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bmp);
            if (result != null)
            {
                if (test != result.Text)
                {
                    name_student.Invoke(new MethodInvoker(delegate ()
                    {
                        name_student.Text = Check(result.Text);
                        test = result.Text;
                    }));

                }
            }
            camera_barcode.Image = bmp;
        }


        

        void set(bool Stage = false, bool Type = false, bool Division = false, bool Groups = false, bool Subjects = false)
        {

            if (Stage) DB_Functions.SetComboBox(ref kryptonComboBoxStage, DB_Functions.Load_data("select [Sta] from [IS] where [teaID]=" + id + " group by [Sta];"));
            if (Type) DB_Functions.SetComboBox(ref kryptonComboBoxType, DB_Functions.Load_data("select [Typ] from [IS] where [teaID]=" + id + " and [Sta]='" + kryptonComboBoxStage.Text + "' group by [Typ];"));
            if (Division) DB_Functions.SetComboBox(ref kryptonComboBoxDiv, DB_Functions.Load_data("select [Div] from [IS] where [teaID]= " + id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + "  group by [Div];"));
            if (Groups) DB_Functions.SetComboBox(ref kryptonComboBoxGro, DB_Functions.Load_data("select [Gro] from [IS] where [teaID]= " + id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + "  group by [Gro];"));
            if (Subjects) DB_Functions.SetComboBox(ref kryptonComboBoxLes, DB_Functions.Load_data("select distinct [lessonName] from [IS] where [teaID]=" + id + " and [Sta]='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "' and [Gro]='" + kryptonComboBoxGro.Text + "'"));
        }



        string barcode;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            char c = (char)keyData;

            if (char.IsNumber(c))
            {
                barcode += c;
            }
            if (c == (char)Keys.Return && !string.IsNullOrEmpty(barcode))
            {
                name_student.Text = Check(barcode, false);
                barcode = "";
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Event
        private void SideBarControll(int num)
        {
            panelMainScreem.BackColor = num == 1 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShow.BackColor = num == 2 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelListStudent.BackColor = num == 3 ? Color.FromArgb(235, 239, 255) : Color.Transparent;
            panelShowReport.BackColor = num == 4 ? Color.FromArgb(235, 239, 255) : Color.Transparent;

            labelMainScreen.ForeColor = num == 1 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShow.ForeColor = num == 2 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelListStudent.ForeColor = num == 3 ? Color.FromArgb(100, 119, 219) : Color.Black;
            labelShowReport.ForeColor = num == 4 ? Color.FromArgb(100, 119, 219) : Color.Black;

            table_mainscreen.Visible = num==1? true: false;
            filteer_mainscreen.Visible= num==1? true: false;

            show1.Visible = num==2? true: false;

            control1.Visible = num==3? true: false;

            reports1.Visible = num==4? true: false;
        }

        private void panelMainScreem_Click(object sender, EventArgs e)
        {
            SideBarControll(1);
        }

        private void panelShow_Click(object sender, EventArgs e)
        {
            SideBarControll(2);
        }

        private void panelListStudent_Click(object sender, EventArgs e)
        {
            SideBarControll(3);
        }

        private void panelShowReport_Click(object sender, EventArgs e)
        {
            SideBarControll(4);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            camera_off.Visible = false;
            camera_on.Visible = true;
            camera_barcode.Visible = true;
            cbocamera.Visible= true;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            cbocamera.Items.Clear();
            foreach (FilterInfo item in filterInfoCollection)
            {
                cbocamera.Items.Add(item.Name);
            }
            cbocamera.SelectedIndex = 0;
            start();
        }

        private void camera_on_Click(object sender, EventArgs e)
        {
            camera_off.Visible = true;
            camera_on.Visible = false;
            camera_barcode.Visible = false;
            cbocamera.Visible = false;
            if (device != null)
                if (device.IsRunning)
                    device.Stop();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            set(Stage:true);
            kryptonDateTimePicker1.Value = DateTime.Now;
            label2.Text=DB_Functions.Load_data("select Name from teachers where id =" + id).Rows[0][0].ToString();
        }

        private void kryptonComboBoxStage_TextChanged(object sender, EventArgs e)
        {
            set(Type:true);
        }

        private void kryptonComboBoxType_TextChanged(object sender, EventArgs e)
        {
            set(Division:true);
        }

        private void kryptonComboBoxDiv_TextChanged(object sender, EventArgs e)
        {
            set(Groups:true);
        }

        private void kryptonComboBoxGro_TextChanged(object sender, EventArgs e)
        {
            DataTable data = DB_Functions.Load_data("SELECT id FROM [info] where Sta='" + kryptonComboBoxStage.Text + "'" + " and [Typ]='" + kryptonComboBoxType.Text + "'" + " and [Div]='" + kryptonComboBoxDiv.Text + "'" + " and [Gro]='" + kryptonComboBoxGro.Text + "'");
            if (data.Rows.Count > 0) infoid = data.Rows[0][0].ToString();
            set(Subjects:true);
            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string d = DB_Functions.Load_data("if not exists (select * from dates where [Date]='" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") + "')" +
                                                "begin INSERT INTO [dbo].[dates]([Date])output INSERTED.id VALUES('" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") + "'); end " +
                                                "else begin select [id] from dates where [Date]='" + kryptonDateTimePicker1.Value.ToString("yy-MM-dd dddd") + "'; end").Rows[0][0].ToString();
            if (isedit)
            {
                foreach (DataGridViewRow data in table_mainscreen.Rows)
                {
                    string ab = data.Cells["PA"].Value.ToString() == "True" ? "1" : "0"
                        , q = "UPDATE [absences] SET [PA] = " + ab + ",[Note] =' " + data.Cells["notA"].Value.ToString() + "' WHERE [dateID]='" + d+ "' and lessonID = (select [colID] from [IS] where [lessonName]='" + kryptonComboBoxLes.Text + "' and [SI_ID]='" + infoid + "' and [teaID]=" + login.id + ") and [studentID]= " + data.Cells[0].Value;
                    DB_Functions.Execute(q);
                }
            }
            else
            {
                foreach (DataGridViewRow data in table_mainscreen.Rows)
                {
                    string ab = data.Cells["PA"].Value.ToString() == "True" ? "1" : "0"
                        , q = "INSERT INTO [dbo].[absences]([studentID],[dateID],[lessonID],[PA],Note)" +
                        "VALUES(" + data.Cells["studentID"].Value + "," + d +
                        ",(select [colID] from [IS] where [lessonName]='" + kryptonComboBoxLes.Text + "' and [SI_ID]='" + infoid + "' and [teaID]=" + login.id + ")," + ab + ",'" + data.Cells["notA"].Value + "')";
                    DB_Functions.Execute(q);
                }
            }
            MessageBox.Show("تم اضافة الحضورة", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_data_specific();
        }

        private void kryptonDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Load_data_specific();
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            (table_mainscreen.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Name] like '" + kryptonTextBox1.Text + "%'");

        }

        private void kryptonComboBoxLes_TextChanged(object sender, EventArgs e)
        {
            Load_data_specific();

        }
    }
}
