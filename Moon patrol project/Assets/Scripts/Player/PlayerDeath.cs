using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    AudioManager audioManager;
    public Animator animator;
    [SerializeField] public bool die;
    PlayerMovement movement;
    LifeCounter lifeCounter;
    Ground ground;
    FallInHole inHole;
    GameSession gameSession;
    GameTimer gameTimer;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameTimer = FindObjectOfType<GameTimer>();
        gameSession = FindObjectOfType<GameSession>();
        inHole = GetComponent<FallInHole>();
        movement = GetComponent<PlayerMovement>();
        lifeCounter = FindObjectOfType<LifeCounter>();
        ground = FindObjectOfType<Ground>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Player")
        {
            if (!die)
                if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Object")
                    Die();

            if (!die)
                if (collision.gameObject.tag == "Hole")
                {
                    DieHoleFront();
                }
            if (!die)
            {
                if (collision.gameObject.tag == "LaserEnemy" || collision.gameObject.tag == "Enemy")
                {
                    Die();
                }
            }
        }
    }

    private void DieHoleFront()
    {
        audioManager.Stop("Wave");
        audioManager.Play("Dead");
        die = true;
        ground.actualSpeed = 0;
        lifeCounter.ChangeLifeCounter();
        animator.SetTrigger("Hole");
    }

    private void Die()
    {
        audioManager.Stop("Wave");
        audioManager.Play("Dead");
        die = true;
        ground.actualSpeed = 0;
        lifeCounter.ChangeLifeCounter();
        animator.SetBool("KillPlayer", true);
    }
}
