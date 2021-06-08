using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] public bool startWave = false;
    [SerializeField] public int numberOfWave = 0;


    PlayerDeath playerDeath;
    EnemySpawner enemySpawner;
    private void Start()
    {
        playerDeath = FindObjectOfType<PlayerDeath>();
        enemySpawner = FindObjectOfType<EnemySpawner>();

    }
    private void Update()
    {
        if (playerDeath.die == true)
        {
            startWave = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemySpawner.SpawnWave(numberOfWave);
            startWave = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            startWave = false;

        }
    }

}
