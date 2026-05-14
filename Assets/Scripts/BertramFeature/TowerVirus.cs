using UnityEngine;

public class TowerVirus : MonoBehaviour
{
    public int clicksToClean = 5;

    private int currentClicks = 0;
    private GameObject tower;

    public void Initialize(GameObject targetTower)
    {
        tower = targetTower;
    }

    public void Click()
    {
        currentClicks++;

        transform.localScale *= 0.9f;

        if(currentClicks >= clicksToClean)
        {
            CleanTower();
        }
    }

    void CleanTower()
    {
        TowerBehavior tb = tower.GetComponent<TowerBehavior>();
        if (tb != null)
            tb.enabled = true;

        Destroy(gameObject);
    }
}
