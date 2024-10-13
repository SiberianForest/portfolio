using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CharacterGacha : MonoBehaviour
{
    [SerializeField] public MyPage myPage;
    [SerializeField] private Money money;
    [SerializeField] private Button itemGachaButton;
    [SerializeField] private Button itemGacha10Button;
    [SerializeField] private HeaderCharacters headerCharacters;
    [SerializeField] private CharacterGachaApi characterGachaApi;
    public AudioClip[] audioClips;
    
    public void CharacterGachaButton()
    {
        StartCoroutine(RunCharacterGacha());
    }
    
    private IEnumerator RunCharacterGacha()
    {
        //ボタンオフ
        gameObject.GetComponent<Button>().interactable = false;
        itemGachaButton.interactable = false;
        itemGacha10Button.interactable = false;
        //APIを叩く
        yield return StartCoroutine(characterGachaApi.Run());
        //キャラクターガチャの結果を取得
        var characterGachaResult = characterGachaApi.GetCharacterGachaResult();
        //所持金の更新
        money.WriteHeaderMoney(GameDatas.money);
        //サウンド
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if(characterGachaResult == 0)
        {
            audioSource.clip = audioClips[0];
        }
        else
        {
            audioSource.clip = audioClips[1];
        }
        audioSource.Play();
        //ガチャ結果表示
        myPage.CharacterGachaView(characterGachaResult);
        //キャラクター所持の更新
        headerCharacters.WriteHeaderCharactersHave(characterGachaResult);
        //1.5秒待機
        yield return new WaitForSeconds(1.5f);
        if (characterGachaResult >= 0)
        {
            //残金確認
            myPage.CheckMoney();
            //キャラクターとアイテム確認
            CheckCharacters();
            //アイテム数の確認
            myPage.CheckItems();
        }
        else
        {
            //フッターの書き込み
            myPage.footerText.WriteFooterText("何も得られませんでした。", 20);
        }
    }
    
    //キャラクターの確認
    private void CheckCharacters()
    {
        var notHaveCharacter = false;
        for (int i = 0; i < GameDatas.charactersLv.Length; i++)
        {
            if (GameDatas.charactersLv[i] < 1)
            {
                notHaveCharacter = true;
                break;
            }
        }
        if(!notHaveCharacter && GameDatas.itemsNum[3] >= GameDatas.itemsNumLimit)
        { 
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
    
    public void WriteFooterTextCharacterGacha()
    {
        myPage.footerText.WriteFooterText($"キャラクターガチャ   コスト:{GameDatas.characterGachaCost}",20);
    }
}
