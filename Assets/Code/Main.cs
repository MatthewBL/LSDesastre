using UnityEngine;

public class Main : MonoBehaviour
{
    public int counter = 0;
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
        counter += 1;
        obstacleGenerator.GenerateObstacle();
    }
}
