using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShowcase : MonoBehaviour
{

    public Transform PressF;
    public PickUpWeapon pickUp;
    public GameObject NPCGUI, NPCGUIText;
    public float lookRadius = 10f;
    public bool playerInLookingRadius;
    public LayerMask whatIsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PressF.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z);
        //pickUp = GetComponent<PickUpWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCGUI != null)
        {
           
            NPCGUIText.transform.LookAt(Camera.main.transform.position);
            NPCGUIText.transform.Rotate(0, 180, 0);
        }

        playerInLookingRadius = Physics.CheckSphere(transform.position, lookRadius, whatIsPlayer);

        if (playerInLookingRadius)
        {
            NPCGUI.SetActive(true);
            

        }
        else if (!playerInLookingRadius)
        {
            NPCGUI.SetActive(false);
        }
        if(pickUp.weaponIsPicked==true)
        {
            NPCGUI.SetActive(false);


        }
    }
}
