using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Obstacle : MonoBehaviour, IPointerClickHandler
{
    [Header("Settings")]
    public bool debugMode = true;
    [Range(0f, 1f)]
    public float alphaThreshold = 0.1f; // Minimum alpha for click detection

    private Image uiImage;
    private RectTransform rectTransform;

    // Movement trajectory
    public enum TrajectoryType { LeftToRight, Rotate, UpAndDown, BouncingDVD }
    public TrajectoryType trajectoryType;

    private IMovementTrajectory movementTrajectory;
    private Coroutine movementCoroutine;

    void Start()
    {
        uiImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        if (uiImage == null)
        {
            Debug.LogError("No Image component found on this GameObject!");
            return;
        }

        if (uiImage.sprite == null)
        {
            Debug.LogError("No sprite assigned to the Image component!");
            return;
        }

        // Set the alpha threshold for click detection
        uiImage.alphaHitTestMinimumThreshold = alphaThreshold;

        Debug.Log($"Alpha threshold set to: {alphaThreshold}. Clicks will only register on pixels with alpha > {alphaThreshold}");

        switch (trajectoryType)
        {
            case TrajectoryType.LeftToRight:
                movementTrajectory = new Sideways();
                break;
            case TrajectoryType.Rotate:
                movementTrajectory = new RotateTrajectory();
                break;
            case TrajectoryType.UpAndDown:
                movementTrajectory = new UpAndDown();
                break;
            case TrajectoryType.BouncingDVD:
                movementTrajectory = new BouncingDVD();
                break;
        }

        if (movementTrajectory != null)
        {
            movementCoroutine = StartCoroutine(movementTrajectory.MoveCoroutine(gameObject));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // This will only be called if the click hit a pixel with alpha > alphaThreshold
        if (uiImage == null || uiImage.sprite == null)
            return;

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // Convert local point to UV coordinates (0-1 range) within the RectTransform
            Rect rect = rectTransform.rect;
            Vector2 normalizedPoint = new Vector2(
                (localPoint.x - rect.x) / rect.width,
                (localPoint.y - rect.y) / rect.height
            );

            // Get the color from the sprite texture at this UV position
            Color pixelColor = GetColorFromSpriteTexture(normalizedPoint);

            // Print the color information
            Debug.Log($"Clicked on visible pixel! UV: {normalizedPoint}, Color: {pixelColor}, RGBA: ({pixelColor.r}, {pixelColor.g}, {pixelColor.b}, {pixelColor.a})");

            // Optional: Visual feedback in debug mode
            if (debugMode)
            {
                ShowDebugInfo(eventData.position, pixelColor);
            }

            //Si colorobtenido no es transparente, se ejecuta la acción
            if (pixelColor.a > alphaThreshold)
            {
                // Llamar a la función acción aquí
                Debug.Log("Acción llamada porque el pixel no es transparente.");
            }
        }
    }

    private Color GetColorFromSpriteTexture(Vector2 uv)
    {
        Texture2D texture = uiImage.sprite.texture;

        // Convert UV coordinates to pixel coordinates
        int pixelX = Mathf.FloorToInt(uv.x * texture.width);
        int pixelY = Mathf.FloorToInt(uv.y * texture.height);

        // Clamp coordinates to texture bounds
        pixelX = Mathf.Clamp(pixelX, 0, texture.width - 1);
        pixelY = Mathf.Clamp(pixelY, 0, texture.height - 1);

        // Get the pixel color
        return texture.GetPixel(pixelX, pixelY);
    }

    private void ShowDebugInfo(Vector2 position, Color color)
    {
        // Create a small debug object at the click position
        GameObject debugObj = new GameObject("DebugPixel");
        debugObj.transform.SetParent(transform, false);

        Image debugImage = debugObj.AddComponent<Image>();
        debugImage.color = color;

        RectTransform debugRect = debugObj.GetComponent<RectTransform>();
        debugRect.sizeDelta = new Vector2(20, 20);

        // Convert screen position to local position for the debug object
        Vector2 localPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, position, null, out localPosition))
        {
            debugRect.localPosition = localPosition;
        }

        // Destroy after 2 seconds
        Destroy(debugObj, 2f);
    }

    // Public method to adjust alpha threshold at runtime
    public void SetAlphaThreshold(float threshold)
    {
        alphaThreshold = Mathf.Clamp01(threshold);
        if (uiImage != null)
        {
            uiImage.alphaHitTestMinimumThreshold = alphaThreshold;
        }
    }

    void OnDestroy()
    {
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
    }
}