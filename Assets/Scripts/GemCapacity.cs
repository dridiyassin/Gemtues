using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCapacity : MonoBehaviour
{
    public float Gems;

    void Start()
    {
        Destroy(gameObject, 10f);
    }
}
