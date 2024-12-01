using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{

    TMP_Text flashingText;
    string textToBlink;
    public float BlinkTime;

    void Start()
    {
        flashingText = GetComponent<TMP_Text>();
        textToBlink = flashingText.text;
        BlinkTime = 0.5f;
        StartCoroutine(BlinkText());
    }




    IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = textToBlink;
            yield return new WaitForSeconds(BlinkTime);
            flashingText.text = string.Empty;
            yield return new WaitForSeconds(BlinkTime);
        }
    }

    //public float minTime = 0.05f;
    //public float maxTime = 1.2f;

    //private float timer;
    //private Text textFlicker;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    textFlicker = GetComponent<Text>();
    //    timer = Random.Range(minTime, maxTime);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    timer = Time.deltaTime;
    //    if (timer <= 0)
    //    {
    //        textFlicker.enabled = !textFlicker.enabled;
    //        timer = Random.Range(minTime, maxTime);
    //    }
    //}
}
