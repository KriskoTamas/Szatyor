using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float laneDistance;
    public float jumpForce = 5;
    public float gravity = -5;
    private int lane = 1; // 0: left, 1: middle, 2: right

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane--;
            if(lane < 0)
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
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.fixedDeltaTime;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if(lane == 2)
            targetPosition += Vector3.right * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
