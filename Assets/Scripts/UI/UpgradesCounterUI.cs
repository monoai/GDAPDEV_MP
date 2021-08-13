using UnityEngine;
using UnityEngine.UI;

public class UpgradesCounterUI : MonoBehaviour
{
    [Header("Lives Upgrades")]
    public Text currLivesText;

    [Header("Weapon Upgrades")]
    public Text currDmgText;
    public Text currFireRateText;

    void Awake()
    {
        currLivesText = GetComponent<Text>();
        currDmgText = GetComponent<Text>();
        currFireRateText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currLivesText.text = "Current Lives: " + DataManager.data.maxLives.ToString("D2");
        currLivesText.text = "Current Damage: " + DataManager.data.maxDamage.ToString("D2");
        currLivesText.text = "Current Fire Rate: " + DataManager.data.maxFireRate.ToString("");
    }
}
