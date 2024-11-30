using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int maxLives = 3;
    [SerializeField] int maxLevel = 2;

    int lives = 0;
    int score = 0;
    int level = 1;

    public static PlayerStatus Instance;

    public int Lives => lives;
    public int Score => score;
    public int Level => level;

    private void Awake()
    {
        Instance = this;
        score = 0;
        lives = maxLives;
    }

    public void AdjustScore(int value)
    {
        score += value;

        if (score >= level * 200)
        {
            LevelComplete();
        }
    }

    public void AdjustLives(int value)
    {
        lives = Mathf.Min(lives + value, maxLives);

        if (lives <= 0)
            GameOver();
    }

    private void GameOver()
    {
        LevelProgress.Instance.GameOver();

        GetComponent<PlayerMovement>().enabled = false;
    }

    private void LevelComplete()
    {
        level++;
        LevelProgress.Instance.LevelComplete(level);
        //GameManager.Instance.LevelComplete(level);
        GetComponent<PlayerMovement>().enabled = false;
        ResetLevelStatus();
        GetComponent<PlayerMovement>().enabled = true;

    }

    private void ResetLevelStatus()
    {
        score = 0;
        lives = maxLives;
    }

    //private void GameCompletion()
    //{
    //    Debug.Log("Game Completed! Congratulations!");
    //    LevelProgress.Instance.ShowGameCompletionMessage();
    //    GetComponent<PlayerMovement>().enabled = false;
    //}
}
