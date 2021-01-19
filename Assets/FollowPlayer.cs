using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            transform.position = target.position;
        }
    }
}
