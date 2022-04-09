using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    private static GameObject player;
    private static Transform playerTransform;
    public static float forwardSpeed;
    private static int lane = 1; // 0: left, 1: middle, 2: right

    // Constant values //
    public const float defaultSpeed = 4;
    public const float gravity = -40;
    public const float jumpForce = 14;
    public const float laneDistance = 4.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        forwardSpeed = defaultSpeed;
    }

    void Update()
    {
        if (Game.started && !Game.over)
        {
            //if (GetDistance() > 0 && GetDistance() % 50 == 0)
            //{
            //    forwardSpeed *= 1.05f;
            //}

            direction.z = forwardSpeed;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Jump();
                }
            }
            else
            {
                direction.y += gravity * Time.deltaTime;
            }

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (lane == 0)
                targetPosition += Vector3.left * laneDistance;
            else if (lane == 2)
                targetPosition += Vector3.right * laneDistance;

            if (transform.position == targetPosition) // no changes are made
                return;

            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * Time.deltaTime * 20;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }
    }

    private void FixedUpdate()
    {
        if (Game.started && !Game.over)
            controller.Move(direction * Time.deltaTime);
    }

    public static Vector3 GetPos()
    {
        if(playerTransform == null) return Vector3.zero;
        return playerTransform.position;
    }

    public static int GetDistance()
    {
        return (int) GetPos().z;
    }

    public static void SetAnimation(bool boolean)
    {
        player.gameObject.GetComponent<Animator>().enabled = boolean;
    }

    private void Jump()
    {
        if (!Game.paused && Game.started)
            direction.y = jumpForce;
    }

    public static void MoveLeft()
    {
        if (!Game.paused && Game.started)
        {
            lane--;
            if (lane < 0) lane = 0;
        }
    }

    public static void MoveRight()
    {
        if (!Game.paused && Game.started)
        {
            lane++;
            if (lane > 2) lane = 2;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print("OnControllerColliderHit");
        if (hit.transform.tag == "Obstacle" && !Game.over)
        {
            Game.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("OnCollisionEnter");
        //ContactPoint point = collision.GetContact(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("OnTriggerEnter");
    }

}
