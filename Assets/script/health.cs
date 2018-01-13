using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public int maxHealth;
    public Slider healthBar;
    public float iframe;
    public Animator anim;
	// Use this for initialization
	void Awake () {
        maxHealth = 100;
        currentHealth = startingHealth;
        //healthBar = GetComponent<Slider>();
        anim = GetComponent<Animator>();
        healthBar.value = currentHealth / maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (iframe > 0)
            iframe -= Time.deltaTime;
        if (currentHealth == 0)
            print("dead");
	}

    public void damage(int dmg)
    {
        print("damage called");
        if (iframe <= 0)//iframe less than 0 means can be damaged
        {
            anim.Play("hurt");
            currentHealth -= dmg;
            iframe = 1.0f;
            healthBar.value = (float)currentHealth / maxHealth;
        }
        
    }
    public void heal(int value)
    {
        print("heal is called");
        if (currentHealth <= 0)
        {
            print("player is dead lulz");
        }
        else
        {
            currentHealth += value;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }
}
