using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealthController : MonoBehaviour
{
    public int currentHearts = 3; // Number of hearts currently displayed
    public int maxHearts = 3; // Maximum number of hearts (can be increased or decreased)
    private GameObject[] hearts;
    private GameObject gameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOverScreen = GameObject.FindGameObjectWithTag("Finish");
        gameOverScreen.SetActive(false);
        hearts = GameObject.FindGameObjectsWithTag("PlayerHealth");
    }

    private void Update()
    {
        if (currentHearts == 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
     
    }

    public void GainHealth()
    {
        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (!hearts[i].activeInHierarchy)
            {
                hearts[i].SetActive(true);
                currentHearts++;
                return;
            }
        }
    }

    public void RemoveHealth()
    {
        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (hearts[i].activeInHierarchy)
            {
                hearts[i].SetActive(false);
                currentHearts--;
                Debug.Log(currentHearts);
                return;
            }
        }
    }
}
