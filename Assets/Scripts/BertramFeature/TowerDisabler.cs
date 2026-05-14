using UnityEngine;
using System.Collections.Generic;

public class TowerDisabler : MonoBehaviour
{
    public GameObject virusPrefab;
    public int towersToDisable;

    public void DisableRandomTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        List<GameObject> available = new List<GameObject>();

        foreach (var tower in towers)
        {
            if (!tower.GetComponent<TowerVirus>())
                available.Add(tower);
        }

        for (int i = 0; i < towersToDisable && available.Count > 0; i++)
        {
            int index = Random.Range(0, available.Count);
            GameObject tower = available[index];

            InfectTower(tower);
            available.RemoveAt(index);
        }
    }

    void InfectTower(GameObject tower)
    {
        TowerBehavior tb = tower.GetComponent<TowerBehavior>();

        if (tb != null)
            tb.enabled = false;

        GameObject virus = Instantiate(
            virusPrefab,
            tower.transform.position,
            Quaternion.identity
        );

        virus.transform.SetParent(tower.transform);

        TowerVirus tv = virus.GetComponent<TowerVirus>();
        tv.Initialize(tower);
    }
}
