using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    [Header("Types of Enemies")]
    [SerializeField] GameObject[] eligibleEnemiesLevel1;
    [SerializeField] GameObject[] eligibleEnemiesLevel2;
    [SerializeField] GameObject[] eligibleEnemiesLevel3;
    [SerializeField] GameObject[] eligibleEnemiesLevel4;


    [Header("Spawn Conditions")]
    [SerializeField] float spawnIntervalLevel1 = 2f;
    [SerializeField] float spawnIntervalLevel2 = 1.5f;
    [SerializeField] float spawnIntervalLevel3 = 1f;
    [SerializeField] float spawnIntervalLevel4 = 0.5f;
    [SerializeField] int level = 1;

    private float spawnCounter;
    private float levelTimer;

    void Start()
    {
        spawnCounter = spawnIntervalLevel1;
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        spawnCounter -= Time.deltaTime;

        if (levelTimer >= 10f && level < 4)
        {
            level++;
            levelTimer = 0f;
        }

        if (spawnCounter <=0){
            float spawnChance = Random.Range(0f, 1f);
            switch (level)
        {
            case 1:
                {
                    Instantiate(eligibleEnemiesLevel1[0], GameManager.main.startPoint.position, Quaternion.identity);
                }
                spawnCounter = spawnIntervalLevel1;
                break;
            case 2:
                {

                    if (spawnChance < 0.75f)
                    {
                        Instantiate(eligibleEnemiesLevel2[0], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(eligibleEnemiesLevel2[1], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                spawnCounter = spawnIntervalLevel2;
                }
                break;
            case 3:
                {
                    if (spawnChance < 0.5f)
                    {
                        Instantiate(eligibleEnemiesLevel3[0], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else if (spawnChance < 0.9f)
                    {
                        Instantiate(eligibleEnemiesLevel3[1], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(eligibleEnemiesLevel3[2], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                spawnCounter = spawnIntervalLevel3;

                }
                break;
            case 4:
                {
                    if (spawnChance < 0.5f)
                    {
                        Instantiate(eligibleEnemiesLevel4[0], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else if (spawnChance < 0.75f)
                    {
                        Instantiate(eligibleEnemiesLevel4[1], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else if (spawnChance < 0.9f)
                    {
                        Instantiate(eligibleEnemiesLevel4[2], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(eligibleEnemiesLevel4[3], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                spawnCounter = spawnIntervalLevel4;
                }
                break;
            default:
                {
                    if (spawnChance < 0.5f)
                    {
                        Instantiate(eligibleEnemiesLevel4[0], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else if (spawnChance < 0.75f)
                    {
                        Instantiate(eligibleEnemiesLevel4[1], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else if (spawnChance < 0.9f)
                    {
                        Instantiate(eligibleEnemiesLevel4[2], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(eligibleEnemiesLevel4[3], GameManager.main.startPoint.position, Quaternion.identity);
                    }
                }
                break;}
        }

    }
}