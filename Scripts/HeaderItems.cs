using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeaderItems : MonoBehaviour
{
    public GameObject[] itemImages;
    [SerializeField] private GameObject itemPanel;
    public void WriteHeaderItems()
    {
        //アイテム画像のロード
        itemImages = new GameObject[GameDatas.itemsNum.Length];
        for (int i = 0; i < GameDatas.itemsNum.Length; i++)
        {
            //キャラクターのプロパティ付き（未実装）インスタンスを呼び出し。
            GameObject itemImage = Instantiate(Resources.Load<GameObject>("Prefabs/ItemNum")
            , itemPanel.transform);//ゲームオブジェクト生成
            GameObject image = itemImage.transform.Find("Image").gameObject;//子オブジェクトImageを取得
            GameObject num = itemImage.transform.Find("Num").gameObject;//子オブジェクトNumを取得
            //画像を設定
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameDatas.itemImagePath[i]);//スプライトを設定
            //個数を設定
            num.GetComponent<TextMeshProUGUI>().text = GameDatas.itemsNum[i].ToString();
            itemImage.SetActive(true);//ヘッダに表示
            itemImages[i] = itemImage;//配列に追加
        }
    }
    
    public void WriteHeaderItemsNumGacha(int result)
    {
        //アイテムの個数を更新
        GameObject targetItem = itemImages[result];
        GameObject num = targetItem.transform.Find("Num").gameObject;//子オブジェクトNumを取得
        num.GetComponent<TextMeshProUGUI>().text = GameDatas.itemsNum[result].ToString();
        //テキストの色を変える
        num.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0, 1);
        //2秒後に色を戻す
        StartCoroutine(ReturnTextColor(num, new Color(1, 1, 1, 1)));
    }
    
    public void WriteHeaderItemsNumGacha10(int[] results)
    {
        //アイテムの個数を更新
        foreach (int result in results)
        {
            GameObject targetItem = itemImages[result];
            GameObject num = targetItem.transform.Find("Num").gameObject;//子オブジェクトNumを取得
            num.GetComponent<TextMeshProUGUI>().text = GameDatas.itemsNum[result].ToString();
            //テキストの色を変える
            num.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0, 1);
            //2秒後に色を戻す
            StartCoroutine(ReturnTextColor(num, new Color(1, 1, 1, 1)));
        }
    }
    
    private IEnumerator ReturnTextColor(GameObject num, Color color)
    {
        yield return new WaitForSeconds(2);
        num.GetComponent<TextMeshProUGUI>().color = color;
    }
    
    public void WriteHeaderItemsNumLvUp()
    {
        //アイテムの個数を更新
        for (int i = 0; i < GameDatas.itemsNum.Length; i++)
        {
            GameObject targetItem = itemImages[i];
            GameObject num = targetItem.transform.Find("Num").gameObject;//子オブジェクトNumを取得
            num.GetComponent<TextMeshProUGUI>().text = GameDatas.itemsNum[i].ToString();
        }
    }

    public void DeleteItems()
    {
        foreach (GameObject itemImage in itemImages)
        {
            Destroy(itemImage);
        }
    }
}