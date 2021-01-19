using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LieAutoScan : MonoBehaviour
{
    public float ScanCD;
    public float MaxScanCD = 10;
    public float ScanSpeed;
    public float ScanCircleLimit = 30f;
    public string ScanTxt;
    public TextMeshProUGUI ScanOutput;
    public GameObject ScanCircle;
 
    void Start()
    {
        ScanCircle.SetActive(false);
    }
    
    void FixedUpdate()
    {
        if (ScanCD >= 0)
        {
            ScanCD -= Time.deltaTime;
        }
        if (ScanCircle == null)
        {
            return;
        } else
        {
            if (ScanCD <= 0)
            {

                Scan();
            }

            if (ScanCircle.transform.localScale.x >= ScanCircleLimit)
            {
                ScanCircle.SetActive(false);
                ScanCircle.transform.localScale = Vector3.one;
                ScanCD = MaxScanCD;
            }
            ScanOutput.text = ScanCD.ToString("0");
        }
    }

    void Scan()
    {
        ScanCircle.SetActive(true);
        ScanCircle.transform.localScale += Vector3.one * ScanSpeed * Time.deltaTime;
    }
}
