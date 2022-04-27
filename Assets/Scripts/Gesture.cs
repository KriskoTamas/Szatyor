using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class Gesture : MonoBehaviour
{

    private KinectSensor sensor;
    private BodyFrameReader bodyReader;
    private static Body[] bodies;
    private bool leftHandStill = false, rightHandStill = false; // for moving sideways
    private bool jumpStill = false; // for jumping

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
        if (!Game.inited)
        {
            sensor.Open();
            bodyReader = sensor.BodyFrameSource.OpenReader();
            bodies = new Body[sensor.BodyFrameSource.BodyCount];
            bodyReader.FrameArrived += BodyReader_FrameArrived;
            Game.inited = true;
        }
    }
    private void Update()
    {
        if (sensor != null && sensor.IsAvailable)
        {
            Game.kinectConnected = true;
            UIManager.kinectInputModule.enabled = true;
            if (!Game.started || Game.paused)
            {
                UIManager.handRight.SetActive(true);
                UIManager.handRightRing.SetActive(true);
            }
            UIManager.kinectInfoText.text = "A Kinect csatlakoztatva van";
            UIManager.kinectInfoText.color = new Color(166f/255f, 255f/255f, 77f/255f);
        }
        else
        {
            Game.kinectConnected = false;
            UIManager.kinectInputModule.enabled = false;
            UIManager.handRight.SetActive(false);
            UIManager.handRightRing.SetActive(false);
            UIManager.kinectInfoText.text = "A Kinect nincsen csatlakoztatva";
            UIManager.kinectInfoText.color = new Color(255f/255f, 128f/255f, 128f/255f);
        }
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

                // Pause Game Gesture //
                if (handLeft.z + 0.4 < head.z && handRight.z + 0.4 < head.z && Mathf.Abs(handLeft.x - handRight.x) <= 0.1 && Mathf.Abs(handLeft.y - handRight.y) <= 0.1)
                {
                    if (Game.started && !Game.paused)
                        Game.PauseResumeGame();
                }

                // Jump Gesture //
                if (handLeft.y - 0.05 < head.y && handRight.y - 0.05 < head.y)
                {
                    jumpStill = true;
                }

                if (jumpStill && handLeft.y - 0.15 > head.y && handRight.y - 0.15 > head.y)
                {
                    jumpStill = false;
                    Player.Jump();
                }

                // Left Hand Gesture //
                if (handLeft.x + 0.1 >= elbowLeft.x && handLeft.x - 0.1 <= elbowLeft.x && handLeft.y - 0.2 > elbowLeft.y)
                {
                    leftHandStill = true;
                }

                if (leftHandStill && handLeft.x + 0.2 < elbowLeft.x)
                {
                    leftHandStill = false;
                    Player.MoveLeft();
                }

                // Right Hand Gesture //
                if (handRight.x - 0.1 <= elbowRight.x && handRight.x + 0.1 >= elbowRight.x && handRight.y - 0.2 > elbowRight.y)
                {
                    rightHandStill = true;
                }

                if (rightHandStill && handRight.x - 0.2 > elbowRight.x)
                {
                    rightHandStill = false;
                    Player.MoveRight();
                }

            }
        }
    }
}
