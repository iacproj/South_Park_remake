using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeScript : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody myRig;
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        //myRig.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            myRig.mass = 1;
        }
    }
}
