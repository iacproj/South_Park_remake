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
    [SerializeField] float JointPercentage = 100f;

    [SerializeField] Image JointCooldown;
    [SerializeField] basicController basicController;

    

    private void Start()
    {
        healthImage.sprite = fullHp;

    }

    private void Update()
    {


        FillSkillBar();
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
            SkillBar.fillAmount = (SkillPercentage++ / 100);
        }
        else
        {
            SkillPercentage = SkillPercentage;
        }
    }

    public void JointAttack()
    {
        SkillPercentage = SkillPercentage - 55;
        

        
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
        if(healthPrecentage > 50)
        {
            healthImage.sprite = fullHp;
        }
    }

    public void UpdateSkillUI(float jointFillAmount) 
    {
        JointCooldown.fillAmount = jointFillAmount;
    }
}
