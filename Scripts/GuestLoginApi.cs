using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PostLoginUserData
{
    public string userId;
    public string password;
}
public class GuestLoginApi : BasePostApi
{
    [SerializeField] private GameObject footer;
    [SerializeField] private FooterText footerText;
    private string userIdGuestLogin;
    private string passwordGuestLogin;
    protected override string Uri => GameConfig.baseUrl + "/auth/user/guest/login";
    
    protected override string JsonData => JsonUtility.ToJson(new PostLoginUserData
    {
        userId = userIdGuestLogin,
        password = passwordGuestLogin
    });

    protected override void HandleResponse(string jsonResponse)
    {
        return;
    }
    
    protected override void Comment()
    {
        var text = "通信エラーが発生しました。\n 時間をおいて再度お試しください。";
        footer.SetActive(true);
        footerText.WriteFooterText(text, 16);
    }
    
    public IEnumerator Run(string userId, string password)
    {
        this.userIdGuestLogin = userId;
        this.passwordGuestLogin = password;
        yield return SendPostRequest();
    }
}