using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesAndBoosters : MonoBehaviour
{
    [System.Serializable]
    public class shopItems
    {
        [Header("Input")]
        public Type UpgradeOrBooster;
        public bool isBought = false;
        public Sprite Icon;
        public string Title;
        public float Price;
        public int ID;
        
        [Header("Output")]
        public Button IconOutPut;
        public TextMeshProUGUI TitleOutPut;
        public TextMeshProUGUI PriceOutPut;

    }

    public shopItems[] UpB;

    PlayerStatsMenu PlayerStats;

    void Start()
    {
        PlayerStats = GetComponent<PlayerStatsMenu>();

        for (int i = 0; i < UpB.Length; i++)
        {
            UpB[i].IconOutPut.image.sprite = UpB[i].Icon;
            UpB[i].TitleOutPut.text = UpB[i].Title.ToString();
            UpB[i].PriceOutPut.text = UpB[i].Price.ToString();


            UpB[i].isBought = intToBool(PlayerPrefs.GetInt("Bought" + UpB[i].ID));
            if(UpB[i].isBought)
            {
                UpB[i].IconOutPut.interactable = false;
            } else
            {
                UpB[i].IconOutPut.interactable = true;
            }
        }
    }

    public void ClickUpB(int ID)
    {
        if(PlayerStats.TotalGems >= UpB[ID].Price)
        {

            UpB[ID].isBought = true;
            PlayerPrefs.SetInt("Bought" + UpB[ID].ID, boolToInt(UpB[ID].isBought));
            UpB[ID].IconOutPut.interactable = false;
            PlayerStats.TotalGems -= UpB[ID].Price;
            PlayerPrefs.SetFloat("TotalGems", PlayerStats.TotalGems);
            PlayerStats.UpdateTxt();
        }
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


    public enum Type { Booster, Upgrade}
}
