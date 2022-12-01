using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class TargetLock : MonoBehaviour
{
    [Header("Objects")]
    [Space]
    [SerializeField] private Camera mainCamera;            // your main camera object.
    [SerializeField] public CinemachineFreeLook cinemachineFreeLook; //cinemachine free lock camera object.
    [Space]
    [Header("UI")]
    [SerializeField] private Image aimIcon;  // ui image of aim icon u can leave it null.
    [Space]
    [Header("Settings")]
    [Space]
    [SerializeField] private string enemyTag; // the enemies tag.
    [SerializeField] private string AnimalTag;
    [SerializeField] public Vector2 targetLockOffset;
    [SerializeField] private float minDistance; // minimum distance to stop rotation if you get close to target
    [SerializeField] private float maxDistance;
    [SerializeField] public Vector3 viewPos;
    [Space]

    public bool isTargeting;
    public ThirdPersonCamMovement ThirdPersonCamMovement;

    private float maxAngle;
    public Transform currentTarget;
    

    void Start()
    {
        maxAngle = 90f; // always 90 to target enemies in front of camera.
       
    }

    void Update()
    {
        if (aimIcon)
            aimIcon.gameObject.SetActive(isTargeting);

        if (isTargeting)
        {
            NewInputTarget(currentTarget);
            ThirdPersonCamMovement.TargetLookAt = currentTarget.transform;
            ThirdPersonCamMovement.CurrentMode = ThirdPersonCamMovement.CameraMode.Lock;
        }
        else
        {
            ThirdPersonCamMovement.CurrentMode = ThirdPersonCamMovement.CameraMode.Basic;
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            AssignTarget();
        }
    }

    public void TargetForCall()
    {
        NewInputTarget(currentTarget.GetComponent<Transform>());
    }
    private void AssignTarget()
    {
        if (isTargeting)
        {
            isTargeting = false;
            currentTarget = null;
            return;
        }

        if (ClosestTarget())
        {
            currentTarget = ClosestTarget().transform;
            GameObject healthbar = currentTarget.transform.GetChild(4).gameObject;
            healthbar.SetActive(true);
            isTargeting = true;
            
        }
    }

    void NewInputTarget(Transform target) // sets new input value.
    {
        if (!currentTarget)
        return;

        viewPos = mainCamera.WorldToViewportPoint(target.position);




        if (aimIcon)
        
            aimIcon.transform.position = mainCamera.WorldToScreenPoint(target.position);
        
        
    }


    private GameObject ClosestTarget() // this is modified func from unity Docs ( Gets Closest Object with Tag ). 
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        float distance = maxDistance;
        float currAngle = maxAngle;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.magnitude;
            if (curDistance < distance)
            {
                Vector3 viewPos = mainCamera.WorldToViewportPoint(go.transform.position);
                Vector2 newPos = new Vector3(viewPos.x - 0.5f, viewPos.y - 0.5f);
                if (Vector3.Angle(diff.normalized, mainCamera.transform.forward) < maxAngle)
                {
                    closest = go;
                    currAngle = Vector3.Angle(diff.normalized, mainCamera.transform.forward.normalized);
                    distance = curDistance;
                }
            }
        }
        return closest;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
