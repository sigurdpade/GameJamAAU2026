using UnityEngine;
using UnityEngine.InputSystem;

public class TouchClickManager : MonoBehaviour
{
    private Vector2 lastPointerPosition;

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
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        Debug.Log("ScreenPos: " + screenPos);

        if (hit.collider != null)
        {
            Debug.Log("Clicked: " + hit.collider.name);
            hit.collider.GetComponent<ClickableObject>()?.OnClicked();
        }
    }
}