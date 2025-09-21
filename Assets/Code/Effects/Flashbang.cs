using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Flashbang : MonoBehaviour, IPointerClickHandler
{
    public float initialFlashDuration = 0.5f;
    public float flashDuration = 1f;
    private AudioSource audioSource = null;
    private Image flashImage;
    private Camera mainCamera;
    private RenderTexture renderTexture;
    private Texture2D screenTexture;
    private Image flashbangScreenshotImage;
    private FlashbangManager flashbangManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Trigger();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        flashbangManager = FindObjectOfType<FlashbangManager>();
        flashImage = flashbangManager.flashbangImage;
        flashbangScreenshotImage = flashbangManager.flashbangScreenshotImage;
        InitializeRenderTexture();
    }

    public void Trigger()
    {
        StartCoroutine(FlashRoutine());
    }

    void InitializeRenderTexture()
    {
        // Create render texture with screen dimensions
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private IEnumerator FlashRoutine()
    {
        // Play sound
        audioSource.Play();

        // Show white flash
        flashImage.color = Color.white;
        flashImage.gameObject.SetActive(true);
        flashbangScreenshotImage.gameObject.SetActive(true);

        // Set the camera's target texture
        mainCamera.targetTexture = renderTexture;

        // Render the camera's view to the render texture
        mainCamera.Render();

        // Set active render texture and read pixels
        RenderTexture.active = renderTexture;
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        // Reset camera target texture
        mainCamera.targetTexture = null;
        RenderTexture.active = null;

        // Create sprite and assign to image
        Sprite screenSprite = Sprite.Create(
            screenTexture,
            new Rect(0, 0, screenTexture.width, screenTexture.height),
            new Vector2(0.5f, 0.5f)
        );

        flashbangScreenshotImage.sprite = screenSprite;

        // Fade out
        float elapsed = 0f;
        while (elapsed < flashDuration + initialFlashDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f;
            if (elapsed >= initialFlashDuration)
            {
                alpha = Mathf.Lerp(1f, 0f, (elapsed - initialFlashDuration) / flashDuration);
            }
            flashImage.color = new Color(1, 1, 1, alpha);
            flashbangScreenshotImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        flashImage.gameObject.SetActive(false);
        flashbangScreenshotImage.gameObject.SetActive(false);
    }
}