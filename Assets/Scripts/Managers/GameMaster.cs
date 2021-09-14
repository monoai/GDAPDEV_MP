using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;

    void Awake()
    {
        //screenSize = new Vector3(Screen.height, Screen.width, 0);
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
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

    [SerializeField]
    [Header("Player Resources")]
    private static int _remainingLives;
    public static int RemainingLives
    {
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

    //Other things to load
    private WaveSpawner waveSpawn;

    private int currWeapon = 1;

    private Weapon weapon;

    float nextTimeToSearch = 0;

    //Control Type variables
    [HideInInspector] public enum ControlTypes { A, B };
    [HideInInspector] public ControlTypes controlType = ControlTypes.A;
    public GameObject JoystickObject;

    void Start()
    {
        applySettings();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        waveSpawn = GetComponent<WaveSpawner>();
        _remainingLives = DataManager.data.maxLives;
        Score = startingScore;
    }

    void Update()
    {
        timeInterval -= Time.deltaTime;
        if (timeInterval <= 0f)
        {
            timeInterval = 1f;
            if (validScene())
            {
                waveSpawn.enabled = true;
            }
            else
            {
                waveSpawn.enabled = false;
            }
        }
    }

    void applySettings()
    {
        //[Apply ControlType setting]
        //controlType = (ControlTypes)PlayerPrefs.GetInt("ControlType", 0);
        controlType = (ControlTypes)1;
        //Debug.Log("Control type is: " + controlType);
        GestureManager.Instance.controlType = controlType;
        if (controlType == ControlTypes.A)
        {
            JoystickObject.SetActive(true);
        }
        else
        {
            JoystickObject.SetActive(false);
        }
    }

    bool validScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Lobby Scene" && scene.name != "Main Menu")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void gameOver()
    {
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
    }

    public void gameEnd()
    {
        Debug.Log("Game ended!");
        DataManager.data.Money += 3;
        gameWonUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        //Instantiate(playerPrefab, (screenSize - spawnPos), rotation);
        var newPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.25f, 0.0f));
        Instantiate(playerPrefab, newPos, Quaternion.identity);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.gameOver();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    public void changeWeapon(int choice)
    {

        currWeapon = currWeapon + choice;
        if (currWeapon > 3)
        {
            currWeapon = 1;
        }
        else if (currWeapon < 1)
        {
            currWeapon = 3;
        }
        Debug.Log(currWeapon);
        switch (currWeapon)
        {
            case 1:
                weapon.currWeapon = weapon.bulletPrefab;
                weapon.weaponType = Weapon.weaponTypeEnum.Red;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
            case 2:
                weapon.currWeapon = null;
                weapon.weaponType = Weapon.weaponTypeEnum.Blue;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
            case 3:
                weapon.currWeapon = weapon.wavePrefab;
                weapon.weaponType = Weapon.weaponTypeEnum.Yellow;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
        }
    }

    public void testSpawn(Vector3 position)
    {
        var viewPos = Camera.main.ViewportToWorldPoint(position);

        Debug.Log("Spawning at: " + viewPos);
    }

    public GameObject findPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                return searchResult;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }

        Debug.Log("Cant find player!");
        return null;
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
