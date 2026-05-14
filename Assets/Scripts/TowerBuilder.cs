using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    private GameObject selectedTower;
    private Tower selectedTowerScriptable;
    private GameObject selectedPlot;


    public Tower tower;


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
        if (selectedTower == null)
    {
    Debug.LogError("selectedTower is NULL");
    return;
    }

    TowerHolder holder = selectedTower.GetComponent<TowerHolder>();

    if (holder == null)
    {
        Debug.LogError("TowerHolder component missing on " + selectedTower.name);
        return;
    }

    if (holder.tower == null)
    {
        Debug.LogError("tower field is NULL on " + selectedTower.name);
        return;
    }

selectedTowerScriptable = holder.tower;
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

    public void SellTower()
{
    if (tcm.selectedTower == null)
        return;

    TowerBehavior tower = tcm.selectedTower.GetComponent<TowerBehavior>();

    int sellAmount = 0;

    // Decide refund amount based on tier
    if (tower.towerTier == 1)
        sellAmount = 50;

    if (tower.towerTier == 2)
        sellAmount = 100;

    if (tower.towerTier == 3)
        sellAmount = 150;

    // Give money back
    money += sellAmount;

    // Reactivate plot
    if (tower.plot != null)
    {
        tower.plot.SetActive(true);
        tower.plot.GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Destroy tower
    Destroy(tcm.selectedTower);

    // Clear selection
    tcm.selectedTower = null;

    // Update UI
    tcm.ShowBuyMenu();
    UpdateUI();
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
            {
                GameObject newTower1 = Instantiate(selectedTowerScriptable.towerObject2, tcm.selectedTower.transform.position, tcm.selectedTower.transform.rotation);
                newTower1.GetComponent<TowerBehavior>().plot = tcm.selectedTower.GetComponent<TowerBehavior>().plot;
            }
            if (tcm.selectedTower.GetComponent<TowerBehavior>().towerTier == 2)
            {
                GameObject newTower2 = Instantiate(selectedTowerScriptable.towerObject3, tcm.selectedTower.transform.position, tcm.selectedTower.transform.rotation);
                newTower2.GetComponent<TowerBehavior>().plot = tcm.selectedTower.GetComponent<TowerBehavior>().plot;
            }

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
        GameObject newTower = Instantiate(selectedTowerScriptable.towerObject1, selectedPlot.transform.position, selectedPlot.transform.rotation);
        newTower.GetComponent<TowerBehavior>().plot = selectedPlot;
        
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
