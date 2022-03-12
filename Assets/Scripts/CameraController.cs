using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        transform.position = position;
    }
}
