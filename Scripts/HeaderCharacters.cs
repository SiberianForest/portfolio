using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeaderCharacters : MonoBehaviour
{
    private TextMeshProUGUI statusText;
    public static GameObject[] characterImages;
    [SerializeField] private GameObject characterPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        statusText = GetComponent<TextMeshProUGUI>();
        statusText.text = "CHARACTERS: ";
    }
    
    public void WriteHeaderCharacters()
    {
        //キャラクター画像のロード
        characterImages = new GameObject[GameDatas.charactersLv.Length];
        for(int i = 0; i < GameDatas.charactersLv.Length; i++)
        {
            //キャラクターのインスタンスを呼び出し。
            GameObject characterImage = Instantiate(Resources.Load<GameObject>("Prefabs/CharacterHave")
                , characterPanel.transform);//ゲームオブジェクト生成
            characterImage.name = $"CharacterImage{i}";//名前を設定
            characterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameDatas.characterImagePath[i]);//スプライトを設定
            characterImage.SetActive(GameDatas.charactersLv[i] > 0 ? true : false);//ヘッダに表示
            characterImages[i] = characterImage;//配列に追加
        }
    }
    
    public void WriteHeaderCharactersHave(int result)
    {
        foreach (Transform childTransform in characterPanel.transform)
        {
            if (childTransform.name == $"CharacterImage{result}")
            {
                childTransform.gameObject.SetActive(true);
            }
        }
    }
    
    public void DeleteCharacters()
    {
        foreach (GameObject characterImage in characterImages)
        {
            Destroy(characterImage);
        }
    }
}