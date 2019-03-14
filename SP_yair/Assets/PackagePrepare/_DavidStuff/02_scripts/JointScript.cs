using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour
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

  public  void TurnOn()
    {
        myTrig.enabled = true;
    }

  public void TurnOff()
    {
        myTrig.enabled = false;
    }
}
