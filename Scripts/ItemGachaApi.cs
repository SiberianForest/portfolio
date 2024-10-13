using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GetItemGachaData
{
    public ItemGachaResult data;
}

[System.Serializable]
public class ItemGachaResult
{
    public int money;
    public int[] itemsNum;
    public int[] results;
}

[System.Serializable]
public class PostDataGacha
{
    public int iteration;
}

public class ItemGachaApi : BasePostApi
{
    private int itemGachaIteration;
    protected override string Uri => GameConfig.baseUrl + "/game/gacha_item";
    
    protected override string JsonData => JsonUtility.ToJson(new PostDataGacha
    {
        iteration = itemGachaIteration
    });
    
    protected override void HandleResponse(string jsonResponse)
    {
        GetItemGachaData getItemGachaData = JsonUtility.FromJson<GetItemGachaData>(jsonResponse);
        //各々でデータ処理を行う
        GameDatas.money = getItemGachaData.data.money;
        GameDatas.itemsNum = getItemGachaData.data.itemsNum;
        GameDatas.itemGachaResults = getItemGachaData.data.results;
    }
    
    protected override void Comment()
    {
        return;
    }
    
    public IEnumerator Run(int itemGachaType)
    {
        this.itemGachaIteration = itemGachaType;
        yield return SendPostRequest();
    }
}