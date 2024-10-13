using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePage : MonoBehaviour
{
    [SerializeField] private CharacterIcons characterIcons;
    [SerializeField] private CharacterStatusInGame characterStatusInGame;
    [SerializeField] private FooterText footerText;
    public int activeCharacter;
    private GameObject characterToggle;
    [SerializeField] public GameObject activeCharacterAtInGamePage;
    [SerializeField] private GameObject icon;
    private TextMeshProUGUI statusText;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject characterIconToggleInGame;
    public int clickCount;
    public int currentMoney;//表示されるMoney
    public bool isInProgress;
    
    public void LoadActiveCharacterInGamePage()
    {
        if (activeCharacter >= 0)
        {
            activeCharacterAtInGamePage.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(GameDatas.characterImagePath[activeCharacter]); //スプライトを設定
            activeCharacterAtInGamePage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            activeCharacterAtInGamePage.GetComponent<Image>().sprite = null;
            activeCharacterAtInGamePage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
    
    public void CharacterLoadInGamePage()
    {
        ToggleGroup characterIconToggleGroup = this.characterIconToggleInGame.GetComponent<ToggleGroup>();
        //所持キャラクターのアイコンを生成
        for (int i = 0; i < GameDatas.charactersLv.Length; i++)
        {
            //キャラクターのレベルが１以上かつまだオブジェクトが生成されてないとき
            if (GameDatas.charactersLv[i] > 0 && GameObject.Find($"GameIcon{i}") == null)
            {
                characterToggle = Instantiate(icon, characterIconToggleInGame.transform); //Toggleの生成/////Resources.Load<GameObject>("Prefab/Icon");//
                characterToggle.name = "GameIcon" + i.ToString(); //名前を設定
                characterToggle.transform.Find("Background").gameObject.GetComponent<Image>().sprite
                    = Resources.Load<Sprite>(GameDatas.characterImagePath[i]);//CharacterToggleの子オブジェクトのBackgroundのスプライトを設定
                Toggle toggleIcon = characterToggle.GetComponent<Toggle>();//トグルを取得
                //トグルグループを設定
                toggleIcon.group = characterIconToggleGroup;
                //トグルの値が変化した時に実行する関数を設定する
                toggleIcon.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
                {
                    characterIcons.OnToggleValueChangedInGame(toggleIcon);
                });
            }
        }
        //アクティブ状態を初期化
        activeCharacter = -1;//選択なしの状態
        //全てのアイコンを非アクティブにする
        if (GameObject.FindWithTag("active"))
        {
            characterIcons.CharacterNotActive(GameObject.FindWithTag("active"));
        }
        //GameStartテキストの書き込み
        GameStart();
        //アクティブキャラクターのロード
        LoadActiveCharacterInGamePage();
        //キャラクターのステータスのロード
        characterStatusInGame.CharacterStatusViewInGame();
        //フッターの書き込み
        footerText.WriteFooterText(InGameToFooterText(), 20);
    }
    
    //ゲームスタートのテキスト表示
    public void GameStart()
    {
        if (activeCharacter >= 0)
        {
            gameStart.GetComponent<TextMeshProUGUI>().text = "キャラクターが選択されました。\nゲームスタート！";
        }
        else
        {
            gameStart.GetComponent<TextMeshProUGUI>().text = "キャラクターを選択してください。";
        }
    }
    
    //フッターへの書き込み;InGamePage
    private string InGameToFooterText()
    {
        string text = "PUSHボタンを押すとキャラクターのステータスに応じてお金が増えます。\n";
        return text;
    }
    
    public int CalcAddMoney()
    {
        var plus = (int)Mathf.Floor(Mathf.Sqrt(GameDatas.charactersStatus[activeCharacter]));
        return Mathf.Min(plus, Mathf.Max(0, GameDatas.moneyLimit - currentMoney));
    }
}
