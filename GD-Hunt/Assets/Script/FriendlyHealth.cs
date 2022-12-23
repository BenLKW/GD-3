using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyHealth : MonoBehaviour
{
    //public Animator animator;
    public RandomSpawner randomspawner;
    public PlayerMovement PM;
    //public EnemyAI enemyAI;
    
    public  float health;
    public float maxHealth;
    public Quest quest;
    public NPCScript NPCScript;

    public GameObject healthBar;
    public Slider slider;
    public TargetLock TargetLock;


    private void Start()
    {
        //animator = GetComponent<Animator>();
        //enemyAI = GetComponent<EnemyAI>();
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        NPCScript = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
        TargetLock = GameObject.Find("Main Camera").GetComponent<TargetLock>();
        randomspawner = GameObject.Find("AnimalSpawner").GetComponent<RandomSpawner>();
        PM = GameObject.Find("/Player_Test/Player").GetComponent<PlayerMovement>();


        health = maxHealth;
        slider.value = CalculateHealth();
        healthBar.SetActive(false);
    }

    private void Update()
    {
        
        slider.value = CalculateHealth();
        slider.transform.LookAt(Camera.main.transform.position);
        //slider.transform.Rotate(0, 180, 0);
        healthBar.transform.LookAt(Camera.main.transform.position);
        //healthBar.transform.Rotate(0, 180, 0);

        if (health < maxHealth)
        {
            healthBar.SetActive(true);
        }
        
        
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health < 3)
        {
            //enemyAI.lowHealth = true;
        }
        else
        {
            //enemyAI.lowHealth = false;
        }

        if (health <= 0)
        {
            Destroy(GetComponent<CapsuleCollider>());
            TargetLock.isTargeting = false;
            //animator.SetFloat("Speed", 0);
            //animator.SetBool("Dead", true);
            //enemyAI.isDead = true;

            if (PM.TotalAid < 5)
            {
                PM.TotalAid += 1;
            }
            
            randomspawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
    
    private float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void dead()
    {
        
        //if (NPCScript.isQuesting == true)
        {
            //quest.currentAmount++;
        }
        randomspawner.enemyCount -= 1;
        
        Destroy(gameObject);
    }
       

    
}
