using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject HeartPrefab;
    public Health Health;

    List<Heart> Hearts = new List<Heart>();

    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        CleartHeart();

        for (int i = 0; i < Health.maxHealth; i++)
        {
            CreateEmptyheart();
        }

        for (int i = 0; i < Hearts.Count; i++)
        {
            if (i < Health.health)
            {
                Hearts[i].SetheartImage(Heart.Heartstatus.full);
            }
            else
            {
                Hearts[i].SetheartImage(Heart.Heartstatus.empty);
            }
        }
    }

    public void CreateEmptyheart()
    {
        GameObject newHeart = Instantiate(HeartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetheartImage(Heart.Heartstatus.empty);

        Hearts.Add(heartComponent);
    }

    public void CleartHeart()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        Hearts.Clear();
    }
}