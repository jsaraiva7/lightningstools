using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.MacroProgramming;
using Common.SimLinkup.Configuration;
using Common.Statistics;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using Accord.IO;
using Accord.Video.FFMPEG;
using LiveCharts;
using LiveCharts.Configurations;
 
using Rectangle = System.Drawing.Rectangle;
using Timer = System.Windows.Forms.Timer;
using UserControl = System.Windows.Controls.UserControl;

namespace SimLinkupControls
{
    /// <summary>
    /// Interação lógica para SignalView.xam
    /// </summary>
    public partial class SignalView : UserControl
    {
  

        public SeriesCollection SeriesCollection { get; set; }

        public Signal SelectedSignal;
        private PictureBox pbVisualization;
        private bool _isRecording = false;
        private VideoFileWriter _writer;
        private Bitmap _bmp;
        private Window w;
        private int _with = 480;
        private int _height = 370;
        private DateTime _startTime;
        private int _interval = 5000;
        private bool sliderSet = false;
        public bool UpdateInRealtime { get; set; } = true;
        public SignalView( )
        {  
            InitializeComponent();
            Stop.Visibility = Visibility.Hidden;
            Record.Visibility = Visibility.Hidden;
          
            Popup.Visibility = Visibility.Hidden;
            pbVisualization = DrawForm.Child as PictureBox;
            pbVisualization.Width = _with;
            pbVisualization.Height = _height;
            var timer = new Timer { Interval = 30 };
            timer.Tick += (s, e) =>
            {
                if (UpdateInRealtime)
                {
                    pbVisualization.Invalidate();
                    //ptureSnapShot();
                }
            };
            timer.Enabled = true;
            pbVisualization.Paint += SignalsView_Paint;
           


        }

        private void SetupTrackbar()
        {
            if (SelectedSignal is DigitalSignal)
            {
             
                slSignal.Minimum = 0;
                slSignal.Maximum = 1;
                slSignal.Interval = 1;
            }
            else if (SelectedSignal is AnalogSignal)
            {
                var typedSignal = SelectedSignal as AnalogSignal;
                if (typedSignal.MaxValue > 0.001)
                {
                    slSignal.Minimum = typedSignal.MinValue;
                    slSignal.Maximum = typedSignal.MaxValue;
                    slSignal.Interval = 1;
                }
                else
                {
                    slSignal.Minimum = typedSignal.MinValue;
                    slSignal.Maximum = 10;
                    slSignal.Interval = 1;
                }
            }
            else if (SelectedSignal is CalibratedAnalogSignal)
            {
                var typedSignal = SelectedSignal as CalibratedAnalogSignal;
                if (typedSignal != null && typedSignal.MaxValue > 0.001)
                {
                    slSignal.Minimum = typedSignal.MinValue;
                    slSignal.Maximum = typedSignal.MaxValue;
                    slSignal.Interval = 1;
                }
                else
                {
                    if (typedSignal != null) slSignal.Minimum = typedSignal.MinValue;
                    slSignal.Maximum = 10;
                    slSignal.Interval = 1;
                }
            }
        }



        private void SignalsView_Paint(object sender, PaintEventArgs e)
        {
            UpdateVisualization();
        }


        public void MonitorSignal(Signal signal, int timeMs)
        {
            SelectedSignal = signal;
            Record.Visibility = Visibility.Visible;
            UpdateVisualization();
            CbInterval.Visibility = Visibility.Hidden;
            LblInterval.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Hidden;
            Record.Visibility = Visibility.Visible;
            Popup.Visibility = Visibility.Visible;
            StopRec();

        }
        private void UpdateVisualization()
        {
            if (pbVisualization.Image == null)
            {
                if (pbVisualization != null)
                {
                    _bmp = new Bitmap(_with, _height);
                    pbVisualization.Image = _bmp;
                }
         
            }

            using (var graphics = Graphics.FromImage(pbVisualization.Image))
            {
                var targetRectangle = new Rectangle(0, 0, _with, _height);
                if (SelectedSignal != null)
                {
                    UpdateSignalGraph(graphics, targetRectangle);
                    CaptureSnapShot(_bmp);
                }
              
            }
        }
        private void UpdateSignalGraph(Graphics graphics, Rectangle targetRectangle)
        {
            SelectedSignal.DrawGraph(graphics, targetRectangle, _interval);
        }

     
        private void CaptureSnapShot(Bitmap bitmap)
        {
            try
            {
                if (DateTime.Now - _startTime > new TimeSpan(0, 10, 0))
                {
                    StopRec();
                }
                if (_isRecording)
                {
                    if (_writer == null)
                    {
                        _writer = new VideoFileWriter();
                        _writer.Open(GetRecoderFileName(), _with, _height,24, VideoCodec.H264);
                    }
                    else
                    {
                        _writer.WriteVideoFrame(bitmap);
                    }
                    
                }
            }
            catch (Exception e)
            {
               // throw e;
            }
          

        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
             
            Stop.Visibility = Visibility.Visible;
            Record.Visibility = Visibility.Hidden;
            _isRecording = true;
            _startTime = DateTime.Now;
        }
        private void RecordStop1_Click(object sender, RoutedEventArgs e)
        {

            Stop.Visibility = Visibility.Hidden;
            Record.Visibility = Visibility.Visible;
            _isRecording = false;
            StopRec();
            


        }

