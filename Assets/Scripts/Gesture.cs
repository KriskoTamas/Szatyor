using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class Gesture : MonoBehaviour
{

    [SerializeField]
    private UIManager manager;
    private KinectSensor sensor = null;
    private BodyFrameReader bodyReader = null;
    private Body[] bodies = null;

    Vector3 getJointPos(JointType joint)
    {
        Body body = bodies.Where(x => x.IsTracked).FirstOrDefault();
        Vector3 pos = new Vector3();
        pos.x = body.Joints.First(x => x.Key == joint).Value.Position.X;
        pos.y = body.Joints.First(x => x.Key == joint).Value.Position.Y;
        pos.z = body.Joints.First(x => x.Key == joint).Value.Position.Z;
        return pos;
    }

    void Start()
    {
        sensor = KinectSensor.GetDefault();
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

                Vector3 head = getJointPos(JointType.Head);
                Vector3 handLeft = getJointPos(JointType.HandLeft);
                Vector3 handRight = getJointPos(JointType.HandRight);

                //print("head: " + head.y + " handLeft: " + handLeft.y + " handRight: " + handRight.y);
                if (handLeft.y > head.y && handRight.y > head.y)
                {
                    if(UIManager.gameStarted && !UIManager.gamePaused)
                    {
                        manager.PauseResumeGame();
                    }
                }
            }
        }
    }
}
