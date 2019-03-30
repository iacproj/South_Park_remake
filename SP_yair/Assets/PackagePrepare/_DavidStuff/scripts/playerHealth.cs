using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float pHealth;
    private float maxHp;
    public bool isDead;
    [SerializeField] UIController UISCript;

    public FleaScript myFlea;
    // Start is called before the first frame update
    void Start()
    {
        //pHealth = 100;
        isDead = false;
        maxHp = pHealth;



    }

    // Update is called once per frame
    void Update()
    {
        if (pHealth <= 0 && isDead == false)
        {
            isDead = true;
        }
    }




   void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MBP_puncher")
        {
            pHealth -= 10;
            myFlea.Activate();
            UISCript.HealthUpdate((pHealth / maxHp) * 100);
        }

        if (col.gameObject.tag == "Bat_hitter")
        {
            pHealth -= 10;
            myFlea.Activate();
            UISCript.HealthUpdate((pHealth / maxHp) * 100);
        }

        if (col.gameObject.tag == "Bullet")
        {
            pHealth -= 10;
            myFlea.Activate();
            UISCript.HealthUpdate((pHealth / maxHp) * 100);
        }

        if (col.gameObject.tag == "Cereal")
        {
            if (pHealth <= 200)
            {
                pHealth += 100;
                UISCript.HealthUpdate((pHealth / maxHp) * 100);
            }

            else
            {
                pHealth = pHealth + (300 - pHealth);
                UISCript.HealthUpdate((pHealth / maxHp) * 100);
            }
            Destroy(col.gameObject);
            
        }



    }

   
}
