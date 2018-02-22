﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {
    //gene for color
    public float r;
    public float g;
    public float b;

    bool dead = false;
    public float timeToDie = 0;
    SpriteRenderer sRenderer;
    Collider2D sCollider;

    void OnMouseDown()
	{
    	dead = false;
    	timeToDie = PopulationManager.elapased;
    	Debug.Log("Dead at: " + timeToDie);
    	sRenderer.enabled = false;
    	sCollider.enabled = false;
    }
		
	// Use this for initialization
	void Start ()
	{
		sRenderer = GetComponent<SpriteRenderer>();
		sCollider = GetComponent<Collider2D>();
		sRenderer.color = new Color(r,g,b);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
