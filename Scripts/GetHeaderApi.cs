using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetData
{
    public HeaderData data;
}

[System.Serializable]
public class HeaderData
{
    public string userId;
    public string name;
    public int money;
    public int[] itemsNum;
    public int[] charactersLv;
    public int[] charactersStatus;
    public int[] nextCharactersStatus;
    public string[] itemImagePath;
    public string[] characterImagePath;
    public int[] lvUpRequireCaseArray1;
    public int[] lvUpRequireCaseArray2;
    public int[] lvUpRequireCaseArray3;
    public int[] lvUpRequireCaseArray4;
    public int[] lvUpRequireCaseExArray1;
    public int[] lvUpRequireCaseExArray2;
    public int[] lvUpRequireCaseExArray3;
    public int[] lvUpRequireCaseExArray4;
    public int itemGachaCost;
    public int characterGachaCost;
    public int moneyLimit;
    public int itemsNumLimit;
    public int charactersLvLimit;
}

public class GetHeaderApi : BaseGetApi
{
    protected override string Uri => GameConfig.baseUrl + "/game/get_header";
    
    protected override void HandleResponse(string jsonResponse)
    {
        GetData getData = JsonUtility.FromJson<GetData>(jsonResponse);
        //各々でデータ処理を行う
        GameDatas.userId = getData.data.userId;
        GameDatas.userName = getData.data.name;
        GameDatas.money = getData.data.money;
        GameDatas.itemsNum = getData.data.itemsNum;
        GameDatas.charactersLv = getData.data.charactersLv;
        GameDatas.charactersStatus = getData.data.charactersStatus;
        GameDatas.nextCharactersStatus = getData.data.nextCharactersStatus;
        GameDatas.itemImagePath = getData.data.itemImagePath;
        GameDatas.characterImagePath = getData.data.characterImagePath;
        GameDatas.lvUpRequireCase = new int[][]
        {
            getData.data.lvUpRequireCaseArray1,
            getData.data.lvUpRequireCaseArray2,
            getData.data.lvUpRequireCaseArray3,
            getData.data.lvUpRequireCaseArray4
        };
        GameDatas.lvUpRequireCaseEx = new int[][]
        {
            getData.data.lvUpRequireCaseExArray1,
            getData.data.lvUpRequireCaseExArray2,
            getData.data.lvUpRequireCaseExArray3,
            getData.data.lvUpRequireCaseExArray4
        };
        GameDatas.itemGachaCost = getData.data.itemGachaCost;
        GameDatas.characterGachaCost = getData.data.characterGachaCost;
        GameDatas.moneyLimit = getData.data.moneyLimit;
        GameDatas.itemsNumLimit = getData.data.itemsNumLimit;
        GameDatas.charactersLvLimit = getData.data.charactersLvLimit;
    }
    
    public IEnumerator Run()
    {
        yield return SendGetRequest();
    }
}
