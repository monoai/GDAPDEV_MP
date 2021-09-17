using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager data;

    GameObject settingsManager;

    [SerializeField]
    [Header("Player Resources")]
    public int maxLives = 3;
    public int maxHealth = 100;
    public int Money;

    [Header("Player Stats")]
    public float maxFireRate = 0.5f;
    public int maxDamage = 10;

    public float MoveSpeed = 1;
    public bool isAdsEnabled;

    void Awake() {
        if(data == null) {
            data = this;
        } else if (data != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        settingsManager = GameObject.Find("SettingsManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MoveSpeed = settingsManager.GetComponent<SettingsManager>().GetMoveSpeed();
            isAdsEnabled = settingsManager.GetComponent<SettingsManager>().AdsIsEnabled;
        }
    }
}
