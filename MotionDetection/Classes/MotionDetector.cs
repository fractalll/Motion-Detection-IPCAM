using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.VideoSurveillance;
using System;
using System.Drawing;
using System.Linq;

namespace IpCamMotionDetection
{
    class MotionDetector
    {
        TimeSpan _interval;
       
        public double Duration { get; set; }
        public double MaxDelta { get; set; }
        public double MinDelta { get; set; }

        /// <summary>
        /// Интервал сбора данных в секундах
        /// </summary>
        public int CycleInterval
        {
            get
            {
                return (int)_interval.TotalSeconds;
            }
            set
            {
                _interval = new TimeSpan(0, 0, value);
            }
        }       

        public event EventHandler<DetectingEventArgs> DataRecived;
        string _connectionString;
        public MotionDetector(string connectionString, int cycleInterval = 10)
        {
            _connectionString = connectionString;
            _interval = new TimeSpan(0, 0, cycleInterval);
            Duration = 1;
            MaxDelta = 0.05;
            MinDelta = 0.5;
        }

        Capture _capture;
        MotionHistory _motionHistory;           
        public void Start()
        {
            Console.WriteLine("in thread, Start()");
            try
            {
                _motionHistory = new MotionHistory(
                 Duration,  //in second, the duration of motion history you wants to keep
                 MaxDelta,  //in second, maxDelta for cvCalcMotionGradient
                 MinDelta); //in second, minDelta for cvCalcMotionGradient

                _capture = new Capture(_connectionString);
                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
                Console.WriteLine("Start camera " + _connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("in thread, Start() out");
        }


        Mat image;
        private Mat _segMask = new Mat();
        private Mat _forgroundMask = new Mat();
        BackgroundSubtractor _forgroundDetector; 
        private void ProcessFrame(object sender, EventArgs e)
        {
            Console.Write("#");
            image = new Mat();
            _capture.Retrieve(image);
                       
            if (_forgroundDetector == null)
            {
                _forgroundDetector = new BackgroundSubtractorMOG2();
            }

            _forgroundDetector.Apply(image, _forgroundMask);

            //update the motion history
            _motionHistory.Update(_forgroundMask);

            #region get a copy of the motion mask and enhance its color
            double[] minValues, maxValues;
            Point[] minLoc, maxLoc;
            _motionHistory.Mask.MinMax(out minValues, out maxValues, out minLoc, out maxLoc);
            Mat motionMask = new Mat();
            using (ScalarArray sa = new ScalarArray(255.0 / maxValues[0]))
                CvInvoke.Multiply(_motionHistory.Mask, sa, motionMask, 1, DepthType.Cv8U);
            //Image<Gray, Byte> motionMask = _motionHistory.Mask.Mul(255.0 / maxValues[0]);
            #endregion

            //Threshold to define a motion area, reduce the value to detect smaller motion
            double minArea = 100;

            //storage.Clear(); //clear the storage
            Rectangle[] rects;
            using (VectorOfRect boundingRect = new VectorOfRect())
            {
                _motionHistory.GetMotionComponents(_segMask, boundingRect);
                rects = boundingRect.ToArray();
            }

            //iterate through each of the motion component
            foreach (Rectangle comp in rects)
            {
                int area = comp.Width * comp.Height;
                //reject the components that have small area;
                if (area < minArea) continue;

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCount;
                _motionHistory.MotionInfo(_forgroundMask, comp, out angle, out motionPixelCount);

                //reject the area that contains too few motion
                if (motionPixelCount < area * 0.05) continue;

                //Draw each individual motion in red
                //DrawMotion(motionImage, comp, angle, new Bgr(Color.Red));
            }

            // find and draw the overall motion angle
            double overallAngle, overallMotionPixelCount;

            _motionHistory.MotionInfo(_forgroundMask, new Rectangle(Point.Empty, motionMask.Size), out overallAngle, out overallMotionPixelCount);

            //Display the amount of motions found on the current image
            //Total Motions found: {0}; Motion Pixel count: {1}
            AddData(rects.Length, overallMotionPixelCount);
        }
        
        
        int total_count = 0;
        double average_motions = 0;
        double average_pixels = 0;
        DateTime _cycleStart;
        private void AddData(double motions, double pixels)
        {
            GC.Collect();              

            if (total_count == 0)           
                _cycleStart = DateTime.Now;     

            average_motions = (average_motions * total_count + motions) / (total_count + 1);
            average_pixels = (average_pixels * total_count + pixels) / (total_count + 1);
            total_count++;

            if (DateTime.Now >= _cycleStart + _interval)
            {        
                SendData();      
                total_count = 0;
                average_motions = 0;
                average_pixels = 0;               
            }
        }

        private void SendData()
        {
            DetectingEventArgs data = new DetectingEventArgs
            {
                AverageMotions = (int)average_motions,
                AveragePixels = average_pixels,
                TimeStart = _cycleStart,
                TimeFinish = DateTime.Now,
                TotalCount = total_count,
                DataSource = _connectionString
            };

            DataRecived?.Invoke(this, data); //если есть подписчик, то передаем ему данные
        }

        public void Dispose()
        {
            _capture.Dispose();
        }   
    }
}
