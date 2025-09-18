using TMPro;
using UnityEngine;

public class EndScriptManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = MainUtils.finalScore;
        Debug.Log("Final Score: " + score);
        scoreText.text = "Final Score: " + score;
    }

}
