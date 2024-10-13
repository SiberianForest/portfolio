using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetInGameData
{
    public InGameResult data;
}

[System.Serializable]
public class InGameResult
{
    public int money;
}

[System.Serializable]
public class PostDataInGame
{
    public int activeCharacter;
    public int clickCount;
}

public class InGameApi : BasePostApi
{
    private int activeCharacterInGame;
    private int clickCountInGame;
    protected override string Uri => GameConfig.baseUrl + "/game/in_game";
    
    protected override string JsonData => JsonUtility.ToJson(new PostDataInGame
    {
        activeCharacter = activeCharacterInGame,
        clickCount = clickCountInGame
    });
    
    protected override void HandleResponse(string jsonResponse)
    {
        GetInGameData getInGameData = JsonUtility.FromJson<GetInGameData>(jsonResponse);
        //各々でデータ処理を行う
        GameDatas.money = getInGameData.data.money;
    }
    
    protected override void Comment()
    {
        return;
    }
    
    public IEnumerator Run(int activeCharacter, int clickCount)
    {
        this.activeCharacterInGame = activeCharacter;
        this.clickCountInGame = clickCount;
        yield return SendPostRequest();
    }
}