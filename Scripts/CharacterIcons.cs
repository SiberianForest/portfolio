using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcons : MonoBehaviour
{
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private CharacterStatusInGame characterStatusInGame;
    [SerializeField] private CharacterLvUpPage characterLvUpPage;
    [SerializeField] private CharacterLvUpButton characterLvUpButton;
    [SerializeField] private InGamePage inGamePage;
    [SerializeField] private FooterText footerText;
    [SerializeField] private Button pushButton;
    [SerializeField] private Button lvUpButton;
    [SerializeField] private Push push;
    [SerializeField] private CharacterLvUpRequire characterLvUpRequire;
    
    public static ColorBlock selected = new ColorBlock
    {
        normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        highlightedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        pressedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        selectedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        disabledColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        colorMultiplier = 1.0f
    };
    public static ColorBlock unSelected = new ColorBlock
    {
        normalColor = new Color(0.7f, 0.7f, 0.7f, 0.7f),
        highlightedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        pressedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f),
        selectedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
        disabledColor = new Color(0.0f, 0.0f, 0.0f, 0.5f),
        colorMultiplier = 1.0f
    };
    
    public void OnToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            //非アクティブにする
            if(GameObject.FindWithTag("active"))
            {
                CharacterNotActive(GameObject.FindWithTag("active"));
            }
            //アクティブキャラクターの変更
            characterLvUpPage.activeCharacter = change.name.Length > 4 ? int.Parse(change.name.Substring(4)) : characterLvUpPage.activeCharacter;///////////////////エラー　？？
            //アクティブにする
            CharacterActive(change.gameObject);
            //アクティブキャラクターのロード
            characterLvUpPage.LoadActiveCharacter();
            //キャラクターのステータスのロード
            characterStatus.CharacterStatusView();
            //レベルアップ条件の確認
            lvUpButton.interactable =
                characterLvUpButton.CheckLvUpCondition(characterLvUpPage.activeCharacter);
            //フッターの書き込み
            footerText.WriteFooterText(characterLvUpPage.CharacterLvUpToFooterText(), 20);
            //アイテム条件
            characterLvUpRequire.WriteLvUpCost(characterLvUpPage.activeCharacter);
        }
    }
    
    public void OnToggleValueChangedInGame(Toggle change)
    {
        if (change.isOn)
        {
            if(inGamePage.clickCount > 0)
            {
                //APIを叩く
                StartCoroutine(push.RunInGame());
            }
            //非アクティブにする
            if(GameObject.FindWithTag("active"))
            {
                CharacterNotActive(GameObject.FindWithTag("active"));
            }
            //アクティブキャラクターの変更
            inGamePage.activeCharacter = change.name.Length > 8 ? int.Parse(change.name.Substring(8)) : inGamePage.activeCharacter;///////////////////エラー　？？
            //アクティブにする
            CharacterActive(change.gameObject);
            //アクティブキャラクターのロード
            inGamePage.LoadActiveCharacterInGamePage();
            //キャラクターのステータスのロード
            characterStatusInGame.CharacterStatusViewInGame();
            //Game Startのテキスト表示
            inGamePage.GameStart();
            //PUSHボタンをinteractableにする
            pushButton.interactable = false;
            Invoke("PushButtonInteractable", 0.5f);
        }
    }
    
    //pushボタンをinteractableにする
    public void PushButtonInteractable()
    {
        pushButton.interactable = true;
    }
    
    //キャラクターをアクティブにする
    public void CharacterActive(GameObject icon)
    {
        //アクティブキャラクターのアイコンの色を設定
        icon.GetComponent<Toggle>().colors = selected;
        //アクティブキャラクターのタグを設定
        icon.tag = "active";
    }
    
    //キャラクターを非アクティブにする
    public void CharacterNotActive(GameObject icon)
    {
        //アクティブキャラクターのアイコンの色を設定
        icon.GetComponent<Toggle>().colors = unSelected;
        //アクティブキャラクターのタグを設定
        icon.tag = "notActive";
    }
}
