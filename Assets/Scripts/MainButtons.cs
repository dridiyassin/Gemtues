using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{

    public void ClickLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClosePanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
    }
    public void SetSD()
    {
        Screen.SetResolution(800, 480, false);
    }
    public void SetHD()
    {
        Screen.SetResolution(1280, 720, false);
    }
    public void SetFullHD()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
