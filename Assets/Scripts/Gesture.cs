using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class Gesture : MonoBehaviour
{

    private KinectSensor sensor;
    private BodyFrameReader bodyReader;
    private static Body[] bodies;
    private bool leftHandStill, rightHandStill;

    public static Vector3 GetJointPos(JointType joint)
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
    private void Update()
    {
        if (sensor.IsAvailable)
        {
            Game.kinectConnected = true;
            UIManager.kinectInfoText.text = "A Kinect csatlakoztatva van";
            UIManager.kinectInfoText.color = new Color(166f/255f, 255f/255f, 77f/255f);
        }
        else
        {
            Game.kinectConnected = false;
            UIManager.kinectInfoText.text = "A Kinect nincsen csatlakoztatva";
            UIManager.kinectInfoText.color = new Color(255f/255f, 128f/255f, 128f/255f);
        }
        // print("sensor available: " + sensor.IsAvailable);
    }

    void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null)
            {
                frame.GetAndRefreshBodyData(bodies);

                Vector3 head = GetJointPos(JointType.Head);
                Vector3 handLeft = GetJointPos(JointType.HandLeft);
                Vector3 handRight = GetJointPos(JointType.HandRight);

                Vector3 elbowLeft = GetJointPos(JointType.ElbowLeft);
                Vector3 elbowRight = GetJointPos(JointType.ElbowRight);

                Vector3 leftFoot = GetJointPos(JointType.FootLeft);
                Vector3 rightFoot = GetJointPos(JointType.FootRight);

                //print("head: " + head.y + " handLeft: " + handLeft.y + " handRight: " + handRight.y);
                if (handLeft.y > head.y && handRight.y > head.y)
                {
                    if(Game.started && !Game.paused)
                    {
                        Game.PauseResumeGame();
                    }
                }

                if(handRight.x - 0.2 > elbowRight.x)
                {
                    Player.MoveRight();
                    //print("jobbra");
                }
                if(handLeft.x + 0.2 < elbowLeft.x)
                {
                    Player.MoveLeft();
                    //print("balra");
                }
                print(handRight.x);
            }
        }
    }
}
