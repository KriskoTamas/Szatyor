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

            if (Player.GetLane() == 0)
                targetPosition += Vector3.left * Player.laneDistance;
            else if (Player.GetLane() == 2)
                targetPosition += Vector3.right * Player.laneDistance;

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
            controller.Move(Player.GetDirection() * Time.deltaTime);
    }

    public static void SetAnimation(bool boolean)
    {
        skeleton.GetComponent<Animator>().enabled = boolean;
    }
}
