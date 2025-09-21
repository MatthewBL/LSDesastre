using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class BackgroundClicker : MonoBehaviour, IPointerClickHandler
{
    public Main main = null;
    private float time = 0f;
    private float transitionTime = 1f;
    Image image = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = transitionTime;
        image = GetComponent<Image>();
        ChangeToRandomColor(image);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // Gradually decrease transitionTime from 1f to 0.05f over 60 seconds
        float elapsed = Mathf.Min(Time.timeSinceLevelLoad, 60f);
        transitionTime = Mathf.Lerp(1f, 0.2f, elapsed / 60f);

        if (time >= transitionTime)
        {
            time = 0;
            StartRandomColorTransition(image, transitionTime);
        }
    }

    // Increases counter by 1 when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        main.IncreaseCounter();
    }

    // Method to change to a random color while preserving alpha
    public void ChangeToRandomColor(Image image)
    {
        if (image != null)
        {
            // Store the current alpha value
            float currentAlpha = image.color.a;

            Color randomColor = ObtainRandomColor();

            // Apply the original alpha value
            randomColor.a = currentAlpha;

            // Set the new color
            image.color = randomColor;
        }
    }

    public Color ObtainRandomColor()
    {
        // Generate a random color with full opacity
        Color randomColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1f // Start with full opacity
        );

        return randomColor;
    }

    // Coroutine to smoothly transition between colors over time
    public IEnumerator SmoothColorTransition(Image image, Color targetColor, float transitionTime)
    {
        if (image == null) yield break;

        Color startColor = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionTime);

            // Smooth interpolation (you can use different easing functions)
            image.color = Color.Lerp(startColor, targetColor, t);

            yield return null; // Wait until next frame
        }

        // Ensure we end exactly at the target color
        image.color = targetColor;
    }

    // Method to start a random color transition
    public void StartRandomColorTransition(Image image, float transitionTime)
    {
        if (image != null)
        {
            // Store current alpha
            float currentAlpha = image.color.a;

            // Generate random target color with full opacity
            Color targetColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                1f
            );

            // Apply original alpha to target color
            targetColor.a = currentAlpha;

            // Start the coroutine
            StartCoroutine(SmoothColorTransition(image, targetColor, transitionTime));
        }
    }

    // Alternative method with custom start color
    public void StartColorTransition(Image image, Color startColor, Color targetColor, float transitionTime)
    {
        if (image != null)
        {
            // Preserve alpha from the image's current color
            float currentAlpha = image.color.a;
            targetColor.a = currentAlpha;
            startColor.a = currentAlpha;

            image.color = startColor; // Set starting color
            StartCoroutine(SmoothColorTransition(image, targetColor, transitionTime));
        }
    }
}