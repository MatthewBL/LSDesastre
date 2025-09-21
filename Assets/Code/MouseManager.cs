using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Add this for the new Input System

public class MouseManager : MonoBehaviour
{
    public Main main = null;
    Canvas canvas = null;
    GameObject mouseObject = null;

    void Start()
    {
        mouseObject = this.gameObject;
        // Obtain the canvas where this object is placed
        canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false;
        if (mouseObject == null)
        {
            return;
        }
        // Disable raycast target on the Image component
        var img = mouseObject.GetComponent<Image>();
        if (img != null)
            img.raycastTarget = false;

        mouseObject.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
        SetMouseObjectPosition();

        ChangeToRandomColor(img);
    }

    void Update()
    {
        SetMouseObjectPosition();
        SetMouseSize(32 + main.score * 4);
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

    // Method to change to a random color while preserving alpha
    public void ChangeToRandomColor(Image image)
    {
        if (image != null)
        {
            // Store the current alpha value
            float currentAlpha = image.color.a;

            // Generate a random color with full opacity
            Color randomColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                1f // Start with full opacity
            );

            // Apply the original alpha value
            randomColor.a = currentAlpha;

            // Set the new color
            image.color = randomColor;
        }
    }
}
