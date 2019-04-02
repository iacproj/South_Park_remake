using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject EndPic;

    private void Awake()
    {
        EndPic.SetActive(false);
    }

    public void ItsDone()
    {
        EndPic.SetActive(true);
    }
}
