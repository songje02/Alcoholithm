using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChasePlayer_Camera : MonoBehaviour
{
    private GameObject player;
    private Transform Follow_Target;
    private CinemachineVirtualCamera vcam;


    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = null;
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player_character(Clone)");

            if(player != null)
            {
                vcam.Follow = player.transform;
                vcam.m_LookAt = player.transform;
            }
        }
      

        
    }

    private void LateUpdate()
    {
       
    }
}
