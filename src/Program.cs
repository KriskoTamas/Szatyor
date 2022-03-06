using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Windows.Kinect;
using System;
using System.Linq;

public class Program : MonoBehaviour
{
    public GameObject armLeft, armRight, handLeft, handRight, spine;
    public GameObject Block;
    private KinectSensor kinect = null;
    private BodyFrameReader bodyReader = null;
    public static int width = 10;
    public static int height = 4;
    Body[] bodies = null;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello!");

        initObjects();

        KinectSensor sensor = KinectSensor.GetDefault();
        sensor.Open();
        bodyReader = sensor.BodyFrameSource.OpenReader();
        bodies = new Body[sensor.BodyFrameSource.BodyCount];
        bodyReader.FrameArrived += BodyReader_FrameArrived;

        //cube = GameObject.Find("MyObject");
        //cube = Resources.Load("CubeRig", typeof(GameObject)) as GameObject;

        // Destroy(cube);

        // Block.GetComponent<Camera>().enabled = true;

        //Block = Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
        //Block.GetComponentInChildren<Camera>().enabled = false;
        // Debug.Log(c);
        //for (int i = 0; i < Block.transform.childCount; i++)
        //{
        //    Debug.Log(Block.transform.GetChild(i).name);
        //}
        //Camera[] allCameras = FindObjectsOfType<Camera>();
        //Debug.Log(allCameras.Length);
        //foreach (var item in allCameras)
        //{
        //    //Debug.Log(item.name);
        //    if (item.name != "Main Camera")
        //        item.enabled = false;
        //}
        //Camera.main.fieldOfView = 45;
        //Camera c = GameObject.Find("Block").GetComponent<Camera>();
        //Debug.Log(c);


        //for (int y = 0; y < height; ++y)
        //{
        //    for (int x = 0; x < width; ++x)
        //    {
        //        Block = Instantiate(Block, new Vector3(x + x, y + y, 0), Quaternion.identity);
        //        Block.GetComponentInChildren<Camera>().enabled = false;
        //    }
        //}

        //Camera.main.ViewportToWorldPoint(new Vector3(-15f, 0f, 0f));

        //Instantiate(Block, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity/*, myPrefab.transform*/);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("asd");
        //Camera obj = Block.GetComponent<Camera>();
        //if (obj != null)
        //    Debug.Log("cam");
    }

    void initObjects()
    {
        armLeft = GameObject.Find("Shoulder.L");
        armRight = GameObject.Find("Shoulder.R");
        handLeft = GameObject.Find("Hand.L");
        handRight = GameObject.Find("Hand.R");
        spine = GameObject.Find("Spine");
        Debug.Log("x: " + armLeft.transform.position.x + " y: " + armLeft.transform.position.y + " z: " + armLeft.transform.position.z);
    }

    float[] getJointPos(JointType joint)
    {
        Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();
        float[] pos = new float[3];
        pos[0] = body.Joints.First(x => x.Key == joint).Value.Position.X;
        pos[1] = body.Joints.First(x => x.Key == joint).Value.Position.Y;
        pos[2] = body.Joints.First(x => x.Key == joint).Value.Position.Z;
        return pos;
    }

    void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null)
            {
                frame.GetAndRefreshBodyData(bodies);

                float[] pos = getJointPos(JointType.HandLeft);
                Vector3 armPos = armLeft.transform.position;

                Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();
                Debug.Log(body.Lean.X);
                //armLeft.transform.position = new Vector3(armPos.x + pos[0]/10, armPos.y + pos[1]/10, armPos.z + pos[2]/10);
                //Debug.Log("x: " + pos[0] + " y: " + pos[1] + " z: " + pos[2]);
                spine.transform.Rotate(body.Lean.Y, body.Lean.X, 0);
            }
        }
    }
}
