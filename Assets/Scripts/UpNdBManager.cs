using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpNdBManager : MonoBehaviour
{
    [System.Serializable]
    public class Upgrades
    {
        public bool isBought = false;
        public int UpgradeID;
    }
    [System.Serializable]
    public class Booster
    {
        public bool isBought = false;
        public int BoosterID;
        public float Cooldown;
        public GameObject boosterUI;
        public TextMeshProUGUI CooldownUI;
    }

    public GameObject ReplacedBullet;


    public Upgrades[] Ups;
    public Booster[] Boosters;

    LieAutoScan LieScanner;
    PlayerMovements PlayerControls;
    void Start()
    {
        LieScanner = GetComponent<LieAutoScan>();
        PlayerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>();
        LoadUpB();
        if (Ups[0].isBought)
        {
            UseUps(0);
        }
        if (Ups[1].isBought)
        {
            UseUps(1);
        }
        if(Ups[2].isBought)
        {
            UseUps(2);
        }
        if(Boosters[0].isBought)
        {
            UnlockBoosters(3);
        }
        if (Boosters[1].isBought)
        {
            UnlockBoosters(4);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Boosters[0].CooldownUI.text = PlayerControls.MagnentCD.ToString("0");
        Boosters[1].CooldownUI.text = PlayerControls.LiesDestroyerCD.ToString("0");
    }

    void LoadUpB()
    {
        for (int i = 0; i < Ups.Length ; i++)
        {
            Ups[i].isBought = intToBool(PlayerPrefs.GetInt("Bought" + Ups[i].UpgradeID));
        }
        for (int i = 0; i < Boosters.Length; i++)
        {
            Boosters[i].isBought = intToBool(PlayerPrefs.GetInt("Bought" + Boosters[i].BoosterID));
        }
    }

    void UseUps(int ID)
    {
        if(ID == 0)
        {
            LieScanner.MaxScanCD -= 3;
            LieScanner.ScanSpeed += 1;
            LieScanner.ScanCircleLimit += 20;
        }
        if(ID == 1)
        {
            PlayerControls.bulletCD -= 0.5f;
            PlayerControls.bullet = ReplacedBullet;
        }
        if(ID == 2)
        {
            PlayerControls.NoseGrowthRate /= 2;
        }
    }
    void UnlockBoosters(int ID)
    {
        if (ID == 3)
        {
            Boosters[0].boosterUI.SetActive(true);
            PlayerControls.AllowMagnent = true;
        }
        if (ID == 4)
        {
            Boosters[1].boosterUI.SetActive(true);
            PlayerControls.AllowDestroyer = true;
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
}
