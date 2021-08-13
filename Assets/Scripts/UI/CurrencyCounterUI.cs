using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrencyCounterUI : MonoBehaviour
{
    private Text scoreText;

    void Awake() {
        scoreText = GetComponent<Text>();
    }

    void Update() {
        scoreText.text = "Currency: " + DataManager.data.Money.ToString("D2");
    }
}
