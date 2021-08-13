using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting, BossBattle, Finished };

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float timeInterval = 1f;

    //Should be, or can be private
    public SpawnState state = SpawnState.Counting;

    private GameMaster gm;

    void Start() {
        if(spawnPoints.Length == 0 ) {
            Debug.LogError("No Spawn Points, please add one.");
        }

        waveCountdown = timeBetweenWaves;
        gm = GetComponent<GameMaster>();
    }

    void Update() {
        if(state == SpawnState.Finished) {
            gm.gameEnd();
            return;
        }
        if(state == SpawnState.Waiting) {
            if(!enemyStillAlive()) {
                Debug.Log("Wave Completed");
                waveCompleted();
                return;
            } else {
                return;
            }
        }

        if(waveCountdown <= 0) {
            if(state != SpawnState.Spawning) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    void waveCompleted() {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("Completed all waves");
            state = SpawnState.Finished;
        } else {
            nextWave++;
        }
    }

    bool enemyStillAlive() {
        timeInterval -= Time.deltaTime;
        if(timeInterval <= 0f) {
            timeInterval = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.Spawning;

        for(int i = 0; i < _wave.count; i++) {
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void spawnEnemy(Transform _enemy) {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Transform newSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, newSpawn.position, newSpawn.rotation);
    }

}
