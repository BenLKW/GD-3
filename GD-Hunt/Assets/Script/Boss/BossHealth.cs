using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Animator anim;
    public BossAI bossAI;

    //NPC and Quest
    public Quest quest;
    public NPCScript NPCScript;

    //Health
    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;
    public GameObject Bar;

    //Snesor
    public FieldOfView fov;

    public SkinnedMeshRenderer skinnedMeshRenderer;
    Color originalColor;


    void Start()
    {

        //bossAI = GetComponent<BossAI>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Bar.SetActive(false);
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        NPCScript = GameObject.Find("NPC_Test").GetComponent<NPCScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.canSeePlayer&bossAI.isDead==false)
        {
            Bar.SetActive(true);

        }
        else
        {
            Bar.SetActive(false);
        }
       

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
        FlashRed();
    }

    public IEnumerator FlashRed()
    {

        skinnedMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        ResetColor();
    }
    void ResetColor()
    {
        skinnedMeshRenderer.material.color= originalColor;
    }
    public void Die()
    {
        anim.SetBool("Dead", true);
        //Destroy(GetComponent<BoxCollider>());
        bossAI.isDead = true;
        anim.SetFloat("Speed", 0);
        if (NPCScript.isQuesting == true)
        {
            quest.currentAmount++;
        }
    }
   
   
}
