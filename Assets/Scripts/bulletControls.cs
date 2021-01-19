using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControls : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject Particle;
    private CameraShaker Shake;
    PlayerStats stats;
    GameStats Gstats;
    void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Gstats = GameObject.FindGameObjectWithTag("GManager").GetComponent<GameStats>();
        Shake = GameObject.FindGameObjectWithTag("GManager").GetComponent<CameraShaker>();
    }
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, bulletSpeed * Time.deltaTime, 0f));
    }


    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("gem"))
            {
                    stats.DestroyedGems += 1;

                    
                    DestroyBullet(other.gameObject);
                
            }
            if (other.gameObject.CompareTag("rock"))
            {
                    stats.DestroyedLies += 1;

                    
                    DestroyBullet(other.gameObject);
            }
            if (other.gameObject.CompareTag("boss"))
            {
                BossAI Bassa = other.gameObject.GetComponent<BossAI>();
                Bassa.UpdateAndMakeGem();
                DestroyBullet(other.gameObject);
            }
    }
    void DestroyBullet(GameObject ToDestroy)
    {
        Instantiate(Particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(ToDestroy.gameObject);
        FindObjectOfType<AudioManager>().Play("Hurt");
        Shake.ShakeCam();
        Gstats.UpdateText();
    }

}