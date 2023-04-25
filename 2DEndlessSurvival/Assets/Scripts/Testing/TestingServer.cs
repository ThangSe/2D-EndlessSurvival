using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class TestingServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWebData("localhost:5600/account/all-resident"));
    }

    void ProcessServerResponse (string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        Debug.Log(node);
    }

    IEnumerator GetWebData(string address)
    {
        UnityWebRequest www = UnityWebRequest.Get(address);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong " + www.error);
        } else
        {
            ProcessServerResponse(www.downloadHandler.text);
        }
    }
}
