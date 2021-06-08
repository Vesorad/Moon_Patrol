using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLaser : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] GameObject rightLaserBum;
    [SerializeField] GameObject upLaserBum;

    Ground ground;

    float realTime = 0.0f;

    private void Start()
    {
        ground = FindObjectOfType<Ground>();
    }
    void Update()
    {
        realTime += Time.deltaTime;
        if (realTime >= timeToDestroy)
        {
            Die();
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "LaserRight")
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Rock")
            {
                Instantiate(rightLaserBum, transform.position, Quaternion.identity, ground.gameObject.transform);
                Die();
            }
        }
        if (collision.gameObject.tag =="DestroyLaser")
        {
            this.GetComponent<BoxCollider2D>();
        }
        if (gameObject.tag == "LaserUp")
        {
            if (collision.gameObject.tag == "LaserEnemy")
            {
                Instantiate(upLaserBum, transform.position, Quaternion.identity, ground.gameObject.transform);
                Die();
            }
            if (collision.gameObject.tag == "Enemy")
            {
                Die();
            }
        }     
    }

    private void Die()
    {
        if (gameObject.tag == "LaserRight")
        { Instantiate(rightLaserBum, transform.position, Quaternion.identity, ground.gameObject.transform); 
        }

            Destroy(gameObject);
    }
}
