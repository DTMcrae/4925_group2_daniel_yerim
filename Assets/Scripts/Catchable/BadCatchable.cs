using UnityEngine;

public class BadCatchable : ICatchable
{
    public override void OnCatch()
    {
        Debug.Log("You caught a bad thing... oh no");
    }

    public override void OnMiss()
    {
        Debug.Log("You missed a bad thing! Yay!");
    }
}
