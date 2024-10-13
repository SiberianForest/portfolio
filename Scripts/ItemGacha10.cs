using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGacha10 : MonoBehaviour
{
    [SerializeField] public MyPage myPage;
    [SerializeField] public HeaderItems headerItems;
    [SerializeField] public Money money;
    [SerializeField] public Button characterGachaButton;
    [SerializeField] private Button itemGachaButton;
    [SerializeField] private ItemGachaApi itemGachaApi;
    
    public void ItemGacha10Button()
    {
        StartCoroutine(RunItemGacha10());
    }
    
    private IEnumerator RunItemGacha10()
    {
        //ボタンオフ
        gameObject.GetComponent<Button>().interactable = false;
        characterGachaButton.interactable = false;
        itemGachaButton.interactable = false;
        //回数
        var itemGachaType = 10;
        //APIを叩く
        yield return StartCoroutine(itemGachaApi.Run(itemGachaType));
        //アイテムガチャの結果を取得
        var itemGachaResults = GameDatas.itemGachaResults;
        //所持金の更新
        money.WriteHeaderMoney(GameDatas.money);
        //ガチャ結果表示
        yield return myPage.ItemGacha10View(itemGachaResults);
        //アイテム数の更新
        headerItems.WriteHeaderItemsNumGacha10(itemGachaResults);
        //1.5秒待機
        yield return new WaitForSeconds(1.5f);
        if(itemGachaResults[0] >= 0)
        {
            //所持金の確認
            myPage.CheckMoney();
            //アイテム数の確認
            myPage.CheckItems();
        }
        else
        {
            //フッターの書き込み
            myPage.footerText.WriteFooterText("アイテム数が最大です。ガチャ結果を得られませんでした。", 20);
        }
        
    }
    
    public void WriteFooterTextItemGacha10()
    {
        myPage.footerText.WriteFooterText($"10連アイテムガチャ   コスト:{GameDatas.itemGachaCost * 10}",20);
    }
}
