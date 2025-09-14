using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; // List of obstacle prefabs to choose from
    public Canvas targetCanvas; // Assign your Canvas in the Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateObstacle()
    {
        if (obstaclePrefabs.Count == 0 || targetCanvas == null)
            return;

        int r = UnityEngine.Random.Range(0, obstaclePrefabs.Count);
        GameObject prefab = obstaclePrefabs[r];

        // Instantiate as a child of the canvas
        GameObject obstacle = Instantiate(prefab, targetCanvas.transform);

        // Set random anchored position within the canvas RectTransform
        RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
        RectTransform obstacleRect = obstacle.GetComponent<RectTransform>();

        float x = UnityEngine.Random.Range(-canvasRect.rect.width / 2f, canvasRect.rect.width / 2f);
        float y = UnityEngine.Random.Range(-canvasRect.rect.height / 2f, canvasRect.rect.height / 2f);
        obstacleRect.anchoredPosition = new Vector2(x, y);
    }
}
