using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChasePlayer_Camera : MonoBehaviour
{
    private GameObject player; // 플레이어 오브젝트
    private CinemachineVirtualCamera vcam;

    [SerializeField]
    private Transform cameraArm; // 움직일 카메라

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float CameraAngleOverride;

    public float BottomClamp = 0.0f;
    public float TopClamp = 180.0f;
    public float Sensitivity = 0.5f;


    [Header("CameraSetting")]
    public Vector3 Camera_StartPos;
    public GameObject CinemachineCamera_Target;


    void Start()
    {
        this.transform.position = Camera_StartPos;
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = null;
        player = GameObject.Find("Player_character(Clone)");
        CinemachineCamera_Target = player.transform.GetChild(0).gameObject;
        _cinemachineTargetYaw = CinemachineCamera_Target.transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player_character(Clone)");

            if(player != null)
            {
                
            }
        }
        vcam.Follow = CinemachineCamera_Target.transform;

    }


    private void CameraRotation()
    {
        _cinemachineTargetYaw += Input.GetAxis("Mouse X") * Sensitivity;
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

        _cinemachineTargetPitch -= Input.GetAxis("Mouse Y") * Sensitivity;
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCamera_Target.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);

    }

    private static float ClampAngle(float IfAngle, float min, float max)
    {
        if (IfAngle < -360.0f) IfAngle += 360.0f;
        if (IfAngle > 360.0f) IfAngle -= 360.0f;

        return Mathf.Clamp(IfAngle, min, max);
    }


    private void LateUpdate()
    {
        CameraRotation();
    }
}