        private void StopRec()
        {
            if (_isRecording)
            {
                _isRecording = false;
                _writer.Close();
                _writer.Dispose();
            }
           
            _writer = null;
        }

        private string GetRecoderFileName()
        {
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule
                           .FileName) + "\\Recordings";
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var cfgFileName = SelectedSignal.FriendlyName + "_" + DateTime.Now.ToString("yyyyMMddHHms") + ".avi";
        
            path += "\\" + cfgFileName;
            return path;
        }

        private void Popup_OnClick(object sender, RoutedEventArgs e)
        {
            Window pw = new Window();
            pw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            pw.Width = 500;
            pw.Height = 650;
            SignalView s = new SignalView();
            s.CbInterval.Visibility = Visibility.Visible;
            s.LblInterval.Visibility = Visibility.Visible;
            pw.Closing += window_Closing;
            s.SelectedSignal = this.SelectedSignal;
            s.w = pw;
            s.w.Closing += s.window_Closing;
            s.Popup.Visibility = Visibility.Hidden;
            s.Record.Visibility = Visibility.Visible;
            pw.Content = s;
            pw.Show();
            this.SelectedSignal = null;

        }

      

        void window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            if (this._isRecording && _writer != null)
            {
                this.StopRec();
            }
        }
 

        private void CbInterval_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem ComboItem = (ComboBoxItem)CbInterval.SelectedItem;
            string content = ComboItem.Name;
           
            if (content != null)
            {
                if (content != null && content.Contains("ec5"))
                {
                    _interval = 5000;
                }
                else if (content.Contains("ec10"))
                {
                    _interval = 10000;
                }
                else if (content.Contains("ec15"))
                {
                    _interval = 15000;
                }
                else if (content.Contains("ec20"))
                {
                    _interval = 20000;
                }
                else if (content.Contains("ec30"))
                {
                    _interval = 30000;
                }
                else if (content.Contains("min1"))
                {
                    _interval = 60000;
                }
                else if (content.Contains("min10"))
                {
                    _interval = (int)new TimeSpan(0,10,0).TotalMilliseconds;
                }
                else if (content.Contains("min30"))
                {
                    _interval = (int)new TimeSpan(0, 30, 0).TotalMilliseconds;
                }
                else if (content.Contains("hour1"))
                {
                    _interval = (int)new TimeSpan(1, 0, 0).TotalMilliseconds;
                }
            }
        }

        private void SlSignal_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!sliderSet)
            {
                SetupTrackbar();
                sliderSet = true;
            }
            if (SelectedSignal is DigitalSignal)
            {
                var typedSignal = SelectedSignal as DigitalSignal;
                if (slSignal.Value > 0)
                {
                    typedSignal.State = true;
                }
                else
                {
                    typedSignal.State = false;
                }

            }
            else if (SelectedSignal is AnalogSignal)
            {
                var typedSignal = SelectedSignal as AnalogSignal;
                if (typedSignal.MaxValue > 0.001)
                {
                    typedSignal.State = slSignal.Value;
                }
                else
                {
                    var diff = slSignal.Maximum - slSignal.Value;
                    if (diff < 3)
                    {
                        slSignal.Maximum = slSignal.Maximum + 3;
                    }
                    typedSignal.State = slSignal.Value;
                }
            }
        }
    }


}
