using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{
    PlayerStats PlayerStatsScript;
    public TextMeshProUGUI GemText;
    public TextMeshProUGUI DestroyedGemText;
    public TextMeshProUGUI DestroyedLiesText;

    void Start()
    {
        PlayerStatsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        GemText.text = "0";
        DestroyedGemText.text = "0";
        DestroyedLiesText.text = "0";
    }

    // Update is called once per frame
    public void UpdateText()
    {
        GemText.text = PlayerStatsScript.CollectedGems.ToString();
        DestroyedGemText.text = PlayerStatsScript.DestroyedGems.ToString();
        DestroyedLiesText.text = PlayerStatsScript.DestroyedLies.ToString();
    }
}
