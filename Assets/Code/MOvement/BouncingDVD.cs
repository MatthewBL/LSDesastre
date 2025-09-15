using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class BouncingDVD : IMovementTrajectory
{
    public IEnumerator MoveCoroutine(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        Canvas canvas = obj.GetComponentInParent<Canvas>();
        Vector2 direction = new Vector2(1, 1).normalized;
        float speed = 200f;

        // Cache values for performance
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta;
        Vector2 size = rectTransform.sizeDelta;
        float canvasScale = canvas.scaleFactor;

        float directionDelay = 0.25f; //Para impedir que rebote varias veces en el mismo borde
        float timeCounter = 0f; //Contador de tiempo para el delay

        while (true)
        {
            if (timeCounter < directionDelay)
            {         
            // Move the object
            rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

            // Get the current position of the object
            Vector2 pos = rectTransform.anchoredPosition;

            // Check collisions with edges of the canvas
            if (pos.x + size.x / 2 > canvasSize.x / 2 || pos.x - size.x / 2 < -canvasSize.x / 2)
                direction.x *= -1;
            if (pos.y + size.y / 2 > canvasSize.y / 2 || pos.y - size.y / 2 < -canvasSize.y / 2)
                direction.y *= -1;

            timeCounter += Time.deltaTime;
            }
            else
            {
                timeCounter = 0f;
            }
            yield return null;
        }
    }
}