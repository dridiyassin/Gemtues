using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public float maxHealth;
    public Slider BossHealth;
    public GameObject winPanel;

    public GameObject[] gems;
    public GameObject fakeGem;

    [HideInInspector]
    public float health;

    bool StartRotating;
    bool GoUp;
    bool ShowedPanel = false;
    bool Dielol = false;

    float cooldown = 10;
    float sidetoSidetime = 10;
    Animator anim;
    ScoreCalculator ScoreCalculs;
    PlayerStats PStats;
    GameObject BossParticles;
    void Start()
    {
        BossParticles = transform.GetChild(1).gameObject;
        BossParticles.SetActive(false);

        maxHealth = BossHealth.maxValue;
        health = maxHealth;
        BossHealth.value = health;
        StartCoroutine(Spell1C());
        anim = GetComponent<Animator>();

        ScoreCalculs = GameObject.FindGameObjectWithTag("GManager").GetComponent<ScoreCalculator>();
        PStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void FixedUpdate()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            anim.SetBool("SideToSide", true);
            sidetoSidetime -= Time.deltaTime;
            if (sidetoSidetime <= 0)
            {
                anim.SetBool("SideToSide", false);
                cooldown = 10;
                sidetoSidetime = 10;
            }
        }
        if (StartRotating)
        {
            Spell2();
        }
        if (GoUp)
        {
            if (transform.position.y < 10)
            {
                transform.Translate(Vector2.up * 5 * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y > 3.96f)
            {
                transform.Translate(Vector2.down * 5 * Time.deltaTime);
            }
            if(transform.position.y <= 4)
            {
                anim.enabled = true;
            }
        }
        if(Dielol)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }
    }
    public void UpdateAndMakeGem()
    {
        Instantiate(gems[Random.Range(0, gems.Length)], transform.position, Quaternion.identity);
        health -= 1;
        BossHealth.value = health;
        if(health <= 0)
        {
            BossParticles.SetActive(true);
            Dielol = true;
            Invoke("ShowWinPanel", 1);
            Destroy(gameObject, 1);
            FindObjectOfType<AudioManager>().Play("Die");
        }
    }
    void FinishLevel()
    {
        
        Invoke("ShowWinPanel", 2);
        
    }
    //Drop 5
    void Spell1()
    {
        Vector3 offset = new Vector3(0, 0);
        Vector3 AddOffset = new Vector3(2f, 0);
        for (int i = 0; i < 5; i++)
        {
            GameObject Gem = Instantiate(gems[Random.Range(0, gems.Length)], transform.position + offset, Quaternion.identity);
            Physics2D.IgnoreCollision(Gem.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            offset += AddOffset;
            offset = -offset;
        }
    }

    //rotate and shoot
    void Spell2()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }

    void ShowWinPanel()
    {
        if (!ShowedPanel)
        {
            ScoreCalculs.CalculateScore(PStats.CollectedGems, PStats.DestroyedGems, PStats.DestroyedLies);
            winPanel.SetActive(true);
            Animator panelAnim = winPanel.GetComponent<Animator>();
            panelAnim.SetTrigger("Drop");
            ShowedPanel = true;
        }
    }




    IEnumerator Spell1C()
    {

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            Spell1();
            if (i == 4)
            {
                StopCoroutine(Spell1C());
                StartCoroutine(Spell2C());

            }
            yield return null;
        }
    }
    IEnumerator Spell2C()
    {
        for (int i = 0; i < 17; i++)
        {
            StartRotating = true;
            yield return new WaitForSeconds(0.5f);
            GameObject BossBull = Instantiate(gems[Random.Range(0, gems.Length)], transform.position, Quaternion.identity);
            BossBull.transform.Translate(Vector3.forward * 10f);
            yield return null;
            if (i == 16)
            {
                StartRotating = false;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                StopCoroutine(Spell2C());
                StartCoroutine(Spell3C());
            }
        }
    }

    IEnumerator Spell3C()
    {
        GoUp = true;
        anim.enabled = false;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(3.5f);
            Vector3 offset = new Vector3(0, 0);
            Vector3 AddOffset = new Vector3(3f, 3f);
            for (int j = 0; j < 10; j++)
            {
                GameObject Gem = Instantiate(gems[Random.Range(0, gems.Length)],  transform.position + offset, Quaternion.identity);
                Physics2D.IgnoreCollision(Gem.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                offset += AddOffset;
                offset = -offset;

            }
            if (i == 2)
            {

                StopCoroutine(Spell3C());
                StartCoroutine(Spell1C());
                GoUp = false;
            }
        }
    }
}
