using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public RandomSpawner randomspawner;
    public DamageDealer DamageDealer;
    public  float health;
    public float maxHealth;
    public Quest quest;
    public NPCScript NPCScript;

    public GameObject healthBar;
    public Slider slider;
    public TargetLock TargetLock;


    private void Start()
    {
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        NPCScript = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
        TargetLock = GameObject.Find("Main Camera").GetComponent<TargetLock>();
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        DamageDealer = GameObject.Find("pCube1").GetComponent<DamageDealer>();
        health = maxHealth;
        slider.value = CalculateHealth();
        healthBar.SetActive(false);
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

        if (health <= 0)
        {
            
            TargetLock.isTargeting = false;
            randomspawner.enemyCount -= 1;

            if (NPCScript.isQuesting == true)
            {
                quest.currentAmount++;
            }
            Destroy(gameObject);
        }
    }
    
    private float CalculateHealth()
    {
        return health / maxHealth;
    }

    
       

    
}
