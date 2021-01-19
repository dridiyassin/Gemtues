using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool Played = false;
    public int TutorialLevel;
    public GameObject TutorialPanel;
    public GameObject Page1;
    public GameObject Page2;

    void Start()
    {
        Played = intToBool(PlayerPrefs.GetInt("Tutorial" + TutorialLevel));
        if (Played)
        {
            enabled = false;
        } else
        {
            ShowTutorial();
        }
        
    }

    public void NextButton()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }
    public void StartGame()
    {
        Page1.SetActive(false);
        Page2.SetActive(false);
        TutorialPanel.SetActive(false);
        Time.timeScale = 1;
        Played = true;
        PlayerPrefs.SetInt("Tutorial" + TutorialLevel, boolToInt(Played));
    }

    void ShowTutorial()
    {
        TutorialPanel.SetActive(true);
        Page1.SetActive(true);
        Page2.SetActive(false);

        Time.timeScale = 0;
        
    }



    #region Bool Prefs
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }
    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
    #endregion
}
