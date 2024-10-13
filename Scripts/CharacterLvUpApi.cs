using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCharacterLvUpData
{
    public CharacterLvUpResult data;
}

[System.Serializable]
public class CharacterLvUpResult
{
    public int[] itemsNum;
    public int[] charactersLv;
    public int[] charactersStatus;
    public int[] nextCharactersStatus;
}

[System.Serializable]
public class PostDataLvUp
{
    public int activeCharacter;
    public int clickCount;
}

public class CharacterLvUpApi : BasePostApi
{
    private int activeCharacterLvUp;
    protected override string Uri => GameConfig.baseUrl + "/game/character_lv_up";
    
    protected override string JsonData => JsonUtility.ToJson(new PostDataLvUp
    {
        activeCharacter = activeCharacterLvUp
    });
    
    protected override void HandleResponse(string jsonResponse)
    {
        GetCharacterLvUpData getCharacterLvUp = JsonUtility.FromJson<GetCharacterLvUpData>(jsonResponse);
        //各々でデータ処理を行う
        GameDatas.itemsNum = getCharacterLvUp.data.itemsNum;
        GameDatas.charactersLv = getCharacterLvUp.data.charactersLv;
        GameDatas.charactersStatus = getCharacterLvUp.data.charactersStatus;
        GameDatas.nextCharactersStatus = getCharacterLvUp.data.nextCharactersStatus;
    }
    
    protected override void Comment()
    {
        return;
    }
    
    public IEnumerator Run(int activeCharacter)
    {
        this.activeCharacterLvUp = activeCharacter;
        yield return SendPostRequest();
    }
}
