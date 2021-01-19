using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Animator camAnim;
    public void ShakeCam()
    {
        camAnim.SetTrigger("Shake");
    }
    public void LongShakeCam()
    {
        camAnim.SetTrigger("Longshake");
    }
}
