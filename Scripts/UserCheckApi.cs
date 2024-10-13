using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetExistUser
{
    public bool exist;
}

public class UserCheckApi : BaseGetApi
{
    GetExistUser getExistUser;
    protected override string Uri => GameConfig.baseUrl + "/game/user/check";
    protected override void HandleResponse(string jsonResponse)
    {
        getExistUser = JsonUtility.FromJson<GetExistUser>(jsonResponse);
        //各々でデータ処理を行う
    }
    
    public IEnumerator Run()
    {
        yield return SendGetRequest();
    }
    
    public bool GetCheck()
    {
        if(getExistUser == null) return true;
        return getExistUser.exist;
    }
}