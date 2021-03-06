using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private static Transform playerTransform;
    private static GameObject player;
    private static Vector3 direction;
    private static int lane = 1; // 0: left, 1: middle, 2: right
    public static float forwardSpeed;
    public static string playerName = "Player";

    public static int score = 0, highscore = 0;

    // Constant values //
    public const float defaultSpeed = 5;
    public const float gravity = -40;
    public const float jumpForce = 14;
    public const float laneDistance = 4f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        forwardSpeed = defaultSpeed;
        lane = 1;
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
            Vector3 moveDir = 20 * Time.deltaTime * diff.normalized;
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
        if (playerTransform == null) return Vector3.zero;
        return playerTransform.position;
    }

    public static Vector3 GetDirection()
    {
        return direction;
    }

    public static int GetLane()
    {
        return lane;
    }

    public static int GetDistance()
    {
        return (int) GetPos().z;
    }

    public static void SetAnimation(bool boolean)
    {
        player.GetComponent<Animator>().enabled = boolean;
    }

    public static void Jump()
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
        if (hit.transform.CompareTag("Obstacle") && !Game.over)
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
