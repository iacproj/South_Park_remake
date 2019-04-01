using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanScript : MonoBehaviour
{

    public float timeStamp;
    bool hasStomped = false;
   public bool triggered = false;
    public MedKitSpawner mySpawner;
   

    public GameObject fireBlock;
    
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        myAnim = GetComponent<Animator>();
        


    }

    // Update is called once per frame
    void Update()
    {

        if (triggered == true)
        {
            if (hasStomped == false)
            {
                timeStamp = Time.time + 30;
                hasStomped = true;

            }





            if (Time.time >= timeStamp)
            {
                myAnim.SetTrigger("Stomp");
                hasStomped = false;
            }

            fireBlock.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggered = true;
            
        }
    }

    

    void Spawnz()
    {
        mySpawner.Spawn();
    }



}
