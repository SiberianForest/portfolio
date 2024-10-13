using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemGacha : MonoBehaviour
{
    [SerializeField] public MyPage myPage;
    [SerializeField] public HeaderItems headerItems;
    [SerializeField] public Money money;
    [SerializeField] public Button characterGachaButton;
    [SerializeField] private Button itemGacha10Button;
    [SerializeField] private ItemGachaApi itemGachaApi;
    
    public void ItemGachaButton()
    {
        StartCoroutine(RunItemGacha());
    }
    
    private IEnumerator RunItemGacha()
    {
        //ボタンオフ
        gameObject.GetComponent<Button>().interactable = false;
        characterGachaButton.interactable = false;
        itemGacha10Button.interactable = false;
        //回数
        var itemGachaType = 1;
        //APIを叩く
        yield return StartCoroutine(itemGachaApi.Run(itemGachaType));
        //アイテムガチャの結果を取得
        var itemGachaResult = GameDatas.itemGachaResults[0];
        //所持金の更新
        money.WriteHeaderMoney(GameDatas.money);
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        //ガチャ結果表示
        myPage.ItemGachaView(itemGachaResult);
        //アイテム数の更新
        headerItems.WriteHeaderItemsNumGacha(itemGachaResult);
        //1.5秒待機
        yield return new WaitForSeconds(1.5f);
        if(itemGachaResult >= 0)
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
    
    
    public void WriteFooterTextItemGacha()
    {
        myPage.footerText.WriteFooterText($"アイテムガチャ   コスト:{GameDatas.itemGachaCost}",20);
    }
    
}
