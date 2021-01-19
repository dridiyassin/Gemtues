using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovements : MonoBehaviour
{
    public float Speed;

    public float bulletCD;
    public GameObject bullet;
    public Transform bulletPos;

    public Transform Nose;
    public float NoseGrowthRate = 2f;
    public Transform Statue;

    public Rigidbody2D rb;


    float bulletCurrentCD;

    [Header("AfterDeath")]
    public GameObject panelToLoad;
    [Header("Boosters")]
    public bool AllowMagnent = false;
    public float MagnentMaxCD = 20f;
    public float MagnentCD;
    public bool AllowDestroyer = false;
    public float LiesDestroyerMaxCD = 30f;
    public float LiesDestroyerCD;



    private PlayerStats PStats;
    private CameraShaker shakescript;
    private GameStats Gstats;
    private ScoreCalculator ScoreCalculs;
    private Animator GraphicAnim;

    private PointEffector2D PF2D;
    private bool isGameOver = false;

    void Start()
    {
        bulletCurrentCD = 0;
        GraphicAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        shakescript = GameObject.FindGameObjectWithTag("GManager").GetComponent<CameraShaker>();
        Gstats = GameObject.FindGameObjectWithTag("GManager").GetComponent<GameStats>();
        ScoreCalculs = GameObject.FindGameObjectWithTag("GManager").GetComponent<ScoreCalculator>();
        PStats = GetComponent<PlayerStats>();
        PF2D = GetComponent<PointEffector2D>();
    }
    void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(hor * Speed * Time.deltaTime, 0f));
        if (hor != 0)
        {
            GraphicAnim.SetBool("Run", true);
        } else {
            GraphicAnim.SetBool("Run", false);
}
        bulletCurrentCD -= Time.deltaTime;
        if (MagnentCD >= 0)
        {
            MagnentCD -= Time.deltaTime;
        }
        if (LiesDestroyerCD >= 0)
        {
            LiesDestroyerCD -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (bulletCurrentCD <= 0)
            {
                Shoot();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if(AllowMagnent)
            {
                if (MagnentCD <= 0)
                {
                    UseMagnent();
                    MagnentCD = MagnentMaxCD;
                }
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (AllowDestroyer)
            {
                if (LiesDestroyerCD <= 0)
                {
                    
                    UseLieDestroyer();
                    LiesDestroyerCD = LiesDestroyerMaxCD;
                }
            }
        }

        if(isGameOver)
        {
            Statue.Rotate(new Vector3(0f, 0f, -60f * Time.deltaTime));
            Statue.Translate(new Vector2(5f, -2f) * Time.deltaTime);
            shakescript.LongShakeCam();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);

        FindObjectOfType<AudioManager>().Play("Shoot");

        bulletCurrentCD = bulletCD;
    }
    void BiggerNose()
    {
        Nose.transform.localScale += new Vector3(NoseGrowthRate, 0);
        Nose.transform.position += new Vector3(NoseGrowthRate / 2, 0f);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("gem"))
        {
            GemCapacity GemCap = other.gameObject.GetComponent<GemCapacity>();
            FindObjectOfType<AudioManager>().Play("PickUp");
            Destroy(other.gameObject);
            PStats.CollectedGems += GemCap.Gems;
            Gstats.UpdateText();
        }
        if (other.gameObject.CompareTag("rock"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Hurt");
            BiggerNose();
            shakescript.ShakeCam();
            CheckStatueNose();
        }
    }
    void CheckStatueNose()
    {
        if (Nose.localScale.x >= 7)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
            Animator StatueAnim = transform.parent.gameObject.GetComponent<Animator>();
            StatueAnim.enabled = false;
            
            isGameOver = true;
            Invoke("KillStatue", 3);
            FindObjectOfType<AudioManager>().Play("Die");

    }
    void ShowDeathPanel()
    {
        ScoreCalculs.CalculateScore(PStats.CollectedGems, PStats.DestroyedGems, PStats.DestroyedLies);
        panelToLoad.SetActive(true);
        Animator panelAnim = panelToLoad.GetComponent<Animator>();
        panelAnim.SetTrigger("Drop");
    }
    void KillStatue()
    {
        ShowDeathPanel();
        Destroy(transform.parent.gameObject);
        
    }


    void UseMagnent()
    {
        PF2D.enabled = true;
        Invoke("DisableMagnent", 8.5f);
    }
    void DisableMagnent()
    {
        PF2D.enabled = false;
    }
    void UseLieDestroyer()
    {
        GameObject[] Lies = GameObject.FindGameObjectsWithTag("rock");
        for (int i = 0; i < Lies.Length; i++)
        {
            Lies[i].transform.GetChild(0).gameObject.SetActive(true);
            Destroy(Lies[i], 0.5f);
            Invoke("ShakeAllCam", 0.5f);
        }
    }
    void ShakeAllCam()
    {
        shakescript.ShakeCam();
    }
}
