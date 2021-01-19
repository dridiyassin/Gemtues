using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fakegem : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    void Start()
    {
        Destroy(gameObject, 15f);
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
    }

}
