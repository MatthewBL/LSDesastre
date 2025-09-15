using System.Collections;
using UnityEngine;

public class ShakingEffect : IMovementTrajectory
{
    public IEnumerator MoveCoroutine(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Canvas canvas = obj.GetComponentInParent<Canvas>();
        Vector2 originalPosition = rectTransform.anchoredPosition;

        // Define shake parameters
        float shakeDuration = 0.5f;  // Duration of the shake in seconds
        float shakeMagnitude = 10f;  // Maximum magnitude of the shake
        float shakeCooldown = 2f;   // Time before the shake starts again (optional)

        while (obj.activeInHierarchy)
        {
            // Wait for cooldown before the next shake (optional)
            yield return new WaitForSeconds(shakeCooldown);

            // Start shaking
            float shakeTimer = 0f;
            while (shakeTimer < shakeDuration)
            {
                // Apply random offset to position
                Vector2 shakeOffset = new Vector2(
                    Random.Range(-shakeMagnitude, shakeMagnitude),
                    Random.Range(-shakeMagnitude, shakeMagnitude)
                );

                rectTransform.anchoredPosition = originalPosition + shakeOffset;

                // Increment timer
                shakeTimer += Time.deltaTime;

                yield return null;
            }

            // Reset to original position after shake ends
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
