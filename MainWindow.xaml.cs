﻿using System;
//using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Controls;
//using System.Windows.Input;
using System.Text;
//using System.Xml.Linq;
using System.IO;

//using System.Object;
//using Extreme.Mathematics;
//using Extreme.Mathematics.LinearAlgebra.IO;
//using Extreme.Statistics;
//using Extreme.Statistics.Multivariate;

namespace Kinect2FaceHD_NET
{
    public partial class MainWindow : Window

    {
        private KinectSensor _sensor = null;

        private BodyFrameSource _bodySource = null;

        private BodyFrameReader _bodyReader = null;

        private HighDefinitionFaceFrameSource _faceSource = null;

        private HighDefinitionFaceFrameReader _faceReader = null;

        private FaceAlignment _faceAlignment = null;

        private FaceModel _faceModel = null;

        private List<Ellipse> _points = new List<Ellipse>();
        private List<float> distance1 = new List<float>(6);
        private List<float> distance2 = new List<float>();
        private List<float> check = new List<float>(3);
        private List<Vector3D> _vectors = new List<Vector3D>(4);
        private List<Vector3D> _vectors1072 = new List<Vector3D>();
        private List<Vector3D> _vectors10 = new List<Vector3D>();
        private List<Vector3D> _vectors687 = new List<Vector3D>();
        private List<Vector3D> _vectors91 = new List<Vector3D>();

        private int index = 0;
        private int tt = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _bodySource = _sensor.BodyFrameSource;
                _bodyReader = _bodySource.OpenReader();
                _bodyReader.FrameArrived += BodyReader_FrameArrived;

                _faceSource = new HighDefinitionFaceFrameSource(_sensor);

                _faceReader = _faceSource.OpenReader();
                _faceReader.FrameArrived += FaceReader_FrameArrived;

                _faceModel = new FaceModel();
                _faceAlignment = new FaceAlignment();

                _sensor.Open();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_faceModel != null)
            {
                _faceModel.Dispose();
                _faceModel = null;
            }

