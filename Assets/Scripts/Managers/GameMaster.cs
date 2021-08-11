using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;

    void Awake() {
        //screenSize = new Vector3(Screen.height, Screen.width, 0);
        if(gm == null) {
            gm = this;
        }
    }

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives {
        get { return _remainingLives; }
    }

    [SerializeField]
    private int startingMoney;
    public static int Money;

    public Transform playerPrefab;
    public int spawnDelay;

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

    void Start() {
        _remainingLives = maxLives;
        Money = startingMoney;
    }

    public void EndGame()
    {
        //Anything Game Over related here
        //GameOver UI here
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
            gm.EndGame();
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
