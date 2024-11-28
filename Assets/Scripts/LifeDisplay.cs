using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [Header("References")]
    [SerializeField] PlayerStatus status;

    Image[] hearts;
    int lastLiveCount = 0;

    private void Start()
    {
        hearts = GetComponentsInChildren<Image>();

        if (hearts.Length == 0 || status == null)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if(status.Lives != lastLiveCount)
        {
            lastLiveCount = status.Lives;

            for(int index = 0; index < hearts.Length; index++)
            {
                if (index + 1 <= lastLiveCount)
                {
                    hearts[index].sprite = fullHeart;
                }
                else
                    hearts[index].sprite = emptyHeart;
            }
        }
    }
}
