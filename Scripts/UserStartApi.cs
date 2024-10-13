using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserStartApi : BasePostApi
{
    [SerializeField] private GameObject footer;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private FooterText footerText;
    private bool started = false;
    protected override string Uri => GameConfig.baseUrl + "/game/user/start";

    protected override string JsonData => JsonUtility.ToJson(null);
    

    protected override void HandleResponse(string jsonResponse)
    {
        started = true;
    }
    
    protected override void Comment()
    {
        var text = "ログインに失敗しました。正しいIDとパスワードを入力してください。";
        footer.SetActive(true);
        footerText.WriteFooterText(text, 16);
        loginButton.GetComponent<Button>().interactable = true;
        started = false;
    }
    
    public IEnumerator Run()
    {
        yield return SendPostRequest();
    }
    
    public bool GetStarted()
    {
        return started;
    }
}