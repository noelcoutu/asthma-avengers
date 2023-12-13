using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DuckChargeBar : MonoBehaviour
{
    private float currentChargeTime = 0f;
    public float chargeDuration = 1.0f;
    public Image chargeBarFill;
    public GameObject chargeBarContainer;
    // Start is called before the first frame update
    void Start()
    {
        chargeBarFill = GameObject.FindWithTag("ChargeBar").GetComponent<Image>();
        chargeBarContainer = GameObject.FindWithTag("ChargeBarContainer");
    }

    // Update is called once per frame
    void Update()
    {
        // Your charging logic here
        if (Input.GetButton("Fire1"))
        {
            Color red = new Color(255, 0, 0, 255);
            chargeBarFill.color = red;
            currentChargeTime += Time.deltaTime;
            float chargePercentage = Mathf.Clamp01(currentChargeTime / chargeDuration);
            if (chargePercentage > .10)
            {
                chargeBarContainer.SetActive(true);
                chargeBarFill.fillAmount = chargePercentage;
            }
        }
        else
        {
            currentChargeTime = 0f;
            chargeBarFill.fillAmount = 0f;
            chargeBarContainer.SetActive(false);
        }

        // Handle charging completion here
        if (chargeBarFill.fillAmount == 1f)
        {
            Color green = new Color(0, 256, 0);
            chargeBarFill.color = green;
            // Call your superShoot() function or handle charging completion as needed
            // Example: superShoot();
        }
    }
}
