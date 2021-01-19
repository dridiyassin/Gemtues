using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float DestroyedGems;
    public float DestroyedLies;
    public float CollectedGems;

    void Start()
    {
        CollectedGems = 0;
        DestroyedGems = 0;
        DestroyedLies = 0;
    }
}
