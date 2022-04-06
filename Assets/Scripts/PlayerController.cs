using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public static GameObject player;
    public static Transform playerTransform;
    public static float forwardSpeed;
    private static int lane = 1; // 0: left, 1: middle, 2: right

    // Constant values //
    public const float playerSpeed = 4;
    public const float gravity = -40;
    public const float jumpForce = 14;
    public const float laneDistance = 4.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        forwardSpeed = playerSpeed;
    }

    void Update()
    {
        if (Game.started)
        {
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
        controller.Move(direction * Time.deltaTime);
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
        print("OnControllerColliderHit");
        //if(hit.transform.tag == "Obstacle")
        //{
        //    //Debug.Log("hit");
        //    Time.timeScale = 0;
        //    Game.over = true;
        //    gameOverPanel.SetActive(true);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter");
        //ContactPoint point = collision.GetContact(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
    }

}
