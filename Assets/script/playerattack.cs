using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code ref: https://www.youtube.com/watch?v=xwPahXLpNh8
public class playerattack : MonoBehaviour {

    private bool attack;
    private float timer;
    private float attCD = 0.3f;
    public Collider2D trigger;
    private Animator anim;
    // Use this for initialization
    void Awake() {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.name == "player")
            attackDetect("_p1");
        else
            attackDetect("_p2");
    }

    private void attackDetect(string name)
    {
        if (Input.GetButtonDown("Fire"+name) && !attack)
        {
            anim.SetTrigger("attack");
            attack = true;
            timer = attCD;
            trigger.enabled = true;
        }

        if (attack)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                attack = false;
                trigger.enabled = false;
            }

        }
    }

    
}
