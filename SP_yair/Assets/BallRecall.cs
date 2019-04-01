using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRecall : MonoBehaviour
{
    [SerializeField] GameObject BowlingBall;


   public void RecallBall()
    {
        BowlingBall.SetActive(true);
    }
}
