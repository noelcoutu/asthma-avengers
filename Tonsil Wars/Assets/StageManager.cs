using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    public BossStage currentStage;

    public enum BossStage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeStage(BossStage newStage)
    {
        currentStage = newStage;
        // Implement logic to handle the stage change (e.g., boss behavior changes).
    }
}