using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSwaps : MonoBehaviour
{

    [SerializeField] Transform LakeSpawnPoint;
    [SerializeField] GameObject Player;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Animator myanim;

    [SerializeField] playerHealth playerHealth;
    [SerializeField] UIController UIController;
    [SerializeField] basicController basicController;

    private void HealthBarUpdate(float healthPrecentage)
    {
        UIController.HealthBar.fillAmount = healthPrecentage / 100;


    }
    private void HealthImageUpdate(float healthPrecentage)
    {

        if (healthPrecentage == 50)
        {
            UIController.healthImage.sprite = UIController.halfHp;
        }
        else if (healthPrecentage <= 20)
        {
            UIController.healthImage.sprite = UIController.noHp;
        }
        if (healthPrecentage > 50)
        {
            UIController.healthImage.sprite = UIController.fullHp;
        }
    }

    public void HealthUpdate(float healthPrecentage)
    {
        HealthImageUpdate(healthPrecentage);
        HealthBarUpdate(healthPrecentage);


    }

    public void Respawn()
    {
        Player.transform.position = SpawnPoint.position;
        playerHealth.pHealth = 300;
        UIController.HealthBar.fillAmount = 1;
        UIController.healthImage.sprite = UIController.fullHp;
        myanim.SetTrigger("Respawn");
        basicController.hasDied = false;
        playerHealth.isDead = false;

        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SpawnPoint")
        {
            SpawnPoint = LakeSpawnPoint;
        }
    }
    


}
