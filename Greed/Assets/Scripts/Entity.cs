﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public float health = 5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
