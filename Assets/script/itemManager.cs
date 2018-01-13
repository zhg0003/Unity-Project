using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour {

    public GameObject pizza;
    private float spawnCD;
    public Transform spawnPos; 
	// Use this for initialization
	void Start ()
    {
        spawnCD = 3.0f;
        spawn();
        //can also use InvokeRepeating() here instead of using Update()
	}
	
	// Update is called once per frame
	//void Update ()
 //   {
 //       if (spawnCD < 0)
 //       {
 //           spawn();
 //           spawnCD = 3.0f;
 //       }
 //       else
 //           spawnCD -= Time.deltaTime;
	//}
    private void spawn()
    {
        print("spawn is called");
        //float pos = Random.Range(-5.0f, 5.0f);
        spawnPos.position = new Vector2(Random.Range(-5.0f, 5), Random.Range(0.5f, 3f));
        Instantiate(pizza, spawnPos.position, spawnPos.rotation);
        print("spawned at " + spawnPos.position);
    }
}
