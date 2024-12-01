using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelMenu : MonoBehaviour
{
    public void LoadScene()
    {
        //SceneManager.LoadScene("LevelMenu");
        GameManager.Instance.LoadScene(3);
    }
}
