using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth = DataManager.data.maxHealth;

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

    public float maxFireRate = DataManager.data.maxFireRate;
    public float fireRate;

    public int maxDamage = DataManager.data.maxDamage;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

}
