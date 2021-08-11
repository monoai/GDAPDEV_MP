using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth = 100;

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

    public float maxFireRate = 0.5f;
    public float fireRate;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

}