            GC.SuppressFinalize(this);
        }

        private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    Body[] bodies = new Body[frame.BodyCount];
                    frame.GetAndRefreshBodyData(bodies);

                    Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();

                    if (!_faceSource.IsTrackingIdValid)
                    {
                        if (body != null)
                        {
                            _faceSource.TrackingId = body.TrackingId;

                        }
                    }
                }
            }
        }

        private void FaceReader_FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null && frame.IsFaceTracked)
                {
                    frame.GetAndRefreshFaceAlignmentResult(_faceAlignment);
                    UpdateFacePoints();
                }
            }
        }

        private void UpdateFacePoints()
        {
            if (_faceModel == null) return;

            var vertices = _faceModel.CalculateVerticesForAlignment(_faceAlignment);

            if (vertices.Count > 0)
            {
                if (_points.Count == 0)
                {
                    for (int index = 0; index < vertices.Count; index++)
                    {
                        if (index > 1119 && index < 1156)
                        {

                            Ellipse ellipse = new Ellipse
                            {
                                Width = 2.0,
                                Height = 2.0,
                                Fill = new SolidColorBrush(Colors.Blue)
                            };

                            _points.Add(ellipse);
                        }
                        else
                        {
                            Ellipse ellipse = new Ellipse
                            {
                                Width = 2.0,
                                Height = 2.0,
                                Fill = new SolidColorBrush(Colors.Blue)
                            };

                            _points.Add(ellipse);
                        }
                    }

                    foreach (Ellipse ellipse in _points)
                    {
                        canvas.Children.Add(ellipse);
                    }
                }

                for (int index = 0; index < vertices.Count; index++)
                {
                    float x10 = 0;
                    float y10 = 0;
                    float z10 = 0;
                    float x91 = 0;
                    float y91 = 0;
                    float z91 = 0;
                    float x687 = 0;
                    float y687 = 0;
                    float z687 = 0;
                    float x1072 = 0;
                    float y1072 = 0;
                    float z1072 = 0;
                    float x17 = 0;
                    float y17 = 0;
                    float z17 = 0;
                    float x453 = 0;
                    float y453 = 0;
                    float z453 = 0;
                    float x717 = 0;
                    float y717 = 0;
                    float z717 = 0;
                    float x1149 = 0;
                    float y1149 = 0;
                    float z1149 = 0;
                    float x1123 = 0;
                    float y1123 = 0;
                    float z1123 = 0;
                    float x1143 = 0;
                    float y1143 = 0;
                    float z1143 = 0;
                    float x1138 = 0;
                    float y1138 = 0;
                    float z1138 = 0;
                    float x1155 = 0;
                    float y1155 = 0;
                    float z1155 = 0;


                    CameraSpacePoint vertice = vertices[index];

                    DepthSpacePoint point = _sensor.CoordinateMapper.MapCameraPointToDepthSpace(vertice);

                    if (float.IsInfinity(point.X) || float.IsInfinity(point.Y)) return;

                    Ellipse ellipse = _points[index];

                    Canvas.SetLeft(ellipse, point.X);
                    Canvas.SetTop(ellipse, point.Y);
                    //base points
                    if (index == 17)//nose
                    {
                        x17 = vertice.X * 100;
                        y17 = vertice.Y * 100;
                        z17 = vertice.Z * 100;

                    }

                    if (index == 453)//left ear
                    {
                        x453 = vertice.X * 100;
                        y453 = vertice.Y * 100;
                        z453 = vertice.Z * 100;

                    }

                    if (index == 717)//right ear
                    {
                        x717 = vertice.X * 100;
                        y717 = vertice.Y * 100;
                        z717 = vertice.Z * 100;

                    }

                    //lip mid bottom  to nose
                    if (index == 1149)
                    {
                        x1149 = vertice.X * 100;
                        y1149 = vertice.Y * 100;
                        z1149 = vertice.Z * 100;
                        distance1.Add(euclidean(0, y1149, 0, 0, y17, 0));
                        distance1[4] = euclidean(0, y1149, 0, 0, y17, 0);
                    }
                    //lip mid up to mid mid low
                    if (index == 1072)
                    {
                        x1072 = vertice.X * 100;
                        y1072 = vertice.Y * 100;
                        z1072 = vertice.Z * 100;

                        distance1.Add(euclidean(0, y1072, 0, 0, y1149, 0));
                        distance1[1] = euclidean(0, y1072, 0, 0, y1149, 0);


                        Vector3D v3 = new Vector3D(x1072, y1072, z1072);
                        _vectors.Add(v3);
                        _vectors[3] = v3;
                        //ellipse.Fill = new SolidColorBrush(Colors.Green);


                    }

                    //left low mouth to right ear
                    if (index == 1123)
                    {
                        x1123 = vertice.X * 100;
                        y1123 = vertice.Y * 100;
                        z1123 = vertice.Z * 100;
                        distance1.Add(euclidean(x1123, y1123, 0, x717, y717, 0));
                        distance1[2] = euclidean(x1123, y1123, 0, x717, y717, 0);

                    }
                    //right low

                    if (index == 1143)
                    {
                        x1143 = vertice.X * 100;
                        y1143 = vertice.Y * 100;
                        z1143 = vertice.Z * 100;
                        distance1.Add(euclidean(x1143, y1143, 0, x453, y453, 0));
                        distance1[3] = euclidean(x1143, y1143, 0, x453, y453, 0);

                    }
                    //left point
                    if (index == 1138)
                    {
                        x1138 = vertice.X * 100;
                        y1138 = vertice.Y * 100;
                        z1138 = vertice.Z * 100;
                        distance1.Add(euclidean(x1138, y1138, 0, x717, y717, 0));
                        distance1[2] = euclidean(x1138, y1138, 0, x717, y717, 0);

                    }
                    //right point
                    if (index == 1155)
                    {
                        x1155 = vertice.X * 100;
                        y1155 = vertice.Y * 100;
                        z1155 = vertice.Z * 100;
                        distance1.Add(euclidean(x1155, y1155, 0, x453, y453, 0));
                        distance1[5] = euclidean(x1155, y1155, 0, x453, y453, 0);

                    }




                    if (index == 10)
                    {
                        x10 = vertice.X * 100;
                        y10 = vertice.Y * 100;
                        z10 = vertice.Z * 100;
                        //  distance1.Add(euclidean(x10, y10, z10, x17, y17, z17));
                        // distance1[0] = euclidean(x10, y10, z10, x17, y17, z17);
                        Vector3D v0 = new Vector3D(x10, y10, z10);
                        _vectors.Add(v0);
                        _vectors[0] = v0;


                    }
                    if (index == 91)
                    {
                        x91 = vertice.X * 100;
                        y91 = vertice.Y * 100;
                        z91 = vertice.Z * 100;
                        distance1.Add(euclidean(x91, y91, 0, x687, y687, 0));
                        distance1[0] = euclidean(x91, y91, 0, x687, y687, 0);
                        Vector3D v1 = new Vector3D(x91, y91, z91);
                        _vectors.Add(v1);
                        _vectors[1] = v1;
                        //ellipse.Fill = new SolidColorBrush(Colors.Green);


                    }

                    if (index == 687)
                    {
                        x687 = vertice.X * 100;
                        y687 = vertice.Y * 100;
                        z687 = vertice.Z * 100;
                        Vector3D v2 = new Vector3D(x687, y687, z687);
                        _vectors.Add(v2);
                        _vectors[2] = v2;


                    }





                }
            }

        }
        void Training(object sender, RoutedEventArgs e)
        {

            //if ((index==91)||(index==687)||(index==1072)||(index==10))
            // btn.Content = _vectors[0].Z.ToString();

            _vectors10.Add(_vectors[0]);
            _vectors10.Add(_vectors[1]);
            _vectors10.Add(_vectors[2]);
            _vectors10.Add(_vectors[3]);
            distance2.Add(distance1[0]);
            distance2.Add(distance1[1]);
            distance2.Add(distance1[2]);
            distance2.Add(distance1[3]);
            distance2.Add(distance1[4]);
            distance2.Add(distance1[5]);

            index++;
            //  btn.Content = _vectors10.Count.ToString();
            btn.Content = index.ToString();

        }
        void Reboot(object sender, RoutedEventArgs e)
        {

            _vectors10 = new List<Vector3D>();
            _vectors91 = new List<Vector3D>();
            _vectors687 = new List<Vector3D>();
            _vectors1072 = new List<Vector3D>();
            distance2 = new List<float>();
            tt++;
            index = 0;
            btn.Content = (tt.ToString());
        }

        void Csv(object sender, RoutedEventArgs e)
        {
            Dowmload_to_csv();
        }



        void Dowmload_to_csv()
        {

            StringBuilder csv = new StringBuilder();
            // csv.AppendLine(Avg(_vectors10).ToString() + "," + Avg(_vectors91).ToString() + "," + Avg(_vectors687).ToString() + "," + Avg(_vectors1072).ToString());

            //csv.AppendLine(pca[0].ToString("G5") + "I LOVE SPAGETHI");csv.AppendLine(Avg(_vectors10).ToString() + "," + Avg(_vectors91).ToString() + "," + Avg(_vectors687).ToString() + "," + Avg(_vectors1072).ToString());
            //
            for (int k = 5; k < distance2.Count; k++)
            {
                csv.Append(((float)Math.Abs(distance2[k] - distance2[k - 5]) * 100).ToString() + ",");
            }
            // csv.Append("i realy need help");

            string csvpath = "D:\\py.csv";
            csv.AppendLine(" ");
            File.AppendAllText(csvpath, csv.ToString());

        }
        float euclidean(float x0, float y0, float z0, float x1, float y1, float z1)
        {
            float deltaX = x1 - x0;
            float deltaY = y1 - y0;
            float deltaZ = z1 - z0;

            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            return distance;
        }



    }
}