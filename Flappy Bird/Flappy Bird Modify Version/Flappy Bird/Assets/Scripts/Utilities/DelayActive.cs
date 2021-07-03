﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour
{
    // Script Description : make the object delay to do something

    public float dealy = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(Delay());	
	}
	
	IEnumerator Delay()
    {
        yield return new WaitForSeconds(dealy);
        this.gameObject.SetActive(false);
    }
}
