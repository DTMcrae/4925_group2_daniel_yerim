using UnityEngine;

public class LoadRestart : MonoBehaviour
{
    public void LoadScene()
    {
        GameManager.Instance.StartLevel(GameManager.Instance.LastLevel, 0);
    }
    
}
