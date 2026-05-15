using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner main;

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
        public float spawnDuration = 10f;
        public Enemy[] enemies;

        public AudioClip music;
    }

    [Header("Levels")]
    [SerializeField] private Level[] levels;

    [Header("UI")]
    [SerializeField] private GameObject levelTextPanel;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private float breakDuration = 3f;
    public TextMeshProUGUI remainingEnemiesText;

    [SerializeField] private int level = 0;
    private float spawnCounter;
    private bool isSpawning = false;

    private List<GameObject> currentSpawnList = new List<GameObject>();
    private int spawnIndex;

    public AudioClip changeLevelSFX;
    public GameObject winScreen;
    public int remainingEnemies = 0;

    void Awake()//TODO: Addition.
    {
        main = this;
    }

    void Start()
    {
        //spawnCounter = levels[level].spawnInterval;
        StartCoroutine(LevelLoop());
    }


    IEnumerator LevelLoop()//TODO: Addition.
    {
        while (level < levels.Length)
        {
            if (levels[level].music != null)
                PlayMusic(levels[level].music);

            SoundManager.instance.PlayImportantSFX(changeLevelSFX);
            yield return StartCoroutine(ShowLevelText("Level " + (level + 1)));

            currentSpawnList = GenerateEnemyPool(levels[level]);
            spawnIndex = 0;
            remainingEnemies = currentSpawnList.Count;
            UpdateRemainingEnemiesText();


            isSpawning = true;
            spawnCounter = levels[level].spawnInterval;

            float timer = 0f;

            while (timer < levels[level].spawnDuration)
            {
                timer += Time.deltaTime;

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0f)
                {
                    SpawnEnemyFromPool();
                    spawnCounter = levels[level].spawnInterval;
                }

                yield return null;
            }

            isSpawning = false;
            float levelEndTime = Time.time + 10f; // Messy fix in cases where remaining enemies doesn not properly hit 0.
            
            yield return new WaitUntil(() => remainingEnemies == 0 || Time.time >= levelEndTime);
            CleanUpSpawnList();
            yield return StartCoroutine(ShowLevelText("Next Wave Incoming"));

            level++;
        }

        winScreen.SetActive(true);
        PauseManager.instance.PauseTime();
        Debug.Log("All levels complete!");
    }

    private IEnumerator ShowLevelText(string message)
    {
        levelTextPanel.SetActive(true);
        levelText.text = message;
        yield return new WaitForSeconds(breakDuration);

        levelTextPanel.SetActive(false);
    }

    /*void Update()
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
    }*/

    /*private void SpawnEnemy()
    {
        GameObject enemy = PickWeightedEnemy(levels[level].enemies);
        Instantiate(enemy,GameManager.main.startPoint.position,Quaternion.identity);
    }*/

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

    // Pools the enemyspawn logic by pre-instantiating all enemies
    private List<GameObject> GenerateEnemyPool(Level levelData)//TODO: Addition.
    {
        List<GameObject> spawnList = new List<GameObject>();

        int spawnCount = Mathf.FloorToInt(levelData.spawnDuration / levelData.spawnInterval);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemyPrefab = PickWeightedEnemy(levelData.enemies);

            GameObject enemyInstance = Instantiate(
                enemyPrefab,
                GameManager.main.startPoint.position,
                Quaternion.identity);

            enemyInstance.SetActive(false);
            spawnList.Add(enemyInstance);
        }

        return spawnList;
    }

    // Spawns the next enemy from the pre-generated pool
    private void SpawnEnemyFromPool()//TODO: Addition.
    {
        if (spawnIndex >= currentSpawnList.Count)
            return;

        GameObject enemy = currentSpawnList[spawnIndex];

        enemy.SetActive(true);

        spawnIndex++;
    }

    // Destroys enemies in the current spawn list and empties the list
    private void CleanUpSpawnList()//TODO: Addition.
    {
        foreach (GameObject enemy in currentSpawnList)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        currentSpawnList.Clear();
        Debug.Log("Cleaned up spawn list. Remaining enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
    }

    private void PlayMusic(AudioClip clip)
    {
        SoundManager.instance.PlayMusic(clip);
    }

    public void EnemyDefeated()//TODO: Addition.
    {
        remainingEnemies--;
        UpdateRemainingEnemiesText();
    }

    public void UpdateRemainingEnemiesText()//TODO: Addition.
    {
        remainingEnemiesText.text = remainingEnemies.ToString();
    }
}