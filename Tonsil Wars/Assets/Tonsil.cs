using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tonsil : MonoBehaviour
{

    private HealthBarController healthBarController;
    private float currentHealth;

    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    public float shootTimer = 0f;
    public float delayTimer = 0f;
    public Throat tonsilThroat;
    private enum Stage {Stage1,Stage2,Stage3,Stage4};
    private Stage stage = Stage.Stage1;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBarController = GameObject.FindWithTag("HealthBar").GetComponent<HealthBarController>();
        currentHealth = healthBarController.maxHealth;
        healthBarController.AddTonsil(this);
    }

    void Update()
    {
        switch (StageManager.Instance.currentStage)
        {
            case StageManager.BossStage.Stage1:
                shootIntervalSeconds = 5f;
                
                break;

            case StageManager.BossStage.Stage2:
                shootIntervalSeconds = 2.5f;
                break;

            case StageManager.BossStage.Stage3:
                shootIntervalSeconds = 1f;
                break;

            case StageManager.BossStage.Stage4:
                shootIntervalSeconds = 0.5f;
                break;

        }
        if (delayTimer >= shootDelaySeconds)
        {
            if (shootTimer >= shootIntervalSeconds)
            {
                Debug.Log("SHOT 1");
                tonsilThroat.Shoot();
                shootTimer = 0;
            }
            else
            {
                shootTimer += Time.deltaTime;
            }
        }
        else
        {
            delayTimer += Time.deltaTime;
        }

    }

    // Decrease the boss's health (call this method when the boss takes damage)
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, healthBarController.maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        healthBarController.UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Egg egg = collision.GetComponent<Egg>();
        if (egg != null)
        {
            if (!egg.isEnemy )
            {
                if (!egg.isSuper)
                {
                    Debug.Log("Hit!");
                    Destroy(egg.gameObject);
                    healthBarController.TakeDamage("Egg");
                }
                else
                {
                    Debug.Log("Super Hit!");
                    Destroy(egg.gameObject);
                    healthBarController.TakeDamage("Super");
                }
            }
        }
    }

    public void NextStage()
    {
        switch (stage)
        {
            case Stage.Stage1:
                stage = Stage.Stage2;

                break;

            case Stage.Stage2:
                stage= Stage.Stage3;
                break;

            case Stage.Stage3:
                stage = Stage.Stage4;
                break;
        }
    }

}
