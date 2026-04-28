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

    private TouchClickManager tcm;

    [Header("UI Elements...")]
    public TMP_Text moneyText;
    public AudioClip placeBuildingSFX;

    private void Start()
    {
        tcm = GameObject.Find("GameManager").GetComponent<TouchClickManager>();
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
            BuildTower(selectedTower);
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
            BuildTower(selectedTower);
        }
    }

    public void DeselectPlot ()
    {
        if (selectedPlot != null)
        {
            selectedPlot.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void BuildTower(GameObject tower)
    {
        if (tcm.selectedTower != null)
        {
            selectedTower = tower;
            selectedTowerScriptable = selectedTower.GetComponent<TowerHolder>().tower;

            if (selectedTowerScriptable.cost > money)
            {
                selectedTower.GetComponent<Image>().color = Color.white;
                selectedTower = null;
                selectedTowerScriptable = null;
                return;
            }

            money -= selectedTowerScriptable.cost;

            if (tcm.selectedTower.GetComponent<TowerBehavior>().towerTier == 1)
                Instantiate(selectedTowerScriptable.towerObject2, tcm.selectedTower.transform.position, tcm.selectedTower.transform.rotation);
            if (tcm.selectedTower.GetComponent<TowerBehavior>().towerTier == 2)
                Instantiate(selectedTowerScriptable.towerObject3, tcm.selectedTower.transform.position, tcm.selectedTower.transform.rotation);

            SoundManager.instance.PlayImportantSFX(placeBuildingSFX);
            selectedTower.GetComponent<Image>().color = Color.white;
            selectedTower = null;
            selectedTowerScriptable = null;
            Destroy(tcm.selectedTower);

            tcm.selectedTower = null;
            tcm.ShowBuyMenu();
            UpdateUI();
            return;
        }

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
        Instantiate(selectedTowerScriptable.towerObject1, selectedPlot.transform.position, selectedPlot.transform.rotation);
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
