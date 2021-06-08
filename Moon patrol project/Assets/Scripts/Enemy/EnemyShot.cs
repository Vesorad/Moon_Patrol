using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [HideInInspector]public int chosenBox;

    [SerializeField] bool destroyShoot;

    [Header("Statystki")] 
    [SerializeField] float timeBettwenShot = 2f;

    GameObject laser;


    [Header("Objekty")]
    [SerializeField] GameObject Laser;

    float shotCounter;
    int numberGameSession;
    void Start()
    {
        shotCounter = timeBettwenShot;
        chosenBox = 0;
    }
    void Update()
    {
        //numberGameSession = FindObjectsOfType<EnemyLaser>().Length;
        if (destroyShoot)
        {
            WaitAndShoot();      
        }
        else
        {
            CountDownAndShoot();
        }
    }
    void WaitAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (laser == null)
        {
            if (numberGameSession < 1)
            {
                laser = Instantiate(Laser, transform.position, Quaternion.Euler(0, 0, -90)) as GameObject;
            }
        }
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            laser = Instantiate(Laser, transform.position, Quaternion.Euler(0, 0, -90)) as GameObject;
            shotCounter = timeBettwenShot;
        }
    }
}
