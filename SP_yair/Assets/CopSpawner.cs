using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour
{
    public int i = 0;
    public GameObject[] CopArray;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

  public void SpawnCop()
    {
        if (i <= 5)
        {
            CopArray[i].SetActive(true);
            i++;
            Debug.Log("dingdong");
        }
    }
}
