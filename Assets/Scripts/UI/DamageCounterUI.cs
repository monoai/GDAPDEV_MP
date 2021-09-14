using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DamageCounterUI : MonoBehaviour
{
    private Text dmgText;

    void Awake() {
        dmgText = GetComponent<Text>();
    }

    void Update() {
        dmgText.text = "Damage: " + DataManager.data.maxDamage.ToString("D2");
    }
}
