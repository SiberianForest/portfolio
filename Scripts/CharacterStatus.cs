using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterStatusText;
    [SerializeField] private CharacterLvUpPage characterLvUpPage;
    
    //キャラクターのステータスのロード
    public void CharacterStatusView()
    {
        int activeCharacter = characterLvUpPage.activeCharacter;
        //キャラクターのレベルの表示
        characterStatusText.text = "LV: " + GameDatas.charactersLv[activeCharacter].ToString();
        //キャラクターのステータスの表示
        characterStatusText.text += "\nSTATUS: " + GameDatas.charactersStatus[activeCharacter].ToString();
    }
}