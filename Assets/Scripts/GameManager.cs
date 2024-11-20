using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     *  To access the GameManager's info or functions, use GameManager.Instance.value and replace value with what you need
     */
    public static GameManager Instance;

    private string username = "";
    private int userID = -1;

    public string Username => username;
    public int UserID => userID;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
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
    }

    public void GameOver()
    {
        //Handle post-game logic
    }

    public void LoadScene(int scene)
    {
        //Handles what to do when a scene is loaded
    }

}
