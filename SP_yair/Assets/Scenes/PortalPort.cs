using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPort : MonoBehaviour
{

    [SerializeField] Transform PortalPosition;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = PortalPosition.position;
            other.transform.rotation = PortalPosition.rotation;
        }
    }

}
