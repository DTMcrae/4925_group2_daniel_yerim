using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);

        Progress[] progress = GameManager.Instance.GetSavedProgress();

        if (progress != null && progress.Length > 0)
        {
            foreach (var p in progress)
            {
                Debug.Log($"Level: {p.level}, High Score: {p.high_score}, Completed: {p.level_complete}");

                int levelNumber = int.Parse(p.level);

                if (levelNumber == 1)
                {
                    AddLevelCard(p, level1);
                }
                else if (levelNumber == 2)
                {
                    AddLevelCard(p, level2);
                }
                else if (levelNumber == 3)
                {
                    AddLevelCard(p, level3);
                }

            }
        }
        else
        {
            Debug.LogError("No progress data found!");
        }
    }

    private void AddLevelCard(Progress progress, GameObject levelCardContainer)
    {
        GameObject levelCard = Instantiate(levelCardContainer, canvas);

        levelCard.SetActive(true);

        // Assign data to the UI elements in the card
        TMP_Text levelText = levelCard.transform.Find("Level").GetComponent<TMP_Text>();
        TMP_Text scoreText = levelCard.transform.Find("Score").GetComponent<TMP_Text>();
        Button startButton = levelCard.transform.Find("StartButton").GetComponent<Button>();

        levelText.text = "Level " + progress.level;
        scoreText.text = "Score: " + progress.high_score;

        // Add button functionality
        startButton.onClick.AddListener(() => StartLevel(progress.level, progress.high_score));
    }

    private void StartLevel(string level, int score)
    {
        Debug.Log("Starting Level: " + level + " Score: " + score);
     

        int levelNumber = int.Parse(level);

        GameManager.Instance.StartLevel(levelNumber, score);
    }
}
