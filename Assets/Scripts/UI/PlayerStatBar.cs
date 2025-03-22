using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image health;
    public Image healthImage;
    public Image healthDelayImage;
    public Text currentHealthText;
    public Text maxHealthText;

    public Image MPImage;
    public Image MPDelayImage;
    public Text currentMPText;
    public Text maxMPText;

    public Image powerImage;
    public Image border;
    public Animator breakAnimation;

    private void Update()
    {


        if(healthImage.fillAmount < healthDelayImage.fillAmount)
        {
            healthDelayImage.fillAmount = Mathf.Lerp(healthDelayImage.fillAmount, healthImage.fillAmount, 4f * Time.deltaTime);
        }
        else
        {
            healthDelayImage.fillAmount = healthImage.fillAmount;
        }

        if(health.fillAmount != healthImage.fillAmount)
        {
            healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, health.fillAmount, 30f * Time.deltaTime);
        }
        else
        {
            healthImage.fillAmount = health.fillAmount;
        }

        if (healthImage.fillAmount <=0 )
        {
            breakAnimation.SetBool("Break", true);
        }
        else
        {
            breakAnimation.SetBool("Break", false);
        }

        if(MPImage.fillAmount < MPDelayImage.fillAmount)
        {
           MPDelayImage.fillAmount = Mathf.Lerp(MPDelayImage.fillAmount, MPImage.fillAmount, 4f * Time.deltaTime);
        }
        else
        {
            MPDelayImage.fillAmount = MPImage.fillAmount;
        }
        
    }

    public void OnHealthChange(float persentage)
    {
        health.fillAmount = persentage;
    }


    public void OnMPChange(float persentage)
    {
        MPImage.fillAmount = persentage;
    }

    public void HealthText(int currentHealth,int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    public void MPText(int currentMP,int maxMP)
    {
        currentMPText.text = currentMP.ToString();
        maxMPText.text = maxMP.ToString();
    }
}
