using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    private GameObject selectedTower;
    private Tower selectedTowerScriptable;
    private GameObject selectedPlot;

    public int money;

    public void SelectTower(GameObject tower)
    {
        selectedTower = tower;
        selectedTowerScriptable = selectedTower.GetComponent<TowerHolder>().tower;
        selectedTower.GetComponent<Image>().color = Color.red;
        //effects to see selection

        if (selectedPlot != null)
        {
            BuildTower();
        }
    }

    public void SelectPlot(GameObject plot)
    {
        selectedPlot = plot;
        selectedPlot.GetComponent<SpriteRenderer>().color = Color.red;
        //effects to see selection
        if(selectedTower != null)
        {
            BuildTower();
        }
    }

    public void BuildTower()
    {
        if (selectedTowerScriptable.cost > money)
            return;

        money -= selectedTowerScriptable.cost;
        
        //build the tower at the plot
        Instantiate(selectedTowerScriptable.towerObject, selectedPlot.transform.position, selectedPlot.transform.rotation);

        selectedTower.GetComponent<Image>().color = Color.white;
        selectedTower = null;
        selectedTowerScriptable = null;

        selectedPlot.GetComponent<SpriteRenderer>().color = Color.white;
        selectedPlot.SetActive(false);
        selectedPlot = null;
        //do de-select effects

    }
}
