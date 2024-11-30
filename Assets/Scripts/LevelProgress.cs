using System.Collections;

using UnityEngine;
using TMPro;

public class LevelProgress : MonoBehaviour
{
    [Header("References")]
    public TMP_Text text;
    public CatchableSpawner spawner;

    public static LevelProgress Instance;

    private void Awake()
    {
        Instance = this;

        Invoke(nameof(HideText), 3f);
    }

    public void LevelComplete(int level)
    {
        ShowText("LEVEL COMPLETE");
        spawner.enabled = false;
        StartCoroutine(RestartSpawningAfterDelay(3f));

        //Do something?
        //GameManager.Instance.HandleLevelComplete(level);
        switch (level)
        {
            case 2:
                spawner.SetSpawnerProperties(1.2f, 0.3f);
                break;
            case 3:
                spawner.SetSpawnerProperties(0.9f, 0.5f);
                break;
            default:
                break;
        }

    }

    private IEnumerator RestartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        HideText();
        spawner.enabled = true;
        //GameManager.Instance.ResumeGame();
    }

    //Plays Game Over Animation
    public void GameOver()
    {
        ShowText("GAME OVER");
        spawner.enabled = false;
        StartCoroutine(EndGame(3f));
    }

    public void ShowGameCompletionMessage()
    {
        ShowText("CONGRATULATIONS! GAME COMPLETED!");
        spawner.enabled = false;
    }

    //Actually Ends the Game
    private IEnumerator EndGame(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        HideText();

        //Either show a menu here, restart the level, or do something else?
    }

    private void ShowText(string display)
    {
        text.text = display;
        text.GetComponent<Animator>().SetBool("visible", true);
    }

    private void HideText()
    {
        text.GetComponent<Animator>().SetBool("visible", false);
    }
}