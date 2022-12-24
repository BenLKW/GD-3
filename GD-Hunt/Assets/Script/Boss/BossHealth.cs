using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Animator anim;
    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //slider.value = CalculateHealth();
        healthBar.SetMaxHealth(maxHealth);
        //healthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
       

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
        //blinkTimer = blinkDuration;
    }
    public void Die()
    {
        anim.SetBool("Dead", true);
        Destroy(GetComponent<BoxCollider>());
        
        anim.SetFloat("Speed", 0);
    }
}
