using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class LeaderboardHandler : MonoBehaviour
{
    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    public GameObject score;

    void Awake()
    {
        StartCoroutine(GetPlayers());
    }

    public void _postScore(InputField receivedname)
    {
        Debug.Log("PostingScore");
        StartCoroutine(PostScore(receivedname.text));
    }

    IEnumerator GetPlayers()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/11", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Got Players: {request.downloadHandler.text}");
            List<
                Dictionary<string, string>
                > playerListRaw = JsonConvert.DeserializeObject<
                List<
                    Dictionary<string, string>
                    >
                >(request.downloadHandler.text);
            foreach (Dictionary<string, string> player in playerListRaw)
            {
                Debug.Log($"Got player: {player["user_name"]}");
                var scoreTxt = Instantiate(score, this.transform);
                var scoreValue = scoreTxt.GetComponent<LeaderboardUIHandler>();
                scoreValue.nameTxt = player["user_name"];
                scoreValue.scoreTxt = player["score"];
            }
        }
        else
        {
            Debug.Log($"Error: {request.error}");
            var scoreTxt = Instantiate(score, this.transform);
            var scoreValue = scoreTxt.GetComponent<LeaderboardUIHandler>();
            scoreValue.nameTxt = "404 ERROR: ";
            scoreValue.scoreTxt = "Leaderboards not received";
        }
    }

    IEnumerator PostScore(string user_name)
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();


        PlayerParams.Add("group_num", "11");
        PlayerParams.Add("user_name", user_name);
        PlayerParams.Add("score", DataManager.data.Score.ToString());

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
