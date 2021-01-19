using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{

    void Update()
    {
        transform.Translate(new Vector3(Random.Range(0.5f, 2) * Time.deltaTime, 0f, 0f));
    }
}
