using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Windows.Kinect;
using System;
using System.Linq;

public class Program : MonoBehaviour
{
    private KinectSensor kinect = null;
    private BodyFrameReader bodyReader = null;
    Body[] bodies = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        KinectSensor sensor = KinectSensor.GetDefault();
        sensor.Open();
        bodyReader = sensor.BodyFrameSource.OpenReader();
        bodies = new Body[sensor.BodyFrameSource.BodyCount];
        bodyReader.FrameArrived += BodyReader_FrameArrived;
    }

    void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null)
            {
                frame.GetAndRefreshBodyData(bodies);

                Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();
                if (body != null)
                    bodies[0] = body;

                // Debug.Log(body.Lean.X);
                Debug.Log(body.Joints.First(x => x.Key == JointType.HandLeft).Value.Position.Y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*BodySourceManager a = new BodySourceManager();
        Body[] bodies = a.GetData();
        Debug.Log(bodies);*/
    }

    private static double VectorLength(CameraSpacePoint point)
    {
        var result = Math.Pow(point.X, 2) + Math.Pow(point.Y, 2) + Math.Pow(point.Z, 2);

        result = Math.Sqrt(result);

        return result;
    }

    private static Body FindClosestBody(BodyFrame bodyFrame)
    {
        Body result = null;
        double closestBodyDistance = double.MaxValue;

        Body[] bodies = new Body[bodyFrame.BodyCount];
        bodyFrame.GetAndRefreshBodyData(bodies);

        foreach (var body in bodies)
        {
            if (body.IsTracked)
            {
                var currentLocation = body.Joints[JointType.SpineBase].Position;

                var currentDistance = VectorLength(currentLocation);

                if (result == null || currentDistance < closestBodyDistance)
                {
                    result = body;
                    closestBodyDistance = currentDistance;
                }
            }
        }

        return result;
    }
}
