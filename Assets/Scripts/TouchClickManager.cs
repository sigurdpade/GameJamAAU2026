using UnityEngine;
using UnityEngine.InputSystem;

public class TouchClickManager : MonoBehaviour
{
    private Vector2 lastPointerPosition;
    public LayerMask clickableLayer;

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
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, clickableLayer);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<ClickableObject>()?.OnClicked();
        }
    }
}