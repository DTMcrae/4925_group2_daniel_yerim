using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

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

    public void LevelComplete(int level, int score)
    {
        ShowText("LEVEL COMPLETE");
        spawner.enabled = false;
        StartCoroutine(RestartSpawningAfterDelay(3f));

        StartCoroutine(GameManager.Instance.UpdateProgress(level.ToString(), score, true));

        SetLevel(level);

    }

    public void SetLevel(int level)
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
                break;
        }
    }

    private IEnumerator RestartSpawningAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        HideText();
        spawner.enabled = true;
    }

    //Plays Game Over Animation
    public void GameOver(int level, int score)
    {
        ShowText("GAME OVER");
        spawner.enabled = false;
        StartCoroutine(GameManager.Instance.UpdateProgress(level.ToString(), score, false));
        StartCoroutine(EndGame(3f));
    }

    public void ShowGameCompletionMessage(int level, int score)
    {
        ShowText("CONGRATULATIONS! GAME COMPLETED!\n\nEndless Mode Unlocked!");
        StartCoroutine(GameManager.Instance.UpdateProgress(level.ToString(), score, true));
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
