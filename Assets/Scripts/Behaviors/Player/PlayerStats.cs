using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth;

    private int _curHealth;
    public int curHealth
    {
        get {
            return _curHealth;
        }
        set {
            _curHealth = Mathf.Clamp(value, 0 , maxHealth);
        }
    }

    public float maxFireRate;
    public float fireRate;

    public int maxDamage;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        maxHealth = DataManager.data.maxHealth;
        maxFireRate = DataManager.data.maxFireRate;
        maxDamage = DataManager.data.maxDamage;
    }

}
