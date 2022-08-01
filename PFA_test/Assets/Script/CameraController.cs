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

    void Start()
    {
        instatiate_player = GameObject.Find("Player_character(Clone)");
        playerTrans = instatiate_player.transform;
    }

    // Update is called once per frame
    void Update()
    {

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

        transform.LookAt(playerTrans.position);
    }

}
