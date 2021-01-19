using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour
{
    public float GemsDurability;
    float CurrentGems;


    PlayerMovements PlayerControls;
    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
        PlayerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>();
        CurrentGems = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("gem") || other.gameObject.CompareTag("rock"))
        {
            Destroy(other.gameObject);
            CurrentGems += 1;
            checkDurability();
        }
    }
    void checkDurability()
    {
        float Percentage = (CurrentGems * 100) / GemsDurability;
        if(Percentage >= 20 && Percentage < 50)
        {
            Anim.SetTrigger("Balance1");
        } else
        if (Percentage >= 50 && Percentage < 80)
        {
            Anim.SetTrigger("Balance2");
            Anim.ResetTrigger("Balance1");
        }
        else
        if (Percentage >= 80 && Percentage < 100)
        {
            Anim.SetTrigger("Balance3");
            Anim.ResetTrigger("Balance2");
        }
        else
        if (CurrentGems >= GemsDurability)
        {
            PlayerControls.GameOver();
        }
        
    }
}
