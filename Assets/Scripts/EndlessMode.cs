using UnityEngine;

public class EndlessMode : MonoBehaviour
{
    [SerializeField] CatchableSpawner spawner;
    [SerializeField] int increaseThreshold = 250;
    [SerializeField] float spawnDelayScalar = 0.05f;
    [SerializeField] float spawnChanceScalar = 0.025f;

    int difficulty = 1;

    void Start()
    {
        PlayerStatus.Instance.SetLevelAndScore(-1, 0);
    }

    private void Update()
    {
        if(PlayerStatus.Instance.Score >= (difficulty * increaseThreshold))
        {
            difficulty++;
            spawner.SetSpawnerProperties(Mathf.Max(0.2f, 1.5f - (difficulty * spawnDelayScalar)), Mathf.Min(0.85f, 0.1f + (spawnChanceScalar * difficulty)));
        }
    }
}
