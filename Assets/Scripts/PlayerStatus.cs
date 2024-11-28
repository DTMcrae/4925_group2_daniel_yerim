using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int maxLives = 3;

    int lives = 0;
    int score = 0;

    public static PlayerStatus Instance;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        Instance = this;
        score = 0;
        lives = maxLives;
    }

    public void AdjustScore(int value)
    {
        score += value;
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
}
