using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LastPoint : MonoBehaviour
{
    WaveConfig waveConfig;
    [SerializeField] List<Transform> wayLastPoint;
    [SerializeField] List<Transform> KamikazeLastPoint;
    [SerializeField] int timeToDestoryObject = 15;

    int numberOfEnemy;
    int random;
    int kamikazeLast;

    void Start()
    {
        numberOfEnemy = FindObjectsOfType<DestroyEnemy>().Length;
        random = Random.Range(0, wayLastPoint.Count);
        kamikazeLast = Random.Range(0, KamikazeLastPoint.Count);
       
    }
    private void Update()
    {
        if (waveConfig.GetKamikazeOn() == true)
        {
            if (numberOfEnemy < waveConfig.GetNumberOfEnemis())
            {
                Move();
            }
            else
            {
                MoveKamikaze();
            }
        }
        else
        {
            Move();
        }
    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void MoveKamikaze()
    {
        StartCoroutine(DestoryObject());
        var targetPos = KamikazeLastPoint[kamikazeLast].transform.position;
        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);  
    }

    private void Move()
    {
        StartCoroutine(DestoryObject());
        var targetPos = wayLastPoint[random].transform.position;
        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
    }
    IEnumerator DestoryObject()
    {
        yield return new WaitForSeconds(timeToDestoryObject);
        Destroy(gameObject);
    }
  
}
