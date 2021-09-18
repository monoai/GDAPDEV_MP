using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField]
    private GameObject bgImage;
    [Range(-1f, 1f)]
    public float bgScrollSpeed = 0.5f;
    private float bgOffset;
    private Material bgMat;

    public enum levelLocked { None, Lvl2, Lvl3, Lvl4 };
    [Header("Current Level Settings")]
    public levelLocked levelToUnlock = levelLocked.None;
    public int tokenReward = 3;

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
        GameObject.Find("NotifsManager").GetComponent<NotificationsManager>().SendSimpleNotif();
        applySettings();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
        waveSpawn = GetComponent<WaveSpawner>();
        _remainingLives = DataManager.data.maxLives;
        startingScore = DataManager.data.Score;
        Score = startingScore;
        bgMat = bgImage.GetComponent<Image>().material;
    }

    void Update()
    {
        if (bgMat == null)
        {
            bgMat = bgImage.GetComponent<Image>().material;
        }
        bgOffset += (Time.deltaTime * bgScrollSpeed) / 10f;
        bgMat.SetTextureOffset("_MainTex", new Vector2(0, bgOffset));
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void applySettings()
    {
        //[Apply ControlType setting]

        // The default value (2nd param) just assigns a value just in case there is no saved playerpref value
        // i.e. For the very first run after playerprefs implementation. Default value is useless after that
        controlType = (ControlTypes)PlayerPrefs.GetInt("ControlType", 1);

        //controlType = (ControlTypes)1;
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
        //FindObjectOfType<AudioManager>().Play("Game_SFX_GameOver");
        AudioManager.instance.Play("Game_SFX_GameOver");
        Debug.Log("Game Over!");

        if (DataManager.data.isAdsEnabled)
            FindObjectOfType<AdsManager>().ShowRewardedAd();
        //GameObject.Find("AdsManager").GetComponent<AdsManager>().ShowRewardedAd();

        gameOverUI.SetActive(true);
    }

    public void gameEnd()
    {
        //FindObjectOfType<AudioManager>().Play("Game_SFX_GameWin");
        AudioManager.instance.Play("Game_SFX_GameWin");
        Debug.Log("Game ended!");
        DataManager.data.Money += tokenReward;
        DataManager.data.Score += Score;
        switch (levelToUnlock)
        {
            case levelLocked.Lvl2:
                DataManager.data.Lvl2unlock = true;
                break;
            case levelLocked.Lvl3:
                DataManager.data.Lvl3unlock = true;
                break;
            case levelLocked.Lvl4:
                DataManager.data.Lvl4unlock = true;
                break;
            default:
                Debug.Log("No Level got unlocked");
                break;
        }

        if (DataManager.data.isAdsEnabled)
            FindObjectOfType<AdsManager>().ShowInterstitialAd();
        //GameObject.Find("AdsManager").GetComponent<AdsManager>().ShowInterstitialAd();

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
        //FindObjectOfType<AudioManager>().Play("Game_SFX_PlayerDeath");
        AudioManager.instance.Play("Game_SFX_PlayerDeath");
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
