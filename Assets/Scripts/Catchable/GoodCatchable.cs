using UnityEngine;

public class GoodCatchable : ICatchable
{
    [SerializeField] int scoreOnCatch = 20;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public override void OnCatch()
    {
        audioManager.PlaySFX(audioManager.goodCatch);
        Debug.Log("You caught a good thing! Yay!");
        if (PlayerStatus.Instance)
            PlayerStatus.Instance.AdjustScore(scoreOnCatch);
    }

    public override void OnMiss()
    {
        Debug.Log("You missed a good thing... why...");
    }
}
