using IpCamLibrary.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpCamMotionDetection
{
    public partial class Form1 : Form
    {
        DetectionController dc = new DetectionController()
        {
            CycleInterval = 60
        };

        public Form1()
        {
            InitializeComponent();            
            AddCameraControls(10,40);

            new Task(DetectionController.GCLoop).Start();
            dc.DataRecived += OnDataRecived;
            dc.FrameProcessed += OnFrameProcessed;

        }

       

        private void AddCameraControls(int X, int Y)
        {
            Camera[] cams = GetCams();

            foreach (Camera cam in cams)
            {
                GroupBox gb = new GroupBox()
                {
                    Location = new Point(X, Y),
                    Height = 40,
                    Width = 500
                };
                int y_label_offset = 15;

                Label l_title = new Label()
                {
                    Name = "l_litle",
                    Text = cam.Title,
                    Location = new Point(5, y_label_offset),
                    Width = 50
                };

                Button b_action = new Button()
                {
                    Name = "b_action",
                    Text = "On",
                    Location = new Point(105, 10)                    
                };
                b_action.Click += B1_Click;

                Label l_out = new Label()
                {
                    Name = "l_out",
                    Text = "#",
                    Location = new Point(200, y_label_offset),
                    Width = 50
                };

                Label l_source = new Label()
                {
                    Name = "l_source",
                    Text = "",
                    Location = new Point(250, y_label_offset),
                    Width = 240
                };

                Control id = new Control()
                {
                    Name = "Id",
                    Text = cam.Id.ToString(),
                    Visible = false,                    
                };
              
                gb.Controls.AddRange(new Control[] { id, l_title, b_action, l_out, l_source });
                this.Controls.Add(gb);
                Y += 35;              
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            Button owner = (Button)sender;

            if (owner.Text == "Off")
            {
                owner.Text = "On";
                StopRecognized(owner.Parent.Controls["l_source"].Text);               
                owner.Parent.Controls["l_source"].Text = "";
                owner.Parent.Controls["l_out"].Text = "#";
            }
            else
            {                
                owner.Text = "Off";
                string source = "";
                try
                {
                    source = GetCams().Where(x => x.Id.ToString() == owner.Parent.Controls["Id"].Text).Single().Source;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                owner.Parent.Controls["l_source"].Text = source;
                StartRecognized(source);
            }           
           
        }

        private void StartRecognized(string source)
        {
            //Task t1 = new Task();
            dc.StartCams(source);
        }

        private void StopRecognized(string source)
        {
            dc.StopCams(source);
        }

        private Camera[] GetCams()
        {
            Camera[] cam = new Camera[0];
            try
            {
                using (JournalDbCobtext db = new JournalDbCobtext())
                {
                    cam = db.Cameras.ToArray();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("При обращении к базе данных произошла ошибка: ", e.Message);
            }
            return cam;
        }

        private static void OnDataRecived(object sender, DetectingEventArgs e)
        {
            e.PrintConsole();
            lock (locker)
            {
                SaveToDatabase(e);
            }
        }

       

        MethodInvoker _action;

        private void OnFrameProcessed(object sender, DetectingEventArgs e)
        {
            try
            {
                Label source_label = (Label)ActiveForm.Controls.Find("l_source", true).Where(x => x.Text == e.DataSource).Single();
                Control contr = source_label.Parent.Controls["l_out"];
                _action = new MethodInvoker(() => contr.Text = e.TotalCount.ToString());


                if (contr.InvokeRequired)
                {
                    contr.Invoke(_action);
                }
                else
                    _action();
            }
            catch { }
                     
        }

        static object locker = new object();
        public static void SaveToDatabase(DetectingEventArgs e)
        {
            using (JournalDbCobtext db = new JournalDbCobtext())
            {
                db.Logs.Add(new Log()
                {
                    AverageMotions = e.AverageMotions,
                    TimeStart = e.TimeStart,
                    TimeFinish = e.TimeFinish,
                    TotalCount = e.TotalCount,
                    Camera = db.Cameras.Where(x => x.Source == e.DataSource).SingleOrDefault()
                });

                db.SaveChanges();                
            }
        }

        private void btnTurnAll_Click(object sender, EventArgs e)
        {
            Control[] ctrls = Controls.Find("b_action", true);
            foreach (var item in ctrls)
            {
                B1_Click(item, new EventArgs());
            }
        }
    }
}
