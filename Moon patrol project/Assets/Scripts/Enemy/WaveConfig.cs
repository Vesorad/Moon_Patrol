using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Objekty")]
    [SerializeField] GameObject enemyPreFab;
    [SerializeField] List<GameObject> pathPreFab;

    [Header("Statystki")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] int numberOfEnemis = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool kamikazeOn;
    int random;

    public GameObject GetEnemyPreFab()
    {
        return enemyPreFab;
    }
    public List<Transform> GetWyePoints()
    {
        random = Random.Range(0, pathPreFab.Count);
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPreFab[random].transform)
        {
            waveWayPoints.Add(child);
        }

        return waveWayPoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public int GetNumberOfEnemis()
    {
        return numberOfEnemis;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public bool GetKamikazeOn()
    {
        return kamikazeOn;
    }
}
