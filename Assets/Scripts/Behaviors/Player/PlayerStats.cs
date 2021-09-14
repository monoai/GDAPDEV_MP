using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth;

    private int _curHealth;
    public int curHealth
    {
        get
        {
            return _curHealth;
        }
        set
        {
            _curHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public float maxFireRate;
    public float fireRate;

    public int maxDamage;

    //Should only be from 0.4f to 2.0f
    //Replace with something more elegant once the settings can refine the movement speed.
    //Implement with range steps of 25.0f?
    [Range(0.4f, 2.0f)]
    public float moveSpeedPercent;
    public float verticalCompensator;
    public float joystickCompensator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        maxHealth = DataManager.data.maxHealth;
        maxFireRate = DataManager.data.maxFireRate;
        maxDamage = DataManager.data.maxDamage;
    }

}
