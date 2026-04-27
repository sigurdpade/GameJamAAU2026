using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject plot;

    public void OnClicked()
    {
        if (plot != null)
        {
            GameObject.Find("GameManager").GetComponent<TowerBuilder>().SelectPlot(plot);
        }
    }
}
