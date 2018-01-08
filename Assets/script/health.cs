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
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            currentHealth -= 1;
        }
        if (currentHealth == 0)
            print("dead");

	}
}
