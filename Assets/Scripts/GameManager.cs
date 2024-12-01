using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
     *  To access the GameManager's info or functions, use GameManager.Instance.value and replace value with what you need
     */
    public static GameManager Instance;
    public CatchableSpawner spawner;
    public Animator loadingAnimator;

    private string username = "";
    private int userID = -1;
    private Progress[] savedProgress;

    private int storedLevel = 1;
    private int storedScore = 0;

    public string Username => username;
    public int UserID => userID;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetUserDetails(string username, int id)
    {
        this.userID = id;
        this.username = username;

        StartCoroutine(LoadProgress());
    }

    public void StartGame()
    {
        //Temporary Function, will be used once the game scene is loaded and ready to play
        //StartCoroutine(LoadAfterWait("New Scene", 3.0f));
        //SceneManager.LoadScene("GameScene");
        LoadScene(1);
    }

    public void StartLevel(int level, int score)
    {
        storedLevel = level;
        storedScore = score;

        SceneManager.sceneLoaded += OnGameSceneLoaded;

        LoadScene(1);
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            SceneManager.sceneLoaded -= OnGameSceneLoaded;
            Invoke(nameof(InitializeLevel), 0.5f);
        }
    }

    private void InitializeLevel()
    {
        PlayerStatus.Instance.SetLevelAndScore(storedLevel, storedScore);

        // Restore spawner state or other level-specific elements
        if (spawner != null)
        {
            RestoreSpawnerState(storedLevel);
        }
    }

    private void RestoreSpawnerState(int level)
    {
        switch (level)
        {
            case 2:
                spawner.SetSpawnerProperties(1.2f, 0.3f);
                break;
            case 3:
                spawner.SetSpawnerProperties(0.9f, 0.5f);
                break;
            default:
                spawner.SetSpawnerProperties(1.0f, 0.5f);
                break;
        }
        Debug.Log($"Spawner state restored for level {level}");
    }

    IEnumerator LoadAfterWait(int scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(LoadProgress());
        LoadScene(scene);
    }

    public void GameOver()
    {
        //Handle post-game failure logic
        StartCoroutine(LoadAfterWait(2, 3.0f));
    }

    AsyncOperation loadingOperation;

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(int scene)
    {
        Debug.Log("Loading Scene...");
        loadingOperation = null;
        loadingAnimator.SetBool("visible", true);

        yield return new WaitForSeconds(2f);
        Debug.Log("Delay passed.");

        loadingOperation = SceneManager.LoadSceneAsync(scene);

        while(!loadingOperation.isDone)
        {
            Debug.Log("Waiting for load to complete...");
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Complete");

        loadingAnimator.SetBool("visible", false);
        //loadingOperation = null;
    }

    public Progress[] GetSavedProgress()
    {
        return savedProgress;
    }

    public IEnumerator LoadProgress()
    {
        if(userID == -1)
        {
            Debug.LogError("User not logged in.");
            yield break;
        }

        string url = API.Base + "progress/loadprogress";
        WWWForm formData = new WWWForm();
        formData.AddField("userId", userID);

        UnityWebRequest request = UnityWebRequest.Post(url, formData);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                ProgressResponse progressResponse = JsonUtility.FromJson<ProgressResponse>(request.downloadHandler.text);

                Debug.Log("Response: " + progressResponse.ToString());
                savedProgress = progressResponse.progress;

                foreach (var progress in progressResponse.progress)
                {
                    Debug.Log($"Level: {progress.level}, High Score: {progress.high_score}, Completed: {progress.level_complete}");
                }
            }
            catch
            {
                Debug.LogError("Error parsing response");
            }
        }
        else
        {
            Debug.Log("Unsuccessful request");
        }
    }

    public IEnumerator UpdateProgress(string level, int score, bool is_completed)
    {
        if (userID == -1)
        {
            Debug.LogError("User not logged in.");
            yield break;
        }

        bool completionState = is_completed;

        foreach(Progress p in savedProgress)
        {
            if(p.level.Equals(level))
            {
                completionState = (p.level_complete || is_completed);
            }
        }

        string url = API.Base + "progress/saveprogress";
        WWWForm formData = new WWWForm();

        formData.AddField("userId", userID);
        formData.AddField("level", level);
        formData.AddField("high_score", score);
        formData.AddField("level_complete", completionState.ToString());

        UnityWebRequest request = UnityWebRequest.Post(url, formData);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Progress saved!");
        }
        else
        {
            Debug.Log("Error occured while saving progress");
        }
    }

}

[Serializable]
public class Progress
{
    public string level;
    public int high_score;
    public bool level_complete;
}

[Serializable]
public class ProgressResponse
{
    public Progress[] progress;
}
