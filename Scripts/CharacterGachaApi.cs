using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetCharacterGachaData
{
    public CharacterGachaResult data;
}

[System.Serializable]
public class CharacterGachaResult
{
    public int money;
    public int[] charactersLv;
    public int[] charactersStatus;
    public int itemsNum4;
    public int result;
}

public class CharacterGachaApi : BaseGetApi
{
    private GetCharacterGachaData getCharacterGachaData;
    
    protected override string Uri => GameConfig.baseUrl + "/game/gacha_character";
    
    protected override void HandleResponse(string jsonResponse)
    {
        getCharacterGachaData = JsonUtility.FromJson<GetCharacterGachaData>(jsonResponse);
        //各々でデータ処理を行う
        GameDatas.money = getCharacterGachaData.data.money;
        GameDatas.charactersLv = getCharacterGachaData.data.charactersLv;
        GameDatas.charactersStatus = getCharacterGachaData.data.charactersStatus;
        GameDatas.itemsNum[3] = getCharacterGachaData.data.itemsNum4;
    }
    
    public IEnumerator Run()
    {
        yield return SendGetRequest();
    }
    
    public int GetCharacterGachaResult()
    {
        //0,1,2,3,(アイテム4が上限のとき)-2
        return getCharacterGachaData.data.result == 0 ? 0 : getCharacterGachaData.data.result - 1;
    }
}
