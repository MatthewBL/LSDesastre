using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reposition : MonoBehaviour, IPointerClickHandler
{
    private Vector2 minPosition;
    private Vector2 maxPosition;

    void Start()
    {
        // Try to get the parent Canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            // Canvas rect is centered at (0,0), so min/max are half extents
            float halfWidth = canvasRect.rect.width / 2f;
            float halfHeight = canvasRect.rect.height / 2f;
            minPosition = new Vector2(-halfWidth, -halfHeight);
            maxPosition = new Vector2(halfWidth, halfHeight);
        }
        else
        {
            minPosition = new Vector2(-100f, -100f);
            maxPosition = new Vector2(100f, 100f);
        }
    }

    public void RepositionSiblings()
    {
        // Get the parent transform (should be the Canvas or a panel)
        Transform parent = transform.parent;

        if (parent == null)
        {
            return;
        }

        // Get all sibling transforms at the same hierarchy level
        List<Transform> siblings = new List<Transform>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            siblings.Add(child);
        }

        // Randomize positions of all siblings
        foreach (Transform sibling in siblings)
        {
            RectTransform rectTransform = sibling.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(minPosition.x, maxPosition.x),
                    Random.Range(minPosition.y, maxPosition.y)
                );

                rectTransform.anchoredPosition = randomPosition;
            }
        }

        Debug.Log($"Randomized positions for {siblings.Count} sibling objects");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RepositionSiblings();
    }
}
