using UnityEngine;

public class GoodCatchable : ICatchable
{
    [SerializeField] int scoreOnCatch = 20;

    public override void OnCatch()
    {
        Debug.Log("You caught a good thing! Yay!");
        if (PlayerStatus.Instance)
            PlayerStatus.Instance.AdjustScore(scoreOnCatch);
    }

    public override void OnMiss()
    {
        Debug.Log("You missed a good thing... why...");
    }
}
