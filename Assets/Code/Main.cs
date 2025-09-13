using UnityEngine;

public class Main : MonoBehaviour
{
    public int score = 0;
    public ObstacleGenerator obstacleGenerator = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCounter()
    {
        score += 1;
        obstacleGenerator.GenerateObstacle();
    }
}
