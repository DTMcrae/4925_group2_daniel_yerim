using UnityEngine;

public class GoodCatchable : ICatchable
{
    public override void OnCatch()
    {
        Debug.Log("You caught a good thing! Yay!");
    }

    public override void OnMiss()
    {
        Debug.Log("You missed a good thing... why...");
    }
}
