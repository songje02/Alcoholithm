using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public float camera_Speed = 0.1f;
    public float dist;

    Transform playerTrans;
    GameObject instatiate_player; // 생성된 플레이어
    RaycastHit hit;

    [SerializeField]
    private Transform cameraArm;

    void Start()
    {
        instatiate_player = GameObject.Find("Player_character(Clone)");
        playerTrans = instatiate_player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }


    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        if (x < 180.0f)
        {
            x = Mathf.Clamp(x, -1.0f, 70.0f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }


    private void LateUpdate()
    {
        dist = Vector3.Distance(playerTrans.position, transform.position);
        if (dist > 15.0f)
        {
            Vector3 direction = playerTrans.position + offset;
            Vector3 chase_Player = Vector3.Lerp(transform.position, direction, camera_Speed * Time.deltaTime);
            transform.position = new Vector3(chase_Player.x, chase_Player.y, chase_Player.z);
        }
        

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            if (hit.transform.CompareTag("wall"))
            {
                transform.position = hit.point;
                Debug.Log("벽 충돌");
            }
        }
    }

}
