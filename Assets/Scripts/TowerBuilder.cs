using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    private GameObject selectedTower;
    private Tower selectedTowerScriptable;
    private GameObject selectedPlot;

    public int money;

    [Header("UI Elements...")]
    public TMP_Text moneyText;
    public AudioClip placeBuildingSFX;

    private void Start()
    {
        UpdateUI();
    }

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
        if (selectedPlot != null)
        {
            selectedPlot.GetComponent<SpriteRenderer>().color = Color.white;
        }

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
        {
            selectedTower.GetComponent<Image>().color = Color.white;
            selectedTower = null;
            selectedTowerScriptable = null;

            selectedPlot.GetComponent<SpriteRenderer>().color = Color.white;
            selectedPlot = null;
            return;
        }

        money -= selectedTowerScriptable.cost;
        
        //build the tower at the plot
        Instantiate(selectedTowerScriptable.towerObject, selectedPlot.transform.position, selectedPlot.transform.rotation);
        SoundManager.instance.PlayImportantSFX(placeBuildingSFX);
        LearningPopUp.instance.TryShowInfo(selectedTowerScriptable.learningInformation, selectedTowerScriptable.name);

        selectedTower.GetComponent<Image>().color = Color.white;
        selectedTower = null;
        selectedTowerScriptable = null;

        selectedPlot.GetComponent<SpriteRenderer>().color = Color.white;
        selectedPlot.SetActive(false);
        selectedPlot = null;
        //do de-select effects

        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = "$" + money;
    }
}
