using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code ref: https://www.youtube.com/watch?v=xwPahXLpNh8
public class attacktrigger : MonoBehaviour {

    public int dmg = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("att triggered "+other.gameObject.name+" istrigger "+other.isTrigger);
        if(!other.isTrigger && other.tag == ("Player"))
        {
            print("about to call other object dmg");
            other.SendMessageUpwards("damage", dmg);
        }
    }
}
