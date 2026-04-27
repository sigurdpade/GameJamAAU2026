using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject plot;
    public void Start()
    {
        plot = this.gameObject;
    }
    public void OnClicked()
    {
        if (plot != null)
        {
            GameObject.Find("GameManager").GetComponent<TowerBuilder>().SelectPlot(plot);
        }
    }
}
