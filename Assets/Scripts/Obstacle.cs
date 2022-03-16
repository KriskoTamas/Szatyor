using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colliosion enter");
    }
}
