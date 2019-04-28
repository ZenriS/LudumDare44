using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mananager_Script : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    private Transform[] _spawnPoints;
    public int[] StaringWaves;
    public float SpawnRate;
    private int waveNr;
    public GameMananger_Script GameManger;

	void Start ()
	{
	    _spawnPoints = transform.GetChild(0).GetComponentsInChildren<Transform>();
	    Invoke("SpawnEnemies", (SpawnRate * 2));
	}

    void SpawnEnemies()
    {
        if (!GameManger.isGameOver)
        {
            int a = 0;
            if (waveNr <= StaringWaves.Length)
            {
                a = StaringWaves[waveNr];
            }
            else
            {
                a = Random.Range(5, 10);
                a += waveNr - StaringWaves.Length;
            }

            for (int i = 0; i < a; i++)
            {
                int s = Random.Range(1, _spawnPoints.Length);
                int e = Random.Range(0, EnemyPrefab.Length);
                Instantiate(EnemyPrefab[e], _spawnPoints[s].position, Quaternion.identity);
            }

            float t = Random.Range(SpawnRate, (SpawnRate + 5));
            Invoke("SpawnEnemies", t);
        }
    }
}
