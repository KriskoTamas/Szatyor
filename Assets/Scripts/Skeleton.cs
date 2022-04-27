using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    private CharacterController controller;
    private static Transform skeletonTransform;
    private static GameObject skeleton;
    private static Vector3 direction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        skeleton = GameObject.Find("Skeleton");
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.started && !Game.over)
        {
            direction.z = Player.forwardSpeed;

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

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
}
