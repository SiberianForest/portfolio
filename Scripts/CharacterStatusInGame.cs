using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatusInGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterStatusText;
    [SerializeField] private InGamePage inGamePage;
    
    public void CharacterStatusViewInGame()
    {
        if (inGamePage.activeCharacter >= 0)
        {
            int activeCharacter = inGamePage.activeCharacter;
            //キャラクターのレベルの表示
            characterStatusText.text = "LV: " + GameDatas.charactersLv[activeCharacter].ToString();
            //キャラクターのステータスの表示
            characterStatusText.text +=
                "\nSTATUS: " + GameDatas.charactersStatus[activeCharacter].ToString();
        }
        else
        {
            characterStatusText.text = "";
        }
    }
}
