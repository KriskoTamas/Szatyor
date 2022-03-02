using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Windows.Kinect;
using System;
using System.Linq;

public class Program : MonoBehaviour
{
    public GameObject armLeft, armRight;
    private KinectSensor kinect = null;
    private BodyFrameReader bodyReader = null;
    Body[] bodies = null;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        armLeft = GameObject.Find("LeftArm");
        armRight = GameObject.Find("RightArm");
        KinectSensor sensor = KinectSensor.GetDefault();
        sensor.Open();
        bodyReader = sensor.BodyFrameSource.OpenReader();
        bodies = new Body[sensor.BodyFrameSource.BodyCount];
        bodyReader.FrameArrived += BodyReader_FrameArrived;
    }

    // Update is called once per frame
    void Update()
    {

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
                float ypos = body.Joints.First(x => x.Key == JointType.HandLeft).Value.Position.Y;
                float ypos2 = body.Joints.First(x => x.Key == JointType.HandRight).Value.Position.Y;
                armLeft.transform.Rotate(ypos*13, 0, 0);
                armRight.transform.Rotate(ypos2 * 13, 0, 0);
                Debug.Log(ypos);
            }
        }
    }
}
