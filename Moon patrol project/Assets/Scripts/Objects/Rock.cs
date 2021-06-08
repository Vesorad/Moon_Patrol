using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    //config params
    [SerializeField] [Range(1, 3)] int maxHits = 1;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] int scoreValue = 100;

    [SerializeField] int timesHit = 0;
    [SerializeField] Sprite rockDouble;

    bool stop = true;
    AudioManager audioManager;
    PlayerDeath playerDeath;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        playerDeath = FindObjectOfType<PlayerDeath>();
    }
    private void Update()
    {
        if (playerDeath.die == true)
        {
            switch (timesHit)
            {
                case 1:
                    if (rockDouble != null)
                    {
                        StartCoroutine(ResetHalfRock());
                    }
                    else
                   StartCoroutine(StartRock());
                    break;
                case 2:
                    StartCoroutine(ResetAllDobuleRock());
                    break;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "LaserRight")
        {
            stop = true;
            if (gameObject.tag == "Hole")
            {
                //Debug.Log("kolizja dziura");
            }
            else
            {             
                //Debug.Log(collision.gameObject.name);
                HandelHit();
            }

        }
    }

  
    private void HandelHit()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        timesHit++;
        audioManager.Play("Rock");
        if (timesHit >= maxHits)
        {

                // ResetAllDobuleRock();
                 HideRock();

       
        }
        if (timesHit < maxHits)
        {
            ShowNextHitSprite();
            
        }
    }

    private void ShowNextHitSprite()
    {
       
        if (hitSprites != null)
        {
            int spriteIndex = timesHit - 1;
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
            Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
            var collider = this.gameObject.AddComponent<PolygonCollider2D>();
            collider.isTrigger = true;
        }
        else
        {
            // Debug.Log("Zjebało się sprawdz:" + gameObject.name);
        }
    }

    public int GetHitTimes()
    {
        
        return timesHit;
    }
    IEnumerator StartRock()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        timesHit = 0;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void HideRock()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

    }
    IEnumerator ResetHalfRock()
    {
        timesHit = 0;
        yield return new WaitForSeconds(0.65f);
        Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
        GetComponent<SpriteRenderer>().sprite = rockDouble;       
        var collider = this.gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }
    IEnumerator ResetAllDobuleRock()
    {
        yield return new WaitForSeconds(0.65f);
       
        GetComponent<SpriteRenderer>().sprite = rockDouble;
        if (stop == true)
        {
            Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
            var collider = this.gameObject.AddComponent<PolygonCollider2D>();
            collider.isTrigger = true;
            stop = false;
        }
        

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        timesHit = 0;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
