using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CharacterLvUpButton : MonoBehaviour
{
    [SerializeField] private GameObject LvUpRequire;
    [SerializeField] private Button characterLvUpButton;
    [SerializeField] private HeaderItems headerItems;
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private CharacterLvUpPage characterLvUpPage;
    [SerializeField] private FooterText footerText;
    [SerializeField] private CharacterLvUpRequire characterLvUpRequire;
    [SerializeField] private CharacterLvUpApi characterLvUpApi;
    public AudioClip[] audioClips;
    //キャラクターのレベルアップ
    public void LvUp()
    {
        StartCoroutine(RunCharacterLvUp());
    }
    
    private IEnumerator RunCharacterLvUp()
    {
        int activeCharacter = characterLvUpPage.activeCharacter;
        //ボタンオフ
        gameObject.GetComponent<Button>().interactable = false;
        //APIを叩く
        yield return StartCoroutine(characterLvUpApi.Run(activeCharacter + 1));
        //サウンド
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if(GameDatas.charactersLv[activeCharacter] % 10 == 0)
        {
           audioSource.clip = audioClips[0];
        }
        else
        {
            audioSource.clip = audioClips[1];
        }
        audioSource.Play();
        //キャラクターのステータスのロード
        characterStatus.CharacterStatusView();
        //アイテム表示の更新
        headerItems.WriteHeaderItemsNumLvUp();
        characterLvUpRequire.WriteLvUpCost(activeCharacter);
        //フッターの書き込み（期待値を表示する）
        footerText.WriteFooterText(characterLvUpPage.CharacterLvUpToFooterText(), 20);
        //1.5秒待機
        yield return new WaitForSeconds(1.5f);
        //レベルアップ条件の確認F
        gameObject.GetComponent<Button>().interactable = CheckLvUpCondition(activeCharacter);
        //キャラレベルの確認
        CheckCharactersLv(characterLvUpPage.activeCharacter);
    }
    
    //レベルアップ条件の確認
    public bool CheckLvUpCondition(int activeCharacter)
    {
        //全てのアイテム所持数がレベルアップ要件を満たすか確認
        for (int i = 0; i < GameDatas.itemsNum.Length; i++)
        {
            //次のレベルが１０の倍数ではないとき
            if((GameDatas.charactersLv[characterLvUpPage.activeCharacter] + 1) % 10 != 0)
            {
                if (GameDatas.itemsNum[i] < GameDatas.lvUpRequireCase[activeCharacter][i])
                {
                    return false;
                }
            }
            else//次のレベルが１０の倍数のとき
            {
                if (GameDatas.itemsNum[i] < GameDatas.lvUpRequireCaseEx[activeCharacter][i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void CheckCharactersLv(int activeCharacter)
    {
        if (GameDatas.charactersLv[activeCharacter] >= GameDatas.charactersLvLimit)
        { 
            characterLvUpButton.interactable = false;
            LvUpRequire.SetActive(false);
        }
    }
    
}
