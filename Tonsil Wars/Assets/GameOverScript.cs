using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    private Button restart;
    private TextMeshProUGUI score;
    private string scoreText;
    public TextMeshProUGUI scoreEnd;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText = score.text;
        scoreEnd.text = "Score: "+scoreText;
        restart = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            OnRestartClick();
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StageManager.Instance.ChangeStage(StageManager.BossStage.Stage1);
    }
}
