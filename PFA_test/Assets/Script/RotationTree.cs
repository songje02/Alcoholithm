using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTree : MonoBehaviour
{
    //�ݴ� �������� �������� ���� ������ �ָ� ��.

    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(rotateSpeed, 0f, 0f) * Time.deltaTime, Space.World);        
    }
}
