using UnityEngine;

public class CatchableSpawner : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxObjects = 50;
    [SerializeField] float startDelay = 3f;
    [SerializeField] float spawnDelay = 1.5f;
    [SerializeField] float badObjectChance = 0.2f;
    [SerializeField] float minX = -15f;
    [SerializeField] float maxX = 15f;

    [Header("References")]
    [SerializeField] Transform spawnPosition;
    [SerializeField] ICatchable[] GoodCatchableObjects;
    [SerializeField] ICatchable[] BadCatchableObjects;

    float timer = 0;
    int objectCounter = 0;

    private void Awake()
    {
        timer -= startDelay;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnDelay)
        {
            timer = 0f;

            if (Random.Range(0f, 1f) <= badObjectChance)
            {
                SpawnBadCatchable();
            }
            else SpawnGoodCatchable();

            objectCounter++;

            if(objectCounter >= maxObjects)
            {
                Debug.Log("Max Objects Reached");
                enabled = false;
            }
        }
    }

    private void SpawnGoodCatchable()
    {
        GameObject newCatchable = Instantiate(GoodCatchableObjects[Random.Range(0, GoodCatchableObjects.Length)].gameObject, spawnPosition);
        newCatchable.transform.position = new Vector3(Random.Range(minX, maxX), newCatchable.transform.position.y);
    }

    private void SpawnBadCatchable()
    {
        GameObject newCatchable = Instantiate(BadCatchableObjects[Random.Range(0, BadCatchableObjects.Length)].gameObject, spawnPosition);
        newCatchable.transform.position = new Vector3(Random.Range(minX, maxX), newCatchable.transform.position.y);
    }

    public void SetSpawnerProperties(float newSpawnDelay, float newBadObjectChance)
    {
        spawnDelay = newSpawnDelay;
        badObjectChance = newBadObjectChance;
        Debug.Log($"Spawner updated: SpawnDelay = {spawnDelay}, BadObjectChance = {badObjectChance}");
    }

}
