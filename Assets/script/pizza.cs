using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza : MonoBehaviour {

    private float spawnCD;
    private Collider2D box;
	// Use this for initialization

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("something touched this pizza");
            other.SendMessageUpwards("heal", 15);
            Destroy(gameObject);
        }
    }
}
