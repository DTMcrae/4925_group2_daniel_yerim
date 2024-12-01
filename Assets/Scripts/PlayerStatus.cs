using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int maxLives = 3;
    [SerializeField] int maxLevel = 4;

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
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        score = 0;
        lives = maxLives;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void AdjustScore(int value)
    {
        score += value;

        if (score >= level * 200 && level > 0)
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

    public void SetLevelAndScore(int newLevel, int newScore)
    {

        level = newLevel;
        score = newScore;

        LevelProgress.Instance.SetLevel(level);

        lives = maxLives;
    }

    private void GameOver()
    {
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.death);

        LevelProgress.Instance.GameOver(level, score);

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

            LevelProgress.Instance.LevelComplete(level-1, score);
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
        LevelProgress.Instance.ShowGameCompletionMessage(Level, Score);
        GetComponent<PlayerMovement>().enabled = false;
        GameManager.Instance.GameOver();
    }
}
