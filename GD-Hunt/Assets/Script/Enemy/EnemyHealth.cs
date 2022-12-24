using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public RandomSpawner randomspawner;
    public EnemyAI enemyAI;
    
    public  float currentHealth;
    public float maxHealth;
    public Quest quest;
    public NPCScript NPCScript;
     public SkinnedMeshRenderer skinnedMeshRenderer;
    Color originalColor;
    public GameObject healthBar;
    public Slider slider;
    public TargetLock TargetLock;

    


    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        NPCScript = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
        TargetLock = GameObject.Find("Main Camera").GetComponent<TargetLock>();
        randomspawner = GameObject.Find("EnemySpawner").GetComponent<RandomSpawner>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        
        currentHealth = maxHealth;
        slider.value = CalculateHealth();
        healthBar.SetActive(false);
        originalColor = skinnedMeshRenderer.material.color;
    }

    private void Update()
    {
        
        slider.value = CalculateHealth();
        slider.transform.LookAt(Camera.main.transform.position);
        
        healthBar.transform.LookAt(Camera.main.transform.position);
        

        if (currentHealth < maxHealth)
        {
            healthBar.SetActive(true);
        }
        
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }


        LowHealthState();
        

    }

    public IEnumerator FlashRed()
    {

        skinnedMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        ResetColor();
    }
    void ResetColor()
    {
        skinnedMeshRenderer.material.color = originalColor;
    }
    private void LowHealthState()
    {
        if (currentHealth < 3)
        {
            enemyAI.lowHealth = true;
        }
        else
        {
            enemyAI.lowHealth = false;
        }
        
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
        StartCoroutine(FlashRed());
      
    }
   
    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public void Die()
    {
        animator.SetBool("Dead", true);
        Destroy(GetComponent<BoxCollider>());
        TargetLock.isTargeting = false;
        animator.SetFloat("Speed", 0);
        enemyAI.isDead = true;
        randomspawner.enemyCount -= 1;

        if (NPCScript.isQuesting == true)
        {
            quest.currentAmount++;
        }
        
        
    }
       
    public void Clear()
    {
        Destroy(gameObject);

    }
    
}
