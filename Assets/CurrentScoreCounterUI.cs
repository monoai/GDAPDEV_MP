using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CurrentScoreCounterUI : MonoBehaviour
{
    private Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = "Current Score: " + DataManager.data.Score.ToString("D5");
    }
}
