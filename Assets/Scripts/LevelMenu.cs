using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject endlessButton;
    [SerializeField] private GameObject[] levelButtons;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Progress endless = null;
        bool unlockedEndless = false;

        foreach(GameObject levelButton in levelButtons)
            levelButton.SetActive(false);

        endlessButton.SetActive(false);

        Progress[] progress = GameManager.Instance.GetSavedProgress();

        if (progress != null && progress.Length > 0)
        {
            foreach (var p in progress)
            {
                Debug.Log($"Level: {p.level}, High Score: {p.high_score}, Completed: {p.level_complete}");

                try
                {
                    int levelNumber = int.Parse(p.level);
                    if (levelNumber > levelButtons.Length) continue;

                    AddLevelCard(p.level, p.high_score, levelButtons[levelNumber-1]);

                    if (levelNumber == levelButtons.Length && p.level_complete)
                        unlockedEndless = true;

                    if (p.level_complete && levelNumber < levelButtons.Length)
                    {
                        AddLevelCard((levelNumber + 1).ToString(), 0, levelButtons[levelNumber]);
                    }
                } 
                catch
                {
                    if(p.level == "endless")
                    {
                        endless = p;
                        unlockedEndless=true;
                    }
                }

            }
        }
        else
        {
            Debug.LogError("No progress data found!");
        }

        if (!levelButtons[0].activeSelf)
        {
            Debug.Log("Defaulting to level 1");
            AddLevelCard("1", 0, levelButtons[0]);
        }

        if (unlockedEndless)
        {
            int endlessScore = (endless != null) ? endless.high_score : 0;
            AddLevelCard("endless", endlessScore, endlessButton);
        }
    }

    private void AddLevelCard(string level, int score, GameObject levelCardContainer)
    {
        GameObject levelCard = levelCardContainer;
        Debug.Log("Enabling " + level);

        levelCard.SetActive(true);

        // Assign data to the UI elements in the card
        TMP_Text levelText = levelCard.transform.Find("Level").GetComponent<TMP_Text>();
        TMP_Text scoreText = levelCard.transform.Find("Score").GetComponent<TMP_Text>();
        Button startButton = levelCard.GetComponent<Button>();

        levelText.text = "Level " + level;
        scoreText.text = "High Score: " + score;

        // Add button functionality
        startButton.onClick.AddListener(() => StartLevel(level, score));
    }

    private void StartLevel(string level, int score)
    {
        Debug.Log("Starting Level: " + level + " Score: " + score);
     
        if(level == "endless")
        {
            GameManager.Instance.StartEndless();
            return;
        }

        int levelNumber = int.Parse(level);

        GameManager.Instance.StartLevel(levelNumber, 0);
    }
}
