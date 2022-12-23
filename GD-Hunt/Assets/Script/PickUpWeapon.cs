using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public WeaponSystem WS;
    public GameObject WhatWeapon;
    public bool weaponIsPicked;
    // Start is called before the first frame update
    void Start()
    {
        WS = GameObject.Find("/Player_Test/Player").GetComponent<WeaponSystem>();
        weaponIsPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        
            if (other.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(WS.currentWeaponInSheath);
                    Destroy(WS.currentWeaponInHand);
                    WS.weapon = WhatWeapon;
                    
                if (WS.weapon != null)
                {
                    WS.currentWeaponInSheath = Instantiate(WS.weapon, WS.weaponSheath.transform);
                    weaponIsPicked = true;
                }
                }
            
            }
    }

}
