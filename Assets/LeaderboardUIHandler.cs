using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class LeaderboardUIHandler : MonoBehaviour
{
    public Text txtName;
    public Text txtScore;

    public string nameTxt;
    public string scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        //txtName = GetComponent<Text>();
        //txtScore = GetComponent<Text>();

        txtName.text = nameTxt;
        txtScore.text = scoreTxt;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
