using UnityEngine;
using System.Collections;
using TMPro;

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

    [SerializeField] private int level = 0;
    private float spawnCounter;
    private bool isSpawning = false;

    public AudioClip changeLevelSFX;
    public GameObject winScreen;

    void Start()
    {
        //spawnCounter = levels[level].spawnInterval;
        StartCoroutine(LevelLoop());
    }

    IEnumerator LevelLoop()
    {
        while (level < levels.Length)
        {
            if (levels[level].music != null)
                PlayMusic(levels[level].music);

            SoundManager.instance.PlayImportantSFX(changeLevelSFX);
            yield return StartCoroutine(ShowLevelText("Level " + (level + 1)));

            isSpawning = true;
            spawnCounter = levels[level].spawnInterval;

            float timer = 0f;

            while (timer < levels[level].spawnDuration)
            {
                timer += Time.deltaTime;

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0f)
                {
                    SpawnEnemy();
                    spawnCounter = levels[level].spawnInterval;
                }

                yield return null;
            }

            isSpawning = false;

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

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

    private void PlayMusic(AudioClip clip)
    {
        SoundManager.instance.PlayMusic(clip);
    }
}