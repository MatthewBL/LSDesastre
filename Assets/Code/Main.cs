using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public int score = 0;
    public bool playing= false;
    public ObstacleGenerator obstacleGenerator = null;
    public float remaining = 60f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        playing = true;
        while (remaining > 0)
        {
            if (playing){
            Debug.Log("Remaining: " + remaining);
            yield return new WaitForSeconds(1f);
            remaining--;
            }
        }
        
        SceneManager.LoadScene("EndScene");
    }


    public void IncreaseCounter()
    {
        score += 1;
        obstacleGenerator.GenerateObstacle();
    }
}
