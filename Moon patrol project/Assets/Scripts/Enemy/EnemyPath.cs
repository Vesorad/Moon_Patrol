using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfig waveConfig;
    EnemySpawner enemySpawner;
    [SerializeField] List<Transform> wayPoint;
    int wayPointIndex = 0;

    [Header("DANE DO WSTAWIENIA RECZNIE")]
    [SerializeField] List<Transform> wayLastPoint;
    [SerializeField] List<Transform> KamikazeLastPoint;

    [SerializeField] public bool kamikaze;

    //DANE ODPOWIADAJĄCE ZA OSATNI PUNKT
    int TimeToLastPath = 10;
    float realTime;
    int random;
    int kamikazeLast;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        wayPoint = waveConfig.GetWyePoints();
        transform.position = wayPoint[wayPointIndex].transform.position;

        random = UnityEngine.Random.Range(0, wayLastPoint.Count);
        kamikazeLast = UnityEngine.Random.Range(0, KamikazeLastPoint.Count);
    }
    void Update()
    {
        realTime += Time.deltaTime;
        MoveTowards();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    public void MoveTowards()
    {
        if (wayPointIndex <= wayPoint.Count - 1)
        {
            Move();
        }

    }

    private void Move()
    {
        if (realTime <= TimeToLastPath) 
        {
            var targetPos = wayPoint[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {

            if (kamikaze == true) // ostatnia droga kamikaze
            {
                Destroy(this.GetComponent<AnimContr>(),4);
                var targetPos = KamikazeLastPoint[kamikazeLast].transform.position;
                var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            }
            else // ostatnia droga normalnego wroga
            {
                Destroy(this.GetComponent<EnemyShot>());
                Destroy(this.GetComponent<AnimContr>(),0.5f);
                var targetPos = wayLastPoint[random].transform.position;
                var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            }
        }
       
    }

}
