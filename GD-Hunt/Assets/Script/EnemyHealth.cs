using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public  float health;
    public float maxHealth;

    public GameObject healthBar;
    public Slider slider;
    

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
        
    }

    private void Update()
    {
        
        slider.value = CalculateHealth();
        slider.transform.LookAt(Camera.main.transform.position);
        slider.transform.Rotate(0, 180, 0);
        healthBar.transform.LookAt(Camera.main.transform.position);
        healthBar.transform.Rotate(0, 180, 0);

        if (health < maxHealth)
        {
            healthBar.SetActive(true);
        }
        
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        
        

    }
    
    private float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    

}
