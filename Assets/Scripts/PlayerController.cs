using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public GameObject gameOverPanel;
    public static Transform playerTransform;
    public static float playerSpeed = 4;
    public static float forwardSpeed = 4;
    public static float laneDistance = 3;
    public static float jumpForce = 14;
    public static float gravity = -40;
    private int lane = 1; // 0: left, 1: middle, 2: right

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (UIManager.gameStarted)
        {
            direction.z = forwardSpeed;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lane--;
                if (lane < 0)
                    lane = 0;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lane++;
                if (lane > 2)
                    lane = 2;
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
            Vector3 moveDir = diff.normalized * Time.deltaTime * 10;
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
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("OnControllerColliderHit");
        //if(hit.transform.tag == "Obstacle")
        //{
        //    //Debug.Log("hit");
        //    Time.timeScale = 0;
        //    UIManager.gameOver = true;
        //    gameOverPanel.SetActive(true);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter");
        ContactPoint point = collision.GetContact(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
    }

}
