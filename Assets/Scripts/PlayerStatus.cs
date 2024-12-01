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

    AudioManager audioManager;

    private void Awake()
    {
        Instance = this;
        score = 0;
        lives = maxLives;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.death);

        LevelProgress.Instance.GameOver();

        GetComponent<PlayerMovement>().enabled = false;
        GameManager.Instance.GameOver();
    }

    private void LevelComplete()
    {
        level++;

        if (level == maxLevel)
        {
            GameCompletion();
        } else
        {
            audioManager.PlaySFX(audioManager.levelComplete);

            LevelProgress.Instance.LevelComplete(level);
            GetComponent<PlayerMovement>().enabled = false;
            ResetLevelStatus();
            GetComponent<PlayerMovement>().enabled = true;
        }  

    }

    private void ResetLevelStatus()
    {
        score = 0;
        lives = maxLives;
    }

    private void GameCompletion()
    {
        audioManager.StopMusic();
        Debug.Log("Game Completed! Congratulations!");
        LevelProgress.Instance.ShowGameCompletionMessage();
        GetComponent<PlayerMovement>().enabled = false;
        GameManager.Instance.GameOver();
    }
}
