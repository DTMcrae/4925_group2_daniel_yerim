using UnityEngine;

public class LoadRestart : MonoBehaviour
{
    public void LoadScene()
    {
        GameManager.Instance.StartLevel(1, 0);
    }
    
}
