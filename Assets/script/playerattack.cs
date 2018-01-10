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
    void Update() {
        if (Input.GetKeyDown("f") && !attack)
        {
            anim.SetBool("attack", true);
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
