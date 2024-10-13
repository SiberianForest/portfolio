using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BaseGetApi : MonoBehaviour
{
    protected abstract string Uri { get; }
    protected abstract void HandleResponse(string jsonResponse);

    // GETリクエストを送信するメソッド
    protected IEnumerator SendGetRequest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(Uri))
        {
            yield return request.SendWebRequest();

            if (UnityWebRequest.Result.ConnectionError == request.result ||
                UnityWebRequest.Result.ProtocolError == request.result)
            {
                Debug.Log("GET Request Failure: " + request.error);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                HandleResponse(request.downloadHandler.text);
            }
        }
    }
}