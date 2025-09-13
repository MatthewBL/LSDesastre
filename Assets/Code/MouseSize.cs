using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Add this for the new Input System

public class MouseSize : MonoBehaviour
{
    public GameObject mouseObject;
    public Main main = null;
    public Canvas canvas; // Assign your Canvas in the Inspector

    void Start()
    {
        Cursor.visible = false;
        if (mouseObject == null)
        {
            Debug.LogError("Mouse object is not assigned.");
            return;
        }
        // Disable raycast target on the Image component
        var img = mouseObject.GetComponent<Image>();
        if (img != null)
            img.raycastTarget = false;

        mouseObject.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
        SetMouseObjectPosition();
    }

    void Update()
    {
        SetMouseObjectPosition();
        SetMouseSize(32 + main.counter);
    }

    void SetMouseObjectPosition()
    {
        if (canvas == null || mouseObject == null)
            return;

        Vector2 mousePos = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;

        Vector2 localPoint;
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            mousePos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );
        mouseObject.GetComponent<RectTransform>().localPosition = localPoint;
    }

    void SetMouseSize(int size)
    {
        if (mouseObject != null)
        {
            mouseObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        }
    }
}
