using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class WebHandlerScript : MonoBehaviour
{
    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    public void CreatePlayer()
    {
        Debug.Log("Creating Player");
        StartCoroutine(SamplePostRoutine());
    }

    public void GetPlayer()
    {
        Debug.Log("Getting player");
        StartCoroutine(SampleGetPlayerRoutine());
    }

    public void GetPlayers()
    {
        Debug.Log("Getting players");
        StartCoroutine(SampleGetPlayersRoutine());
    }

    public void EditPlayer()
    {
        Debug.Log("Editing");
        StartCoroutine(SampleEditPlayerRoutine());
    }

    public void DeletePlayer()
    {
        Debug.Log("Deleting");
        StartCoroutine(SampleDeletePlayerRoutine());
    }

    IEnumerator SampleDeletePlayerRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/11", "DELETE");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Deleted Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleEditPlayerRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("name", "Mono K. Zetsubou");

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/24", "PUT");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Updated Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }
    }

    IEnumerator SampleGetPlayerRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/11", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            Dictionary<string, string> player = JsonConvert.DeserializeObject<
                Dictionary<string, string>>(request.downloadHandler.text);
            Debug.Log($"Got player: {player["user_name"]}");
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }

    }

    IEnumerator SampleGetPlayersRoutine()
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
            }
        }
        else
        {
            Debug.Log($"Error: {request.error}");
        }

    }

    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("group_num", "11");
        PlayerParams.Add("user_name", "Test Player Mukuro");
        PlayerParams.Add("score", "111");

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
}
