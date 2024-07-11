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
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice device;


        //Method

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
            string test = string.Empty;
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
        private void start()
        {
            device = new VideoCaptureDevice(filterInfoCollection[cbocamera.SelectedIndex].MonikerString);
            device.NewFrame += VideoCaptureDevice_NewFrame;
            device.Start();
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

      
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
            kryptonDateTimePicker1.Value = DateTime.Now;
            controller.setStage(ref kryptonComboBoxStage);

        }

        private void kryptonComboBoxStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setType(ref kryptonComboBoxType, kryptonComboBoxStage.SelectedValue.ToString());
        }

        private void kryptonComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setDivision(ref kryptonComboBoxDiv, kryptonComboBoxType.SelectedValue.ToString());
            controller.setLecture(ref kryptonComboBoxLec, kryptonComboBoxType.SelectedValue.ToString());
        }

        private void kryptonComboBoxDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.setGroup(ref kryptonComboBoxGro, kryptonComboBoxDiv.SelectedValue.ToString());
        }

        private void kryptonComboBoxGro_SelectedIndexChanged(object sender, EventArgs e)
        {
            table_mainscreen.DataSource= FunctionsDataBase.view_table("students", $"group_id={kryptonComboBoxGro.SelectedValue}","name,card_num,note");
        }
    }
}
