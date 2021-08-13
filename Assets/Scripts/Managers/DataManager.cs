using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager data;

    [SerializeField]
    [Header("Player Resources")]
    public int maxLives = 3;
    public int maxHealth = 100;
    public int Money;

    [Header("Player Stats")]
    public float maxFireRate = 0.5f;
    public int maxDamage = 10;

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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
