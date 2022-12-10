using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider slider;
    public Animator animator;

    //public PlayerMovement PM;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            healthBar.SetActive(false);
        }
        
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        animator.SetTrigger("GetHit");
    }
    private float CalculateHealth()
    {
        return health / maxHealth;
    }


}
