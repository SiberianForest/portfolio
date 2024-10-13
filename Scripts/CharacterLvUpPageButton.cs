using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLvUpPageButton : MonoBehaviour
{
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject characterLvUp;
    [SerializeField] private CharacterLvUpPage characterLvUpPage;
    [SerializeField] private CharacterLvUpRequire characterLvUpRequire;
    [SerializeField] private TransitionScript transitionScript;
    [SerializeField] private MyPage myPageScript;
    
    public void CharacterLvUpPage()
    {
        StartCoroutine(LoadCharacterLvUpPage());
    }
    
    private IEnumerator LoadCharacterLvUpPage()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(transitionScript.TransitionPage());
        //アイテム画像の取得
        yield return StartCoroutine(characterLvUpRequire.GetItemImages());
        myPageScript.OffGachaResult10();
        myPage.SetActive(false);
        characterLvUp.SetActive(true);
        //キャラクターのリロード
        characterLvUpPage.CharacterLoadLvUpPage();
        //ページのロード
        characterLvUpPage.LoadLvUpPage();
    }
}
