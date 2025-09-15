using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; // List of obstacle prefabs to choose from
    public List<float> obstacleWeights; //Probabilities for each obstacle (prefabs and weights must match in size)
    public Canvas targetCanvas; // Assign your Canvas in the Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (obstaclePrefabs.Count != obstacleWeights.Count)
        {
            Debug.LogError("Las listas de objetos y pesos no tienen la misma longitud");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateObstacle()
    {
        if (obstaclePrefabs.Count == 0 || targetCanvas == null)
            return;

        float totalWeight = 0f;
        int selectedIndex = 0;
        foreach (float weight in obstacleWeights)
        {
            totalWeight += weight;
        }

           // Genera un valor aleatorio entre 0 y la suma total de pesos
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < obstaclePrefabs.Count; i++)
        {
            cumulativeWeight += obstacleWeights[i];
            if (randomValue <= cumulativeWeight)
            {
                selectedIndex = i;
                break;
            } 
        }

        Debug.Log("Random value: " + randomValue + " / Total weight: " + totalWeight + ". Selected index: "+ selectedIndex);

        GameObject prefab = obstaclePrefabs[selectedIndex];

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
