using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class TouchClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick(Input.mousePosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleClick(Input.GetTouch(0).position);
        }

        void HandleClick(Vector2 screenPos)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                hit.collider.GetComponent<ClickableObject>()?.OnClicked();
            }
        }
    }
}
