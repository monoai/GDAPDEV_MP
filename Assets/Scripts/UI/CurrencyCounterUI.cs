using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrencyCounterUI : MonoBehaviour
{
    private Text currencyText;

    void Awake()
    {
        currencyText = GetComponent<Text>();
    }

    void Update()
    {
        currencyText.text = "Tokens: " + DataManager.data.Money.ToString("D2");
    }
}
