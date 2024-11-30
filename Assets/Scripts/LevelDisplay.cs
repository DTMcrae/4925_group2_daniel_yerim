using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text display;
    [SerializeField] PlayerStatus status;

    private void Awake()
    {
        if (display == null || status == null)
            enabled = false;
    }

    private void Update()
    {
        display.text = status.Level.ToString();
    }
}
