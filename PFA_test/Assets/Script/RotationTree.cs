using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTree : MonoBehaviour
{
    //�ݴ� �������� �������� ���� ������ �ָ� ��.

    public float rotateSpeed;

    void Update()
    {
        this.transform.Rotate(new Vector3(0f, rotateSpeed, 0f) * Time.deltaTime, Space.Self);  
    }
}
