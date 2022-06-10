using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float dist = 7f;
    public float height = 5f;

    void Update()
    {
        transform.position = player.position - (1 * Vector3.forward * dist)
            + (Vector3.up * height);
        transform.LookAt(player); 
    }
}
