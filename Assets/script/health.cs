using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public int maxHealth;
    public Slider healthBar;
    
	// Use this for initialization
	void Awake () {
        maxHealth = 100;
        currentHealth = startingHealth;
        print("health script start");
        //healthBar = GetComponent<Slider>();
        healthBar.value = currentHealth / maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            currentHealth -= 1;
            healthBar.value = (float)currentHealth / maxHealth;
            print("health bar value " + healthBar.value);
            print("current health value " + currentHealth);
        }
        if (currentHealth == 0)
            print("dead");

	}
}
