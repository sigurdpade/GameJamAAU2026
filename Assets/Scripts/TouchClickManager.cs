using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchClickManager : MonoBehaviour
{
    private Vector2 lastPointerPosition;
    public LayerMask clickableLayer;
    public GameObject selectedTower;
    public AudioClip tapSound;

    public GameObject[] buyMenues;

    public void OnPosition(InputAction.CallbackContext context)
    {
        lastPointerPosition = context.ReadValue<Vector2>();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.performed)
            HandleClick(lastPointerPosition);
    }

    void HandleClick(Vector2 screenPos)
    {
        SoundManager.instance.PlaySFX(tapSound);

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, clickableLayer);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<ClickableObject>()?.OnClicked();
        } /*else
        {
            DeselectTower();
        }*/
    }

    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.GetComponent<SpriteRenderer>().color = Color.white;
            selectedTower.GetComponent<TowerBehavior>().rangeIndicator.SetActive(false);
            selectedTower = null;
        }
        ShowBuyMenu();
    }

    public void SelectTower(GameObject tower)
    {
        selectedTower = tower;
        selectedTower.GetComponent<SpriteRenderer>().color = Color.red;
        selectedTower.GetComponent<TowerBehavior>().rangeIndicator.SetActive(true);
        GameObject.Find("GameManager").GetComponent<TowerBuilder>().DeselectPlot();
        ShowBuyMenu();
    }

    public void ShowBuyMenu()
    {
        for (int i = 0; i < buyMenues.Length; i++)
        {
            buyMenues[i].SetActive(false);
            buyMenues[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.white;
            buyMenues[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.white;
            buyMenues[i].transform.GetChild(0).GetChild(2).GetComponent<Image>().color = Color.white;
        }

        if (selectedTower == null)
        {
            buyMenues[0].SetActive(true);
            return;
        }
        buyMenues[selectedTower.GetComponent<TowerBehavior>().towerType].SetActive(true);
        buyMenues[selectedTower.GetComponent<TowerBehavior>().towerType].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.darkOliveGreen;
        if (selectedTower.GetComponent<TowerBehavior>().towerTier == 2)
            buyMenues[selectedTower.GetComponent<TowerBehavior>().towerType].transform.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.darkOliveGreen;
        if (selectedTower.GetComponent<TowerBehavior>().towerTier == 3)
            buyMenues[selectedTower.GetComponent<TowerBehavior>().towerType].transform.GetChild(0).GetChild(2).GetComponent<Image>().color = Color.darkOliveGreen;
    }
}