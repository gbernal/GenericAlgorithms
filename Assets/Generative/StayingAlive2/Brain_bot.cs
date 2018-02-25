﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_bot : MonoBehaviour {

    public int DNALength = 2;
    public float timeAlive;
    public float timeWalking;
    public DNA_bot dna;
    public GameObject eyes;
    //Vector3 startPosition;
    bool seeGround = true;
    bool alive = true;

    public GameObject ethanPrefab;
    GameObject ethan;

    private void OnDestroy()
    {
        Destroy(ethan);
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {

            alive = false;
        }
    }

    public void Init()
    {
        // initialize dna
        //0 move foward
        //1 move left
        //2 move right

        dna = new DNA_bot(DNALength, 3);
        timeAlive = 0;
        alive = true;
        ethan = Instantiate(ethanPrefab, this.transform.position, this.transform.rotation);
        ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = this.transform;
    }

	
	// Update is called once per frame
	void Update () {

        if (!alive) return;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10);
        seeGround = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit)) {

            if(hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        }

        timeAlive = populationManager_bot.elapsed;
        //read DNA
        float turn = 0;
        float move = 0;

        //make v relative to character and always move foward
        if (seeGround)
        {
            if (dna.GetGene(0) == 0) { move = 1; timeWalking += 1; }
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0) { move = 1; timeWalking += 1; }
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;
        }
        this.transform.Translate(0, 0, move * 0.1f);
        this.transform.Rotate(0, turn, 0);
	}
}
