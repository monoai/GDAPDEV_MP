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
    public Transform leaderboards;
    public Text postResult;

    public void _deleteScores()
    {
        foreach (Transform scores in leaderboards)
        {
            Destroy(scores.gameObject);
        }
    }

    public void _getScore()
    {
        StartCoroutine(GetPlayers());
    }

    public void _postScore(InputField receivedname)
    {
        if (DataManager.data.gameObject.activeInHierarchy == true)
        {
            Debug.Log("PostingScore");
            StartCoroutine(PostScore(receivedname.text));
        }
        else
        {
            Debug.Log("Score failed to post, where's the data?!");
        }
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
                var scoreTxt = Instantiate(score, leaderboards);
                var scoreValue = scoreTxt.GetComponent<LeaderboardUIHandler>();
                scoreValue.nameTxt = player["user_name"];
                scoreValue.scoreTxt = player["score"];
            }
        }
        else
        {
            Debug.Log($"Error: {request.error}");
            var scoreTxt = Instantiate(score, leaderboards);
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
            postResult.text = "Score has been submitted successfully!";
            postResult.color = Color.green;

            yield return new WaitForSeconds(2);

            postResult.text = "";
        }
        else
        {
            Debug.Log($"Error: {request.error}");
            postResult.text = "Score failed to submit, no internet connection detected";
            postResult.color = Color.red;

            yield return new WaitForSeconds(2);

            postResult.text = "";
        }
    }

    void Awake()
    {

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
