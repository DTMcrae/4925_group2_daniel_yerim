using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Progress[] progress = GameManager.Instance.GetSavedProgress();

        if(progress != null && progress.Length > 0)
        {
            foreach (var p in progress)
            {
                Debug.Log($"Level: {p.level}, High Score: {p.high_score}, Completed: {p.level_complete}");
            }
        }
        else
        {
            Debug.LogError("No progress data found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
