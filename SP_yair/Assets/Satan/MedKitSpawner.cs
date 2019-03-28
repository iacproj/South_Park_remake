using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitSpawner : MonoBehaviour
{

    public GameObject medKit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Instantiate(medKit, gameObject.transform.position, gameObject.transform.rotation);
        
    }
}
