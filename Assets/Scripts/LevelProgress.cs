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

    public void LevelComplete()
    {
        ShowText("LEVEL COMPLETE");
        spawner.enabled = false;

        //Do something?
    }

    //Plays Game Over Animation
    public void GameOver()
    {
        ShowText("GAME OVER");
        spawner.enabled = false;
        StartCoroutine(EndGame(3f));
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
