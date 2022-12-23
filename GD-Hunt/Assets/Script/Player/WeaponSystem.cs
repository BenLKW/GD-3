using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject weaponHolder;

    public GameObject weaponSheath;

    public GameObject weapon;
    public GameObject currentWeaponInHand;
    public GameObject currentWeaponInSheath;

    void Start()
    {
        weapon = null;
        //currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }

    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }

    
}
