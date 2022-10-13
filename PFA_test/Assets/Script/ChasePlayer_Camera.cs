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

    public float BottomClamp = 0.0f;
    public float TopClamp = 180.0f;
    public float Sensitivity = 0.5f;


    [Header("CameraSetting")]
    public Vector3 Camera_StartPos;
    public GameObject CinemachineCamera_Target;
    float Camera_MaxDistance = 15.0f;
    float Camera_MinDistance = 0.0f;

    CinemachineComponentBase componentBase;

    void Start()
    {
        this.transform.position = Camera_StartPos;
        vcam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        player = null;
        player = GameObject.Find("Player_character(Clone)");
        CinemachineCamera_Target = player.transform.GetChild(0).gameObject;
        _cinemachineTargetYaw = CinemachineCamera_Target.transform.rotation.eulerAngles.y;
        componentBase = vcam.GetCinemachineComponent(CinemachineCore.Stage.Body);
    }

    void Update()
    {
        //if (player == null)
        //{
        //    player = GameObject.Find("Player_character(Clone)");

        //    if(player != null)
        //    {
                
        //    }
        //}
        vcam.Follow = CinemachineCamera_Target.transform;


        RaycastHit hit;
        Vector3 Cam_Dir = cameraArm.transform.position - CinemachineCamera_Target.transform.position;

        Debug.DrawRay(CinemachineCamera_Target.transform.position, Cam_Dir, Color.green);

        if (Physics.Raycast(CinemachineCamera_Target.transform.position, Cam_Dir, out hit, Camera_MaxDistance, LayerMask.GetMask("Wall")))
        {
            if (componentBase is Cinemachine3rdPersonFollow)
            {
                float final_distance = Mathf.Clamp(hit.distance, Camera_MinDistance, Camera_MaxDistance);
                (componentBase as Cinemachine3rdPersonFollow).CameraDistance = final_distance;
            }
        }
        else
        {
            if (componentBase is Cinemachine3rdPersonFollow)
            {
                if ((componentBase as Cinemachine3rdPersonFollow).CameraDistance < 15.0f)
                {
                    (componentBase as Cinemachine3rdPersonFollow).CameraDistance = Camera_MaxDistance;
                }
            }
        }
    }


    private void CameraRotation()
    {
        _cinemachineTargetYaw += Input.GetAxis("Mouse X") * Sensitivity;
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

        _cinemachineTargetPitch -= Input.GetAxis("Mouse Y") * Sensitivity;
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCamera_Target.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);

     
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
