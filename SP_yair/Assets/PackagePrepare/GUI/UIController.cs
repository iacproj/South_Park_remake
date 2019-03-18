using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [SerializeField] Image healthImage;
    [SerializeField] Sprite fullHp;
    [SerializeField] Sprite halfHp;
    [SerializeField] Sprite noHp;
    [SerializeField] Image HealthBar;
    [SerializeField] Image SkillBar;
    [SerializeField] float SkillPercentage;

    private void Start()
    {
        healthImage.sprite = fullHp;

    }

    private void Update()
    {



        SkillBar.fillAmount = (SkillPercentage++ / 200);
    }

    public void HealthUpdate(float healthPrecentage)
    {
        HealthImageUpdate(healthPrecentage);
        HealthBarUpdate(healthPrecentage);


    }

    private void HealthBarUpdate(float healthPrecentage)
    {
        HealthBar.fillAmount = healthPrecentage / 100;


    }

    void FillSkillBar()
    {
        if(SkillPercentage <= 100)
        {
            SkillBar.fillAmount = (SkillPercentage++ / 200);
        }
        else
        {
            SkillPercentage = SkillPercentage;
        }
    }

    private void HealthImageUpdate(float healthPrecentage)
    {
        
         if (healthPrecentage == 50)
        {
            healthImage.sprite = halfHp;
        }
        else if (healthPrecentage <= 20)
        {
            healthImage.sprite = noHp;
        }
    }

    
}
