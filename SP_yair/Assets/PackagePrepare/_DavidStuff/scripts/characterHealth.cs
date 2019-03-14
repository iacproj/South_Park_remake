﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterHealth : MonoBehaviour
{

    public enum characterTypes { NPC, Enemy, MBP };
    public characterTypes healthTypes;

    public float charHealth;
    public bool isDead = false;

    float enemyHealth = 100;
    float MBPHealth = 400;
    float NPCHealth = 100;

    public FleaScript myFlea;
    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.tag == "NPC")
        {
            charHealth = NPCHealth;
        }

        else if (gameObject.tag == "Enemy")
        {
            charHealth = enemyHealth;
        }

        else
        {
            charHealth = MBPHealth;
        }


        isDead = false;

        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (charHealth <= 0)
        {
            isDead = true;
        }
    }


    void WoodenSwordDamage()
    {
        charHealth -= 50;
        myFlea.Activate();

    }

    void BallDamage()
    {
        charHealth -= 100;
        myFlea.Activate();
    }


   void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Woody")
        {
           
            WoodenSwordDamage();
            Debug.Log("Triggered");


        }

        if (col.gameObject.tag == "Ball")

           
        {
            BallDamage();
        }

      




    }

   
}
