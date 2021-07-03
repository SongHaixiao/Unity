using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int AddHP = 50;

    public GameObject bullet;

    public float lifeTime = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // after generating, item falling down  
        this.transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
    }

    // use this tiem
    public void Use(Unit target)
    {
        target.AddHP(this.AddHP);   // add hp
        Destroy(this.gameObject);   // destroy this item
    }
}
