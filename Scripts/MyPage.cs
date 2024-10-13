using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyPage : MonoBehaviour
{
    [SerializeField] private GameObject gachaResult;
    [SerializeField] private GameObject gachaResult10;
    [SerializeField] private GameObject gachaResult10Icon;
    [SerializeField] private GameObject itemGacha10Script;
    [SerializeField] private Button itemGachaButton;
    [SerializeField] private Button characterGachaButton;
    [SerializeField] private Button itemGacha10Button;
    [SerializeField] public FooterText footerText;
    [SerializeField] private HeaderItems headerItems;
    public float pos;
    public float time = 0;
    
    void Update()
    {
        if(gachaResult.activeSelf)
        {
            time += Time.deltaTime;
            pos = (Mathf.Abs(((time * 3.0f % 1.0f) * 2.0f - 1.0f)) - 0.5f) * 0.5f;
            gachaResult.transform.position += new Vector3(pos, 0, 0);
        }
    }
    //所持金の確認
    public void CheckMoney()
    {
        if (GameDatas.money < GameDatas.itemGachaCost)
        {
            itemGachaButton.interactable = false;
        }
        else
        {
            itemGachaButton.interactable = true;
        }
        if (GameDatas.money < GameDatas.characterGachaCost)
        { 
            characterGachaButton.interactable = false;
        }
        else
        { 
            characterGachaButton.interactable = true;
        }
        if (GameDatas.money < GameDatas.itemGachaCost * 10)
        {
            itemGacha10Button.interactable = false;
        }
        else
        {
            itemGacha10Button.interactable = true;
        }
    }
    
    //アイテムガチャ結果表示、キャラクターガチャの重複時にも使用
    public void ItemGachaView(int result)
    {
        //ゲームオブジェクトが存在してたら消す
        if(gachaResult.activeSelf || gachaResult10.activeSelf)
        {
            OffGachaResult();
            OffGachaResult10();
        }
        //gachaResultのImageを取得
        Image gachaResultImage = gachaResult.GetComponent<Image>();
        //gachaResultのImageにスプライトを設定
        gachaResultImage.sprite = Resources.Load<Sprite>(GameDatas.itemImagePath[result]);
        //gachaResultをアクティブにする
        gachaResult.SetActive(true);
        time = 0;
        //数秒で非表示にする
        Invoke("OffGachaResult", 2f);
    }
    
        
    //アイテム数の確認
    public void CheckItems()
    {
        var isItemFullMax = true;
        for (int i = 0; i < GameDatas.itemsNum.Length - 1; i++)
        {
            if (GameDatas.itemsNum[i] < GameDatas.itemsNumLimit)
            {
                isItemFullMax = false;
                break;
            }
        }
        if(isItemFullMax)
        {
            itemGachaButton.GetComponent<Button>().interactable = false;
            itemGacha10Button.GetComponent<Button>().interactable = false;
        }
    }

    public IEnumerator ItemGacha10View(int[] results)
    {
        //ゲームオブジェクトが存在してたら消す
        if (gachaResult10.activeSelf　|| gachaResult.activeSelf)
        {
            OffGachaResult();
            OffGachaResult10();
        }

        //gachaResult10の子オブジェクトが存在していなかった場合
        if (!gachaResult10.transform.Find("GachaResult10Content0"))
        {
            for (int i = 0; i < GameDatas.itemsNum.Length - 1; i++)
            {
                GameObject gachaResult10Content = Instantiate(gachaResult10Icon, gachaResult10.transform);
                gachaResult10Content.name = "GachaResult10Content" + i.ToString();
                //gachaResultのImageを取得
                Image gachaResult10Image = gachaResult10Content.transform.GetComponent<Image>();
                //gachaResultのImageにスプライトを設定
                gachaResult10Image.sprite = Resources.Load<Sprite>(GameDatas.itemImagePath[i]);
                //off
                gachaResult10Content.SetActive(false);
            }
        }
        gachaResult10.SetActive(true);
        //結果の数だけアイコンを生成
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] < 0)
            {
                continue;
            }
            //gachaResult10の子オブジェクトにgachaResult10Iconを生成
            GameObject gachaResult10ContentIcon = Instantiate(gachaResult10.transform.Find("GachaResult10Content"
                + results[i].ToString()).gameObject, gachaResult10.transform);
            //gachaResult10ContentIconのタグを設定
            gachaResult10ContentIcon.tag = "GachaResult10ContentIcon";
            //gachaResult10の子オブジェクトをアクティブにする
            gachaResult10ContentIcon.SetActive(false);
            yield return new WaitForSeconds(0.08f);
            //サウンド
            itemGacha10Script.GetComponent<AudioSource>().Play();
            gachaResult10ContentIcon.SetActive(true);
        }
        Invoke("OffGachaResult10", 1f);
    }

    private IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    //ガチャ結果の削除
    public void OffGachaResult()
    {
        gachaResult.SetActive(false);
    }
    
    public void OffGachaResult10()
    {
        //タグがGachaResult10ContentIconのオブジェクトを全て削除
        foreach (GameObject gachaResult10ContentIcon in GameObject.FindGameObjectsWithTag("GachaResult10ContentIcon"))
        {
            Destroy(gachaResult10ContentIcon);
        }
        gachaResult10.SetActive(false);
    }

    public void CharacterGachaView(int result)
    {
        //ゲームオブジェクトが存在してたら消す
        if(gachaResult.activeSelf || gachaResult10.activeSelf)
        {
            OffGachaResult();
            OffGachaResult10();
        }
        if (result == 0)//キャラクターが重複した時
        {  
            //取得アイテム表示
            ItemGachaView(3);
            //アイテムの個数をヘッダに反映
            headerItems.WriteHeaderItemsNumGacha(3);
        }
        else
        {
            //gachaResultのImageを取得
            Image gachaResultImage = gachaResult.GetComponent<Image>();
            //gachaResultのImageにスプライトを設定
            gachaResultImage.sprite = Resources.Load<Sprite>(GameDatas.characterImagePath[result]);
            //gachaResultをアクティブにする
            gachaResult.SetActive(true);
            //数秒で非表示にする
            Invoke("OffGachaResult", 2f);
        }
    }
    
    //テキストクリア
    public void TextClear()
    {
        footerText.ClearFooterText();
    }

    public void WriteFooterToLvUpPage()
    {
        footerText.WriteFooterText("キャラクターレベルアップページへ遷移", 20);
    }
    
    public void WriteFooterToInGamePage()
    {
        footerText.WriteFooterText("ゲームページへ遷移", 20);
    }
}
