using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestCreateApi : BasePostApi
{
    [SerializeField] private GameObject footer;
    [SerializeField] private FooterText footerText;
    private string userIdGuestLogin;
    private string passwordGuestLogin;
    protected override string Uri => GameConfig.baseUrl + "/auth/user/guest/create";
    
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
        //ID重複のときにエラー
        var text = "アカウント作成に失敗しました。\nIDが重複しています。";
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
