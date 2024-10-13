using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BasePostApi : MonoBehaviour
{
    protected abstract string Uri { get; }
    protected abstract string JsonData { get; }
    protected abstract void HandleResponse(string jsonResponse);
    
    protected abstract void Comment();

    // GETリクエストを送信するメソッド
    protected IEnumerator SendPostRequest()
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(Uri, JsonData))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(JsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();

            if (UnityWebRequest.Result.ConnectionError == request.result ||
                UnityWebRequest.Result.ProtocolError == request.result)
            {
                Debug.Log(request.error);
                Comment();
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                HandleResponse(request.downloadHandler.text);
            }
        }
    }
}