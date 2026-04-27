using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public GameObject prefab;
        public float weight = 1f;
    }

    [System.Serializable]
    public class Level
    {
        public float spawnInterval = 2f;
        public Enemy[] enemies;
    }

    [Header("Levels")]
    [SerializeField] private Level[] levels;

    [Header("Progression")]
    [SerializeField] private int level = 0;
    [SerializeField] private float levelDuration = 10f;

    private float spawnCounter;
    private float levelTimer;

    void Start()
    {
        spawnCounter = levels[level].spawnInterval;
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        spawnCounter -= Time.deltaTime;

        if (levelTimer >= levelDuration && level < levels.Length - 1)
        {
            level++;
            levelTimer = 0f;
        }

        if (spawnCounter <= 0f)
        {
            SpawnEnemy();
            spawnCounter = levels[level].spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = PickWeightedEnemy(levels[level].enemies);
        Instantiate(enemy,GameManager.main.startPoint.position,Quaternion.identity);
    }

    private GameObject PickWeightedEnemy(Enemy[] enemies)
    {
        float totalWeight = 0f;

        foreach (Enemy enemy in enemies)
        {
            totalWeight += enemy.weight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        foreach (Enemy enemy in enemies)
        {
            if (randomValue < enemy.weight)
            {
                return enemy.prefab;
            }

            randomValue -= enemy.weight;
        }

        return enemies[enemies.Length - 1].prefab;
    }
}