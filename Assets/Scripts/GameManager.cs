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

    private string username = "";
    private int userID = -1;
    private Progress[] savedProgress;

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
        //SceneManager.LoadScene("New Scene");
        //StartCoroutine(LoadAfterWait("New Scene", 3.0f));
        SceneManager.LoadScene("GameScene");
    }

    //IEnumerator LoadAfterWait(string scene, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene(scene);
    //}

    public void GameOver()
    {
        //Handle post-game failure logic
    }

    //public void LevelComplete(int level)
    //{
    //    LevelProgress.Instance.LevelComplete(level);
    //}

    //public void HandleLevelComplete(int level)
    //{
    //    spawner.enabled = false;

    //    switch (level)
    //    {
    //        case 2:
    //            spawner.SetSpawnerProperties(1.2f, 0.3f);
    //            break;
    //        case 3:
    //            spawner.SetSpawnerProperties(0.9f, 0.5f);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public void ResumeGame()
    //{
    //    spawner.enabled = true;
    //}

    public void LoadScene(int scene)
    {
        //Handles what to do when a scene is loaded
    }

    public IEnumerator LoadProgress()
    {
        if(userID == -1)
        {
            Debug.Log("User not logged in.");
            yield break;
        }

        string url = API.Base + "progress/loadprogress";
        WWWForm formData = new WWWForm();
        Debug.Log("Url: (" + url + ")");
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
