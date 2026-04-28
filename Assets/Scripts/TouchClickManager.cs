using UnityEngine;
using UnityEngine.InputSystem;

public class TouchClickManager : MonoBehaviour
{
    private Vector2 lastPointerPosition;
    public LayerMask clickableLayer;
    public GameObject selectedTower;
    public AudioClip tapSound;

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
        }
    }

    public void DeselectTower()
    {
        selectedTower.GetComponent<SpriteRenderer>().color = Color.white;
        selectedTower.GetComponent<TowerBehavior>().rangeIndicator.SetActive(false);
    }

    public void SelectTower(GameObject tower)
    {
        selectedTower = tower;
        selectedTower.GetComponent<SpriteRenderer>().color = Color.red;
        selectedTower.GetComponent<TowerBehavior>().rangeIndicator.SetActive(true);
    }
}