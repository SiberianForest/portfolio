using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterLvUpRequire : MonoBehaviour
{
    [SerializeField] private GameObject lvUpItemImage;
    public GameObject[] lvUpImages;
    private bool haveImages = false;
    
    //アイテム画像の取得
    public IEnumerator GetItemImages()
    {
        if(haveImages)
        {
            yield break;
        }
        lvUpImages = new GameObject[GameDatas.itemsNum.Length];
        for (int i = 0; i < GameDatas.itemsNum.Length; i++)
        {
            //インスタンスを呼び出し。
            GameObject itemImage = Instantiate(lvUpItemImage, gameObject.transform);//ゲームオブジェクト生成
            itemImage.GetComponent<LvUpItemNum>().Initialize(i);
            lvUpImages[i] = itemImage;//配列に追加
            itemImage.SetActive(false);
        }
        haveImages = true;
        yield return null;
    }
    
    //レベルアップ条件の書き込み
    public void WriteLvUpCost(int activeCharacter)
    {
        DeleteLvUpCost();
        //次のレベルが１０の倍数ではないとき
        if ((GameDatas.charactersLv[activeCharacter] + 1) % 10 != 0)
        {
            for (int i = 0; i < GameDatas.itemsNum.Length; i++)
            {
                int cost = GameDatas.lvUpRequireCase[activeCharacter][i];
                if (cost > 0)
                {
                    lvUpImages[i].GetComponent<LvUpItemNum>().SetLvUpItemNum(cost);
                    lvUpImages[i].SetActive(true);
                }
            }
        }
        else //次のレベルが１０の倍数のとき
        {
            for (int i = 0; i < GameDatas.itemsNum.Length; i++)
            {
                int cost = GameDatas.lvUpRequireCaseEx[activeCharacter][i];
                if (cost > 0)
                {
                    lvUpImages[i].GetComponent<LvUpItemNum>().SetLvUpItemNum(cost);
                    lvUpImages[i].SetActive(true);
                }
            }
        }
    }
    
    //レベルアップ条件の削除
    public void DeleteLvUpCost()
    {
        foreach(var image in lvUpImages)
        {
            image.SetActive(false);
        }
    }
}
