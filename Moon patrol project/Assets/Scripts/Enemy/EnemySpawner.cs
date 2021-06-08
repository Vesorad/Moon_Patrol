using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfig;


    [SerializeField] List<GameObject> startPoints;
    [SerializeField] List<GameObject> realEnemyOnScean;
    [SerializeField] public bool kamikazeStart;



    public void SpawnWave(int i)
    {
        var currentWave = waveConfig[i];
        StartCoroutine(SpawnAllEnemisInWave(currentWave, i));

    }

    public IEnumerator SpawnAllEnemisInWave(WaveConfig waveConfig, int i)
    {
        var AllWave = Instantiate(realEnemyOnScean[i], transform.position, transform.rotation);
        AllWave.AddComponent<DestroyHole>();

        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemis(); enemyCount++)
        {
            //Debug.Log("Spawn wroga: " + enemyCount + "z fali: " + waveConfig + " ze spawna: " + i + "przypisany do :" + realEnemyOnScean[i]);
            SpawnNormalEnemy(waveConfig, enemyCount, AllWave);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }

    private void SpawnNormalEnemy(WaveConfig waveConfig, int enemyCount, GameObject AllWave)
    {
        SpawnEnemy(waveConfig, enemyCount, AllWave);
    }

    private void SpawnEnemy(WaveConfig waveConfig, int enemyCount, GameObject AllWave)
    {
        var newEnemy = Instantiate(waveConfig.GetEnemyPreFab(), waveConfig.GetWyePoints()[0].transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
        if (enemyCount >= waveConfig.GetNumberOfEnemis() - 1)
        {
            Destroy(newEnemy.GetComponent<EnemyShot>());
            if (waveConfig.GetKamikazeOn() == true)
            {

                newEnemy.GetComponent<EnemyPath>().kamikaze = true;

            }
        }
        if (enemyCount > 1)
        {
            Destroy(newEnemy.GetComponent<EnemyShot>());
        }
        newEnemy.transform.parent = AllWave.transform;

    }
}
