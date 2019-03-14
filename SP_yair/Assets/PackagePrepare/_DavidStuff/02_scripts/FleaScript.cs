using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaScript : MonoBehaviour
{


    Collider myTrig;
    // Start is called before the first frame update
    void Start()
    {
        myTrig = GetComponent<Collider>();
        myTrig.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Activate()
    {
        myTrig.enabled = true;
        Invoke("Disactivate", 2);
        Debug.Log("Activated");
    }


    public void Disactivate()
    {
        myTrig.enabled = false;
        Debug.Log("Disactivated");
    }
}
