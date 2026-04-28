using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject plot;
    public TowerBehavior towerBehavior;
    private TouchClickManager tcm;

    public void Start()
    {
        tcm = GameObject.Find("GameManager").GetComponent<TouchClickManager>();

        if (towerBehavior == null)
            plot = gameObject;
    }
    public void OnClicked()
    {
        if (plot != null)
        {
            GameObject.Find("GameManager").GetComponent<TowerBuilder>().SelectPlot(plot);
            tcm.DeselectTower();
        }

        if (towerBehavior != null)
        {
            if (tcm.selectedTower != null)
            {
                tcm.DeselectTower();
            }

            tcm.SelectTower(gameObject.GetComponentInParent<TowerBehavior>().gameObject);
        }
    }
}
