using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterLvUpPage : MonoBehaviour
{
    [SerializeField] private GameObject characterIconToggle;
    [SerializeField] private GameObject icon;
    [SerializeField] public GameObject activeCharacterAtLvUpPage;
    [SerializeField] private GameObject costText;
    [SerializeField] private Button lvUpButton;
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private CharacterIcons characterIcons;
    [SerializeField] private CharacterLvUpButton characterLvUpButton;
    [SerializeField] private CharacterLvUpRequire characterLvUpRequire;
    [SerializeField] private FooterText footerText;
    public int activeCharacter;
    private GameObject characterToggle;
    
    //アクティブキャラクターのロード
    public void LoadActiveCharacter()
    {
        activeCharacterAtLvUpPage.GetComponent<Image>().sprite =
            Resources.Load<Sprite>(GameDatas.characterImagePath[activeCharacter]); //スプライトを設定
    }

    public void CharacterLoadLvUpPage()//画面が遷移してくるとき実行
    {
        ToggleGroup characterIconToggleGroup = this.characterIconToggle.GetComponent<ToggleGroup>();
        //所持キャラクターのアイコンを生成
        for (int i = 0; i < GameDatas.charactersLv.Length; i++)
        {
            //キャラクターのレベルが１以上かつまだオブジェクトが生成されてないとき
            if (GameDatas.charactersLv[i] > 0 && GameObject.Find($"Icon{i}") == null)
            {
                    characterToggle = Instantiate(icon, characterIconToggle.transform); //Toggleの生成/////Resources.Load<GameObject>("Prefab/Icon");//
                    characterToggle.name = "Icon" + i.ToString(); //名前を設定
                    characterToggle.transform.Find("Background").gameObject.GetComponent<Image>().sprite
                        = Resources.Load<Sprite>(GameDatas.characterImagePath[i]);//CharacterToggleの子オブジェクトのBackgroundのスプライトを設定
                    Toggle toggleIcon = characterToggle.GetComponent<Toggle>();//トグルを取得
                    //トグルグループを設定
                    toggleIcon.group = characterIconToggleGroup;
                    //トグルの値が変化した時に実行する関数を設定する
                    toggleIcon.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
                    {
                        characterIcons.OnToggleValueChanged(toggleIcon);
                    });
            }
        }
    }

    public void LoadLvUpPage()
    {
        //アクティブ状態を初期化
        activeCharacter = 0;
        //最初のキャラクターを選択
        GameObject initial = GameObject.Find("Icon0");
        initial.GetComponent<Toggle>().isOn = true;
        //アクティブにする
        characterIcons.CharacterActive(initial);
        //アクティブキャラクターのロード
        LoadActiveCharacter();
        //キャラクターのステータスのロード
        characterStatus.CharacterStatusView();
        //フッターの書き込み
        footerText.WriteFooterText(CharacterLvUpToFooterText(), 20);
        //レベルアップ条件の確認
        lvUpButton.interactable = characterLvUpButton.CheckLvUpCondition(activeCharacter);
        //レベルアップ条件の書き込み
        costText.GetComponent<TextMeshProUGUI>().text = "COST";
        characterLvUpRequire.WriteLvUpCost(activeCharacter);
    }
    
    //フッターへテキストを渡す
    public string CharacterLvUpToFooterText()
    {
        var nowLv = GameDatas.charactersLv[activeCharacter];
        var nowStatus = GameDatas.charactersStatus[activeCharacter];
        //レベルアップ状況の書き込み
        string text =
            "LV: " + nowLv.ToString() + "→" +
            (nowLv + 1).ToString() + "     /    "
            //ステータスアップ状況の書き込み
            + "STATUS: " + nowStatus.ToString() + "→" +
            GameDatas.nextCharactersStatus[activeCharacter];
        return text;
    }
}
