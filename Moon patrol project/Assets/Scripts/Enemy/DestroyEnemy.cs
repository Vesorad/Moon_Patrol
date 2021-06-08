using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int scoreValue = 100;
    [SerializeField] GameObject bum;
    Ground ground;

    AudioManager audioManager;
    private void Start()
    {
        ground = FindObjectOfType<Ground>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaserUp")
        {
            Die();
        }
        if (collision.gameObject.tag == "Ground")
        {
            audioManager.Play("EnemyDead");
            Instantiate(bum, transform.position, Quaternion.Euler(0, 0, 0), ground.transform.parent);
            Destroy(gameObject.GetComponent<BoxCollider2D>());

        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        animator.SetBool("Enemy_Boom", true);
        audioManager.Play("EnemyDead");
        Destroy(gameObject, 0.3f);
    }
}
