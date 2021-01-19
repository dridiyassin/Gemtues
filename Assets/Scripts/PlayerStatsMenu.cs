using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsMenu : MonoBehaviour
{
    public float TotalGems;
    public float DestroyedLies;

    public TextMeshProUGUI GemsTxt;
    public TextMeshProUGUI DestroyedLiesTxt;
    public Button Level2;
    public Button Level3;
    public Slider Progress;
    void Start()
    {
        UpdateTxt();
        if(DestroyedLies >= 30)
        {
            Level2.interactable = true;
        }
        if (DestroyedLies >= 80)
        {
            Level3.interactable = true;
        }
    }

    public void UpdateTxt()
    {
        TotalGems = PlayerPrefs.GetFloat("TotalGems");
        DestroyedLies = PlayerPrefs.GetFloat("TotalDestroyedLies");

        GemsTxt.text = TotalGems.ToString();
        DestroyedLiesTxt.text = DestroyedLies.ToString();
        UpdateProgress();
    }
    public void UpdateProgress()
    {
        Progress.value = DestroyedLies;
    }
}
