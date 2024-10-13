using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCreateApi : BasePostApi
{
    protected override string Uri => GameConfig.baseUrl + "/game/user/create";
    
    protected override string JsonData => JsonUtility.ToJson(null);

    protected override void HandleResponse(string jsonResponse)
    {
        return;
    }
    
    protected override void Comment()
    {
        return;
    }
    
    public IEnumerator Run()
    {
        yield return SendPostRequest();
    }
}