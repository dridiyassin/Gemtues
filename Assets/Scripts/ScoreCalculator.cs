using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{

    public TextMeshProUGUI ScoreOutput;

    public TextMeshProUGUI HighScoreUI;

    public float LiesMulti = 10;

    float collectedGems;
    float destroyedGems;
    float destroyedLies;

    float totalGems;
    float totalDLies;

    public float Score;
    float Highscore;

    void Start()
    {
        Highscore = PlayerPrefs.GetFloat("HighScore");
    }

    public void CalculateScore(float CGems, float DGems, float DLies)
    {
        collectedGems = CGems;
        destroyedGems = DGems;
        destroyedLies = DLies;

        totalGems = PlayerPrefs.GetFloat("TotalGems");
        totalDLies = PlayerPrefs.GetFloat("TotalDestroyedLies");

        totalGems += CGems;
        totalDLies += destroyedLies;

        PlayerPrefs.SetFloat("TotalGems", totalGems);
        PlayerPrefs.SetFloat("TotalDestroyedLies", totalDLies);

        if (destroyedGems <= 0)
        {
            Score = (destroyedLies * LiesMulti) + collectedGems;
        }
        else
        {
            Score = ((destroyedLies * LiesMulti) / destroyedGems) + collectedGems;
        }
        Debug.Log(Score);

        CheckHighScore();
        UpdateUI();
    }

    void CheckHighScore()
    {
        if(Score >= Highscore)
        {
            Highscore = Score;
            PlayerPrefs.SetFloat("HighScore", Highscore);
        }
    }
    void UpdateUI()
    {
        ScoreOutput.text = Score.ToString("0");
        HighScoreUI.text = Highscore.ToString("0");
    }
}
