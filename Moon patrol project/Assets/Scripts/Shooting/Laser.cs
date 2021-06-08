using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    AudioManager audioManager;
    GameManager gameManager;


    int laserUpNumber;
    int laserRightNumber;
    bool reloadUp = false;
    bool reloadRight = false;

    [Header("LaserUp")]
    [SerializeField] GameObject LaserUp;
    [SerializeField] float laserUpSpeed;
    [SerializeField] float reloadLaserUp;
    [SerializeField] float LimitOnLasersUp =4;
    [SerializeField] GameObject PosShootUp;

    [Header("LaserRight")]
    [SerializeField] GameObject LaserRight;
    [SerializeField] float laserRightSpeed;
    [SerializeField] float reloadLaserRight;
    [SerializeField] float LimitOnLasersRight=1;
    [SerializeField] GameObject PosShootRight;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        Fire();
        laserUpNumber = GameObject.FindGameObjectsWithTag("LaserUp").Length;
        laserRightNumber = GameObject.FindGameObjectsWithTag("LaserRight").Length;
    }
    private void Fire()
    {
        if (Input.GetKeyDown(gameManager.fire))
        {
            if (laserRightNumber < LimitOnLasersRight)
            {
                if (reloadRight == false)
                {
                    audioManager.Play("Shoot");
                    FireCorutineRight();
                    reloadRight = true;
                    StartCoroutine(WaitForReloadRight());
                }
            }
            if (laserUpNumber < LimitOnLasersUp)
            {
                if (reloadUp == false)
                {
                    audioManager.Play("Shoot");
                    FireCorutineUp();
                    reloadUp = true;
                    StartCoroutine(WaitForReloadUp());
                }
            }             
        }
    }
    public void FireCorutineUp()
    {  
            GameObject laser = Instantiate(LaserUp, PosShootUp.transform.position, Quaternion.Euler(0,0,90));
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserUpSpeed);         
    }
    public void FireCorutineRight()
    {   
            GameObject laser = Instantiate(LaserRight, PosShootRight.transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserRightSpeed, 0);
              
    }
    IEnumerator WaitForReloadUp()
    {       
        yield return new WaitForSeconds(reloadLaserUp);
        reloadUp = false;
    }
    IEnumerator WaitForReloadRight()
    {
        yield return new WaitForSeconds(reloadLaserRight);
        reloadRight = false;
    }
}
