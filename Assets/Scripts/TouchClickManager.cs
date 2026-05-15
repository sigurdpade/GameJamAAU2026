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

            for (int j = 0; j < 3; j++)
            {
                buyMenues[i].transform.GetChild(0).GetChild(j).GetComponent<Image>().color = Color.white;
                buyMenues[i].transform.GetChild(0).GetChild(j).GetComponent<Button>().interactable = true;
            }
        }

        if (selectedTower == null)
        {
            buyMenues[0].SetActive(true);
            return;
        }

        int type = selectedTower.GetComponent<TowerBehavior>().towerType;
        int tier = selectedTower.GetComponent<TowerBehavior>().towerTier;

        buyMenues[type].SetActive(true);

        for (int i = 0; i < tier; i++)
        {
            Image image = buyMenues[type].transform.GetChild(0).GetChild(i).GetComponent<Image>();
            Button button = buyMenues[type].transform.GetChild(0).GetChild(i).GetComponent<Button>();

            if (i == tier - 1)
                image.color = Color.darkOliveGreen;
            else
                image.color = Color.gray;

            button.interactable = false;
        }
    }
}