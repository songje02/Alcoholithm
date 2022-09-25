using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChasePlayer_Camera : MonoBehaviour
{
    private GameObject player; // 플레이어 오브젝트
    private Transform camera_Root; // 바라볼 플레이어 위치값
    private CinemachineVirtualCamera vcam;

    [SerializeField]
    private Transform cameraArm; // 움직일 카메라

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float CameraAngleOverride;

    public float BottomClamp = 0.0f;
    public float TopClamp = 180.0f;
    public float Sensitivity = 0.5f;

    private Vector3 dirNormalized;
    private Vector3 finalDir;
    private float finalDistance = 0.0f;
    public float minDistance = 0.0f;
    public float maxDistance = 0.0f;
    public float smoothness = 10.0f;

    [Header("CameraSetting")]
    public Vector3 Camera_StartPos;


    void Start()
    {
        this.transform.position = Camera_StartPos;
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = null;
        camera_Root = GameObject.Find("Player_character(Clone)").transform;
        player = camera_Root.GetChild(0).gameObject;
        dirNormalized = cameraArm.localPosition.normalized;
        finalDistance = cameraArm.localPosition.magnitude;
        _cinemachineTargetYaw = cameraArm.transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        Debug.Log(player.name);
        if (player == null)
        {
            player = GameObject.Find("Player_character(Clone)");

            if(player != null)
            {
                
            }
        }
        //vcam.Follow = player.transform;

        //LookAround();
    }

    //private void LookAround()
    //{
    //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //    Vector3 camAngle = cameraArm.rotation.eulerAngles;

    //    float x = camAngle.x - mouseDelta.y;
    //    if(x < 180.0f)
    //    {
    //        x = Mathf.Clamp(x, -1.0f, 70.0f);
    //    }
    //    else {
    //        x = Mathf.Clamp(x, 335f, 361f);
    //    }

    //    cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    //}

    private void CameraRotation()
    {
        _cinemachineTargetYaw += Input.GetAxis("Mouse X") * Sensitivity;
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

        _cinemachineTargetPitch -= Input.GetAxis("Mouse Y") * Sensitivity;
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        cameraArm.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);

       
        
    }

    private static float ClampAngle(float IfAngle, float min, float max)
    {
        if (IfAngle < -360.0f) IfAngle += 360.0f;
        if (IfAngle > 360.0f) IfAngle -= 360.0f;

        return Mathf.Clamp(IfAngle, min, max);
    }

    private void FollowCamera()
    {

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        cameraArm.localPosition = Vector3.Lerp(cameraArm.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }

    private void LateUpdate()
    {
        CameraRotation();

        //FollowCamera();
    }
}
