using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTree : MonoBehaviour
{
    //반대 방향으로 돌리려면 값을 음수로 주면 됨.

    public float rotateSpeed;

    void Update()
    {
        this.transform.Rotate(new Vector3(0f, rotateSpeed, 0f) * Time.deltaTime, Space.Self);  
    }
}
