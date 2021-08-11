using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
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
        //screenSize = new Vector3(Screen.height, Screen.width, 0);

        if(gm == null) {
            gm = this;
        }
    }

    public IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(spawnDelay);
        //Instantiate(playerPrefab, (screenSize - spawnPos), rotation);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer (PlayerMovement player) {
        Destroy (player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}
