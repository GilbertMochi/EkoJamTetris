using UnityEngine;
using TMPro;

public class ScoreAndTimer : MonoBehaviour
{
    float timer;
    private int score;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI scoreTxt;
    
    void Start() 
    {
        timer = 0.0f;
    }

    void Update() 
    {
        timer += Time.deltaTime;
        timerTxt.text = timer.ToString("F2");
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreTxt.text = this.score.ToString();
    }
}
