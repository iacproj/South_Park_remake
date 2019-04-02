using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPrompts : MonoBehaviour
{
    [SerializeField] GameObject RelatedPrompt;
    [SerializeField] basicController basicController;

    private void Awake()
    {
        RelatedPrompt.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RelatedPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RelatedPrompt.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(basicController.hasBall == true)
        {
            Destroy(gameObject);
        }
    }
}
