using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    public float maxHealth = 1000f;
    private List<Tonsil> tonsils = new List<Tonsil>();

    private int stage=1;
    private float currentHealth=0;
    public void AddTonsil(Tonsil tonsil)
    {
        tonsils.Add(tonsil);
    }

    public void RemoveTonsil(Tonsil tonsil)
    {
        tonsils.Remove(tonsil);
    }

    public void TakeDamage(string type)
    {
        if (type == "Egg")
        {
            currentHealth -= 10f;
            UpdateHealthBar();
        }
        else
        {
            currentHealth -= 50f;
            UpdateHealthBar();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarFill = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
    }

    public void UpdateHealthBar()
    {
        Debug.Log(currentHealth);
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;
        if (stage == 1 && fillAmount<=.875)
        {
            StageManager.Instance.ChangeStage(StageManager.BossStage.Stage2);
            stage++;
        } 
        else if(stage==2 && fillAmount <= .575)
        {
            StageManager.Instance.ChangeStage(StageManager.BossStage.Stage3);
            stage++;
        }
        else if (stage == 2 && fillAmount <= .285)
        {
            StageManager.Instance.ChangeStage(StageManager.BossStage.Stage4);
            stage++;
        }
    }

}
