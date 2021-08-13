using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;

    void Awake() {
        //screenSize = new Vector3(Screen.height, Screen.width, 0);
        if(gm == null) {
            gm = this;
        } else if (gm != this) {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    [Header("Player Prefab")]
    public Transform playerPrefab;

    [Header("Spawn Information")]
    public int spawnDelay;
    //-----------------
    public Transform spawnPoint;
    /*
    This is a temporary variable
    Better approach should probably be calculating the screen's DPI
    then getting the position from there to make it adaptable
    instead of uncertainly just dropping the point somewhere
    which wouldn't adapt on multiple screen resolutions
    */

    //-----Work-in-progress Screen Position calculation;
    //public Vector3 screenSize;
    //public Vector3 spawnPos;
    //public Quaternion rotation;
    //-----------------

    [SerializeField]
    [Header("Player Resources")]
    private static int _remainingLives;
    public static int RemainingLives {
        get { return _remainingLives; }
    }

    [SerializeField]
    private int startingScore;
    public static int Score;

    [Header("UI Control")]
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject gameWonUI;

    //For counting purposes
    private float timeInterval = 1f;

    private WaveSpawner waveSpawn;

    void Start() {
        waveSpawn = GetComponent<WaveSpawner>();
        _remainingLives = DataManager.data.maxLives;
        Score = startingScore;
    }

    void Update() {
        timeInterval -= Time.deltaTime;
        if(timeInterval <= 0f) {
            timeInterval = 1f;
            if(validScene()) {
                waveSpawn.enabled = true;
            } else {
                waveSpawn.enabled = false;
            }
        }
    }

    bool validScene() {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != "Lobby Scene" && scene.name != "Main Menu") {
            return true;
        } else {
            return false;
        }
    }

    public void gameOver()
    {
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
    }

    public void gameEnd(){
        Debug.Log("Game ended!");
        DataManager.data.Money += 3;
        gameWonUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(spawnDelay);
        //Instantiate(playerPrefab, (screenSize - spawnPos), rotation);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer (Player player) {
        Destroy (player.gameObject);
        _remainingLives -= 1;
        if(_remainingLives <= 0) {
            gm.gameOver();
        } else {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    /* If we ever need to kill enemies through the GM instead
    public static void killEnemy(Enemy enemy) {
        gm.killEnemyActual(enemy);
    }

    public void killEnemyActual(Enemy _enemy) {
        Destroy(_enemy.gameObject);
    }
    */
}
