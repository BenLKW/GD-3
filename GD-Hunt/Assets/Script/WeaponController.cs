using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject axe;
    public bool CanAttack=true;
    public float AttackCooldown = 1.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {

            }
        }   
    }

    public void AxeAttack()
    {
        CanAttack = false;
        Animator anim = axe.GetComponent<Animator>();
        anim.SetTrigger("Attack");
    }


    IEnumerator ResetAttackCoolDown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}
