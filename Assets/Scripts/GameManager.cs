using System.Collections;

using UnityEngine;
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

}
