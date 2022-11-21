using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCamMovement : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Transform TargetLookAt;
    public Rigidbody rb;

    public TargetLock TargetLock;

    float horizontalInput;
    float verticalInput;

    public float rotationSpeed;

    public CameraMode CurrentMode;

    public Health Health;

    public enum CameraMode
    {
        Basic,
        Lock
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.health > 0)
        {
            if (CurrentMode == CameraMode.Basic)
            {
                Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                orientation.forward = viewDir.normalized;

                cinemachineFreeLook.m_XAxis.m_InputAxisName = "Mouse X";
                cinemachineFreeLook.m_YAxis.m_InputAxisName = "Mouse Y";

                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");



                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }


            }
            else if (CurrentMode == CameraMode.Lock)
            {
                Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                orientation.forward = viewDir.normalized;

                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");

                cinemachineFreeLook.m_XAxis.m_InputAxisName = "";
                cinemachineFreeLook.m_YAxis.m_InputAxisName = "";
                cinemachineFreeLook.m_XAxis.m_InputAxisValue = (TargetLock.viewPos.x - 0.5f + TargetLock.targetLockOffset.x) * 3f;
                cinemachineFreeLook.m_YAxis.m_InputAxisValue = (TargetLock.viewPos.y - 0.5f + TargetLock.targetLockOffset.y) * 1f;

                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
            }
        }

        
        

    }
}
