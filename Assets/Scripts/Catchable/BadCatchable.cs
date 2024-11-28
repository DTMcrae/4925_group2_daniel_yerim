using UnityEngine;

public class BadCatchable : ICatchable
{
    [SerializeField] int scoreOnMiss = 10;
    [SerializeField] int livesLostOnCatch = -1;

    public override void OnCatch()
    {
        Debug.Log("You caught a bad thing... oh no");
        if (PlayerStatus.Instance)
            PlayerStatus.Instance.AdjustLives(livesLostOnCatch);
    }

    public override void OnMiss()
    {
        Debug.Log("You missed a bad thing! Yay!");
        if(PlayerStatus.Instance)
            PlayerStatus.Instance.AdjustScore(scoreOnMiss);
    }
}
